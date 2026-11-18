using UnityEngine;

public class Key_Card : MonoBehaviour
{
    public bool keycard_acquired;

    [SerializeField] private GameObject keycard_object;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer ==  6)
        {
            keycard_acquired = true;
            Destroy(keycard_object);
        }
    }

    private void Start()
    {
        keycard_acquired = false;
    }
}
