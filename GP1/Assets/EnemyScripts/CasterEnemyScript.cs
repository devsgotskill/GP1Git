using System.Runtime.CompilerServices;
using UnityEngine;

// Script created by: Damian Zuniga
public class CasterEnemy : ShamblerEnemy
{
    public GameObject cast_projectile_prefab;

    private void Awake()
    {
        hit_box = null;
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
    }

    public new void AttackBehavior()
    {
        agent_reference.velocity = new Vector3(0, 0, 0);
        countdown_timer -= Time.deltaTime;
        if (countdown_timer <= 0)
        {
            GameObject new_projectile = Instantiate(cast_projectile_prefab, transform.position, Quaternion.identity);
            new_projectile.GetComponent<ProjectileScript>().destination_coords = player_transform_reference.position;
            in_range_to_attack_volume.is_detecting = false;
            countdown_timer = chase_time;
            current_state = enemy_states.chase_state;
        }
    }


}
