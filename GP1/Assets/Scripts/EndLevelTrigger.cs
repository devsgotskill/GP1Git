using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndLevelTrigger : Door
{
    [SerializeField] private GameObject triggerbox;
    [SerializeField] private float timer;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 move_pos_down = new Vector3(door.transform.position.x, new_pos, door.transform.position.z);

        if (other.gameObject.layer == 6)
        {
            CountDownBeforeLevelSwitch();
            MoveDoor(new_pos);
            
        }
    }
    
    //Might be needed for state machine
    public void CountDownBeforeLevelSwitch()
    {
        timer -= Time.deltaTime; 

        if (timer <= 0) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    }

    /*Debug*/  
    private void Update()
    {

        timer -= Time.deltaTime;

        if (timer <= 0) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    }
    
}
