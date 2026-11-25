using Unity.VisualScripting;
using UnityEngine;

// Script created by: Damian Zuniga
public class ProjectileScript : MonoBehaviour
{
    [SerializeField] public float lifespan = 10;
    [SerializeField] public float travel_speed = 10;
    public Vector3 destination_coords;
    private Vector3 movement_vector;

    private void Start()
    {
        movement_vector = (destination_coords - transform.position).normalized * travel_speed;
    }

    private void Update()
    {
        lifespan -= Time.deltaTime;
        transform.position += movement_vector * Time.deltaTime;
        if (lifespan <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Insert logic here to administer damage
        }
        Destroy(gameObject);
    }
}
