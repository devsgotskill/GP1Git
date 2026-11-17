using UnityEngine;
using UnityEngine.AI;

// Script created by: Damian Zuniga
public class TestEnemy : EnemyBase
{
    [SerializeField] private NavMeshAgent agent_reference;
    [SerializeField] private Vector3 player_position;

    private void Update()
    {
        agent_reference.SetDestination(player_position);
    }

}
