using UnityEngine;

// Script created by: Damian Zuniga
public class EnemyHitBox : MonoBehaviour
{
    private int damage_amount;

    public void Initialise(int the_damage_amount)
    {
        damage_amount = the_damage_amount;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage_amount);
        }
    }
}
