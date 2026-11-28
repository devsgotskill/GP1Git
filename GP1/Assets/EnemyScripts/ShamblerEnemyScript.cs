using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// Script created by: Damian Zuniga
public class ShamblerEnemy : EnemyBase
{
    [SerializeField] private DetectionVolume player_detection_volume;
    [SerializeField] protected DetectionVolume in_range_to_attack_volume;
    [SerializeField] protected EnemyHitBox hit_box;
    [SerializeField] protected float chase_time = 5;
    [SerializeField] protected float attack_time = 2;
    [SerializeField] protected float hurt_time = 1;

    

    private void Awake()
    {
        hit_box.enabled = false;
        hit_box.GetComponent<EnemyHitBox>().Initialise(attack_damage);
    }

    public void IdleBehavior()
    {
        if (player_detection_volume.is_detecting) // switch to chase
        {
            agent_reference.isStopped = false;
            countdown_timer = chase_time;
            Debug.Log("Entering Chase State");
            current_state = enemy_states.chase_state;
        }

        if (in_range_to_attack_volume.is_detecting) // switch to attack
        {
            countdown_timer = attack_time;
            Debug.Log("Entering Attack State");
            current_state = enemy_states.attack_state;
        }
    }

    public void ChaseBehavior()
    {
        OrientTowardsPlayer();
        agent_reference.destination = player_transform_reference.position;
        agent_reference.speed = movement_speed;

        countdown_timer -= Time.deltaTime;
        if (countdown_timer <= 0) // switch to idle
        {

            agent_reference.isStopped = true;
            player_detection_volume.is_detecting = false;
            countdown_timer = chase_time;
            Debug.Log("Entering Idle State");
            current_state = enemy_states.idle_state;
        }

        if (in_range_to_attack_volume.is_detecting) // switch to attack
        {
            countdown_timer = attack_time;
            Debug.Log("Entering Attack State");
            current_state = enemy_states.attack_state;
        }
    }

    public void AttackBehavior()
    { // perchance add a way to speed up the enemy while its attacking needs to get added
        agent_reference.destination = in_range_to_attack_volume.point_of_collision;
        countdown_timer -= Time.deltaTime;
        hit_box.enabled = true;

        if (countdown_timer <= 0) // switch to chase
        {
            in_range_to_attack_volume.is_detecting = false;
            hit_box.enabled = false;
            countdown_timer = chase_time;
            Debug.Log("Entering Chase State");
            current_state = enemy_states.chase_state;
        }
    }

    public void HurtBehavior()
    {
        agent_reference.isStopped = true;
        countdown_timer -= Time.deltaTime;
        if (countdown_timer <= 0) // switch to chase
        {
            agent_reference.isStopped = false;
            countdown_timer = chase_time;
            Debug.Log("Entering Chase State");
            current_state = enemy_states.chase_state;
        }
    }

    public override void TakeDamage(int damage)
    {
        if (current_state != enemy_states.hurt_state)
        { // ensures that you can't spam fire to kill the enemy while it's already being hurt
            base.TakeDamage(damage);
        }

        if (current_state == enemy_states.chase_state || current_state == enemy_states.idle_state)
        { // sets us into the hurt state (shit like animation or whatever) if it makes sense
            countdown_timer = hurt_time;
            Debug.Log("Entering Hurt State");
            current_state = enemy_states.hurt_state;
        }
        
    }

    private void Update()
    {
        switch (current_state)
        {
            case (enemy_states.idle_state):
                IdleBehavior();
                break;
            case (enemy_states.chase_state):
                ChaseBehavior();
                break;
            case (enemy_states.attack_state):
                AttackBehavior();
                break;
            case (enemy_states.hurt_state):
                HurtBehavior();
                break;
        }
        CheckDeath();
    }
}
