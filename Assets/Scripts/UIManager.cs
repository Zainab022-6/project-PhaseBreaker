using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject scoresScreen;
    public GameObject creditsScreen;

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        scoresScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }

    public void ShowScores()
    {
        mainMenu.SetActive(false);
        scoresScreen.SetActive(true);
        creditsScreen.SetActive(false);
    }

    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        scoresScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
