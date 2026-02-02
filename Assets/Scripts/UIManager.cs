using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    
    public GameObject creditsScreen;

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
         
        creditsScreen.SetActive(false);
    }

    public void ShowScores()
    {
        mainMenu.SetActive(false);
        
        creditsScreen.SetActive(false);
    }

    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        
        creditsScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
