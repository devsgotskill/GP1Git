using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// Script created by: Damian Zuniga
public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent_reference;
    [SerializeField] protected float movement_speed;
    [SerializeField] private int max_health;
    [SerializeField] private int current_health;
    [SerializeField] private int attack_damage;
    [SerializeField] private float inflict_fear;
    private Vector3 direction;

    private void Awake()
    {
        current_health = max_health;
    }

    public virtual void TakeDamage(int damage_value)
    {
        current_health -= damage_value;
    }
}
