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
    [SerializeField] public float chase_time = 5;
    [SerializeField] public float attack_time = 2;
    [SerializeField] public float hurt_time = 1;

    protected float countdown_timer;

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
            current_state = enemy_states.chase_state;
        }

        if (in_range_to_attack_volume.is_detecting)
        {
            countdown_timer = attack_time;
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
            current_state = enemy_states.idle_state;
        }

        if (in_range_to_attack_volume.is_detecting) // switch to attack
        {
            countdown_timer = attack_time;
            agent_reference.destination = in_range_to_attack_volume.point_of_collision; // Gotta figure this out to have the thing actually move in a single line
            current_state = enemy_states.attack_state;
        }
    }

    public void AttackBehavior()
    {
        countdown_timer -= Time.deltaTime;
        hit_box.enabled = true;

        if (countdown_timer <= 0) // switch to chase
        {
            in_range_to_attack_volume.is_detecting = false;
            hit_box.enabled = false;
            countdown_timer = chase_time;
            current_state = enemy_states.chase_state;
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        countdown_timer = hurt_time;
        current_state = enemy_states.hurt_state;
    }

    public void HurtBehavior() // need to figure out weird behavior with movement during this state but otherwise we kosher
    {
        countdown_timer -= Time.deltaTime;
        if (countdown_timer <= 0)
        {
            countdown_timer = chase_time;
            current_state = enemy_states.chase_state;
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
