using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneMenu : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Scenes/PiMaTesting");
    }

    public void SettingsButton()
    {
        throw new System.NotImplementedException();
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
