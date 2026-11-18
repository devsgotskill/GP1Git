using UnityEngine;

public class BoxRaycastTest : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        gameObject.SetActive(false);

    }
}
