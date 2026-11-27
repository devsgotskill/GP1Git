using System.Runtime.CompilerServices;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] protected GameObject door;
    [SerializeField] private KeyCard keycard;
    
    protected float move_speed = 1;

    private Vector3 old_pos;
    private Vector3 move_pos;
    private bool door_moving;

    public void Start()
    {
        door_moving = false;
    }

    //Playing state function
    public void MoveDoor()
    {
        old_pos = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z);
        move_pos = new Vector3(door.transform.position.x, door.transform.position.y - 6, door.transform.position.z);
        door_moving = true;
    }

    public void Update()
    {
        if (door_moving)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, move_pos, 1 * Time.deltaTime);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player")) 
        //{
        //    Debug.Log("Reached");
        //    MoveDoor();
        //} 
    }
}
