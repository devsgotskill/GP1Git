using UnityEngine;

public class DeathPit : MonoBehaviour
{
    [SerializeField] private Transform reapwan_area;
    [SerializeField] private Transform player_trans;

    [Header("Modify")]
    [SerializeField]private int damage_taken;
    
    private void OnTriggerEnter(Collider other)
    {
        CharacterController cc = other.GetComponent<CharacterController>();
        
        if (other.gameObject.layer == 6)
        {
            other.GetComponent<Player>().TakeDamage(damage_taken);
            cc.enabled = false;
            player_trans.transform.position = reapwan_area.transform.position;
            cc.enabled = true;
        }
    }
}
