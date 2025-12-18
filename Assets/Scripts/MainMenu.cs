using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject creditsPanel; // Drag your CreditsPanel here in Inspector

    public void PlayGame()
    {
        // Make sure your game scene name matches exactly!
        SceneManager.LoadScene("GameScene"); 
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true); // Shows the panel
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false); // Hides the panel
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
}