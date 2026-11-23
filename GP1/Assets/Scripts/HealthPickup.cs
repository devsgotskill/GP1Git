using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().AddHealth();
            Debug.Log("Health pickup added");
            this.gameObject.SetActive(false);
        }
    }
}
