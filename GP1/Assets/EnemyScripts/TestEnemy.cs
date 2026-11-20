using UnityEngine;
using UnityEngine.AI;

// Script created by: Damian Zuniga
public class TestEnemy : EnemyBase
{
    [SerializeField] private Transform player_transform_reference;
    [SerializeField] private BoxCollider player_detector;
    
    private enum enemy_states
    {
        idle_state = 0,
        chase_state = 1,
        attack_state = 2,
        hurt_state = 3,
    }

    private enemy_states current_state = enemy_states.idle_state;

    private void Update()
    {
        switch (current_state)
        {
            case (enemy_states.idle_state):
                break;
            case (enemy_states.chase_state):
                agent_reference.destination = player_transform_reference.position;
                agent_reference.speed = movement_speed;

                Vector3 look_towards_player = (agent_reference.destination - transform.position).normalized;
                look_towards_player.y = 0;
                if (look_towards_player.sqrMagnitude > .01f) // hotfix for "the look rotation viewing vector is 0"
                {
                    Quaternion rotation = Quaternion.LookRotation(look_towards_player);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
                }
                break;
            case (enemy_states.attack_state):
                break;
            case (enemy_states.hurt_state):
                break;
        }
    }
}
