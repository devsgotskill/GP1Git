using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// Script created by: Damian Zuniga
public class TestEnemy : EnemyBase
{
    [SerializeField] private Transform player_transform_reference;
    [SerializeField] private PlayerDetectionVolume player_detection_volume;
    [SerializeField] private PlayerDetectionVolume in_range_to_attack_volume;
    [SerializeField] private EnemyHitBox hit_box;
    [SerializeField] private float chase_time = 3;
    [SerializeField] private float attack_time = 1;
    private float countdown_time;
    private Vector3 position_to_attack;

    [SerializeField] private enemy_states current_state = enemy_states.idle_state;

    private void Awake()
    {
        countdown_time = chase_time;
        hit_box.enabled = false;
    }

    private void Update()
    {
        switch (current_state)
        {
            case (enemy_states.idle_state):
                if (player_detection_volume.is_detecting) // switch to chase
                {
                    Debug.Log("Entering Chase State");
                    agent_reference.isStopped = false;
                    current_state = enemy_states.chase_state;
                }

                // add a case where we can hurt the enemy
                break;
            case (enemy_states.chase_state):
                OrientTowardsPlayer();

                agent_reference.destination = player_transform_reference.position;
                agent_reference.speed = movement_speed;

                countdown_time -= Time.deltaTime;
                if (countdown_time <= 0) // switch to idle
                {
                    Debug.Log("Entering Idle State!");
                    agent_reference.isStopped = true;
                    player_detection_volume.is_detecting = false;
                    countdown_time = chase_time;
                    current_state = enemy_states.idle_state;
                }

                if (in_range_to_attack_volume.is_detecting) // switch to attack
                {
                    position_to_attack = player_transform_reference.position;
                    Debug.Log("Entering Attack State!");
                    countdown_time = attack_time;
                    current_state = enemy_states.attack_state;
                }

                // Add a case so that we can hurt the enemy
                break;
            case (enemy_states.attack_state):
                countdown_time -= Time.deltaTime;
                agent_reference.destination = position_to_attack;
                hit_box.enabled = true;

                if (countdown_time <= 0) // switch to chase
                {
                    Debug.Log("En Route to chase the player");
                    in_range_to_attack_volume.is_detecting = false;
                    hit_box.enabled = false;
                    countdown_time = chase_time;
                    current_state = enemy_states.chase_state;
                }
                break;
            case (enemy_states.hurt_state):
                // Logic that applies knockback goes here
                current_state = enemy_states.chase_state;
                break;
        }
    }
}
