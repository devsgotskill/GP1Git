using UnityEngine;

public class KeyCard : MonoBehaviour
{
    public bool keycard_acquired;

    [SerializeField] private GameObject keycard_object;
    [SerializeField] private Door door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            keycard_acquired = true;
            door.MoveDoor();
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        keycard_acquired = false;
    }
}
