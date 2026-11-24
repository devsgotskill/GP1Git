using UnityEngine;
using UnityEngine.InputSystem;

public class EndLevelTrigger : Door
{
    [SerializeField] private GameObject triggerbox;
    
    private void OnTriggerEnter(Collider other)
    {
        Vector3 move_pos_down = new Vector3(door.transform.position.x, new_pos, door.transform.position.z);

        if (other.gameObject.layer == 6)
        {
            MoveDoor(new_pos);
        }
    }
    
    /*Debug  
    private void Update()
    {

        if (test_trigger == true)
        {
            Vector3 move_pos = new Vector3(door.transform.position.x, 0, door.transform.position.z);
            door.transform.position = Vector3.MoveTowards(door.transform.position, move_pos, move_speed * Time.deltaTime);
        }
    }*/
    
}
