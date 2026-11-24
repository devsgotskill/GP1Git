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
            MoveDoor(new_pos);   
        }
    }

    private void OnTriggerStay(Collider other)
    {
        timer -= Time.deltaTime;

        if (timer <= 0) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    }
}