using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;

    [Header("UI (Optional)")]
    public Canvas transitionCanvas;    // assign optional canvas with panel + text
    public GameObject panel;           // assign panel child (will be enabled/disabled)
    public Text messageText;           // assign the legacy text component inside panel

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // keep across scenes if you want (optional)
        DontDestroyOnLoad(gameObject);

        // start hidden
        if (transitionCanvas != null) transitionCanvas.gameObject.SetActive(false);
        if (panel != null) panel.SetActive(false);
    }

    // public API to start a transition
    // message: text to show, delay: seconds to wait before loading scene
    public void StartTransition(string nextSceneName, string message, float delaySeconds)
    {
        StartCoroutine(TransitionCoroutine(nextSceneName, message, delaySeconds));
    }

    private IEnumerator TransitionCoroutine(string nextSceneName, string message, float delaySeconds)
    {
        if (transitionCanvas != null) transitionCanvas.gameObject.SetActive(true);
        if (panel != null) panel.SetActive(true);
        if (messageText != null) messageText.text = message;

        // small safety wait so player sees message
        yield return new WaitForSeconds(delaySeconds);

        // Load next scene
        SceneManager.LoadScene(nextSceneName);
    }
}