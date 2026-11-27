using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Scenes/TestLevelOne");
    }
    public void SettingsButton()
    {
        // Gold level settings menu if we need
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
