using UnityEngine;

// Script created by: Damian Zuniga
public class PlayerDetectionVolume : MonoBehaviour
{
    public bool is_detecting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            is_detecting = true;
        }
    }
}
