using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StalkerEnemy : EnemyBase
{
    /*
     Modifications for the behavior:
    - when it catches sight of the player
    - Bum rush in a straight line for a set bit of time
    - cooldown for a bit
    - reenter the chase scene and track you down
    - the behavior that is in place CURRENTLY is to just get a general sense of it working, it is to be replaced VERY soon
     */
    [SerializeField] private float wander_time;
    [SerializeField] private float attack_time;
    [SerializeField] private float chase_time;
    [SerializeField] private float hurt_time;
    [SerializeField] private float wander_radius;
    [SerializeField] private DetectionVolume player_detection_volume;
    [SerializeField] private DetectionVolume in_range_to_attack_volume;
    private void Awake()
    {
        current_state = enemy_states.wander_state;
        countdown_timer = wander_time;
    }

    private void WanderBehavior()
    {
        agent_reference.speed = movement_speed;
        countdown_timer -= Time.deltaTime;

        if (countdown_timer <= 0)
        {
            Vector3 next_position = SetWanderDestination(transform.position, wander_radius, -1);
            agent_reference.SetDestination(next_position);
            countdown_timer = wander_time;
        }

        if (player_detection_volume.is_detecting)
        {
            current_state = enemy_states.chase_state;
        }

        if (in_range_to_attack_volume.is_detecting)
        {
            current_state = enemy_states.attack_state;
        }
    }

    private static Vector3 SetWanderDestination(Vector3 origin, float distance, int layermask) // Needs a fuckton of refinement, but a good start for now!
    {
        Vector3 random_direction = UnityEngine.Random.insideUnitSphere * distance;

        random_direction += origin;

        NavMeshHit nav_hit;

        NavMesh.SamplePosition(random_direction, out nav_hit, distance, layermask);

        return nav_hit.position;
    }

    private void ChaseBehavior()
    {
        OrientTowardsPlayer();
        agent_reference.SetDestination(player_transform_reference.position);
        agent_reference.speed = movement_speed * 2;

        countdown_timer -= Time.deltaTime;
        if (countdown_timer <= 0) // switch to wander
        {
            player_detection_volume.is_detecting = false;
            countdown_timer = wander_time;
            Debug.Log("Entering Wander State");
            current_state = enemy_states.wander_state;
        }

        if (in_range_to_attack_volume.is_detecting) // switch to attack
        {
            countdown_timer = attack_time;
            Debug.Log("Entering Attack State");
            current_state = enemy_states.attack_state;
        }
    }

    private void AttackBehavior()
    {
        in_range_to_attack_volume.is_detecting = false;
        countdown_timer = chase_time;
        current_state = enemy_states.chase_state; // Need to add logic here, just for testing purposes
    }

    private void HurtBehavior()
    {

    }

    private void Update()
    {
        switch (current_state)
        {
            case (enemy_states.wander_state):
                WanderBehavior();
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
