using System.Runtime.CompilerServices;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] protected GameObject door;
    [SerializeField] private KeyCard keycard;
    
    protected float move_speed = 1;
    
    [Header("Modify")]
    [SerializeField] protected float new_pos;

    //Playing state function
    public void MoveDoor(float newpos)
    {
        Vector3 move_pos = new Vector3(door.transform.position.x, newpos, door.transform.position.z);
        
        door.transform.position = Vector3.MoveTowards(door.transform.position, move_pos, move_speed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 && keycard.GetComponent<KeyCard>().keycard_acquired == true) 
        {
            MoveDoor(new_pos);
        } 
    }
}
