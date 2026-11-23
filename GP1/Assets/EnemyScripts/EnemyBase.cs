using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// Script created by: Damian Zuniga
public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected NavMeshAgent agent_reference;
    [SerializeField] protected float movement_speed;
    [SerializeField] private int max_health;
    [SerializeField] private int current_health;
    [SerializeField] private int attack_damage;
    [SerializeField] private float inflict_fear;

    protected enum enemy_states
    {
        idle_state = 0,
        chase_state = 1,
        attack_state = 2,
        hurt_state = 3,
    }

    private Vector3 direction;

    private void Awake()
    {
        current_health = max_health;
    }

    public void TakeDamage(int damage)
    {
        current_health -= damage;
        // Pierre: test line added just to make sure enemy is hit by raycast from weapon
        Debug.Log("Enemy damaged");
    }

    public void OrientTowardsPlayer()
    {
        Vector3 look_towards_player = (agent_reference.destination - transform.position).normalized;
        look_towards_player.y = 0;

        if (look_towards_player.sqrMagnitude > .01f) // hotfix for "the look rotation viewing vector is 0"
        {
            Quaternion rotation = Quaternion.LookRotation(look_towards_player);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        }
    }
}
