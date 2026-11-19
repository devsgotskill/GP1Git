using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private KeyCard keycard;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 && keycard.GetComponent<KeyCard>().keycard_acquired == true) 
        { 
                Destroy(door);
        } 
    }
}
