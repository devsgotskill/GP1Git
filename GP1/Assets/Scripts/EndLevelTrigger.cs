using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndLevelTrigger : Door
{
    [SerializeField] private GameObject triggerbox;
    [SerializeField] private float timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            MoveDoor();   
        }
    }

    private void OnTriggerStay(Collider other)
    {
        timer -= Time.deltaTime;

        if (timer <= 0) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    }
}