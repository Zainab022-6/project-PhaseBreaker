using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Tooltip("Name of first level scene (Prologue)")]
    public string startSceneName = "Prologue";

    public void StartGame()
    {
        SceneManager.LoadScene(startSceneName);
    }

    public void LoadCredits(string creditsSceneName)
    {
        SceneManager.LoadScene(creditsSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}