using UnityEngine;

public class SupplyPickup : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().AddBattery();
            other.gameObject.GetComponent<Player>().AddAmmo();
            this.gameObject.SetActive(false);
        }
    }
}
