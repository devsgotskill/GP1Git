using UnityEngine;

public class DeathPit : MonoBehaviour
{
    [SerializeField] private Transform reapwan_area;
    [SerializeField] private Transform player;

    private void OnTriggerEnter(Collider other)
    {
        CharacterController cc = other.GetComponent<CharacterController>();
        
        if (other.gameObject.layer == 6)
        {
            cc.enabled = false;
            player.transform.position = reapwan_area.transform.position;
            cc.enabled = true;
        }
    }
}
