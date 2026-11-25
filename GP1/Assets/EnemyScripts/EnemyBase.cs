using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// Script created by: Damian Zuniga
public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected NavMeshAgent agent_reference;
    [SerializeField] protected Transform player_transform_reference;
    [SerializeField] protected float movement_speed;
    [SerializeField] private int health;
    [SerializeField] protected int attack_damage;
    [SerializeField] private float inflict_fear;

    protected enum enemy_states
    {
        idle_state = 0,
        chase_state = 1,
        attack_state = 2,
        hurt_state = 3,
    }

    protected enemy_states current_state = enemy_states.idle_state;

    public void TakeDamage(int damage)
    {
        health -= damage;
        // Pierre: test line added just to make sure enemy is hit by raycast from weapon
        Debug.Log("Enemy damaged");
    }

    public void OrientTowardsPlayer()
    {
        Vector3 look_towards_player = (player_transform_reference.position - transform.position).normalized;
        look_towards_player.y = 0;

        if (look_towards_player.sqrMagnitude > .01f) // hotfix for "the look rotation viewing vector is 0"
        {
            Quaternion rotation = Quaternion.LookRotation(look_towards_player);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        }
    }

    public void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
