using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [Header("UI References (assign in Inspector)")]
    public Canvas transitionCanvas;
    public GameObject panel;
    public Text messageText;

    [Header("Timing")]
    public float displayTime = 2.2f;

    private void Awake()
    {
        // Ensure UI hidden initially
        if (transitionCanvas != null)
            transitionCanvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// Show a message for displayTime seconds, then load nextScene.
    /// This is per-scene (no DontDestroyOnLoad), so place one SceneTransition in each scene.
    /// </summary>
    public void StartTransition(string message, string nextScene)
    {
        StartCoroutine(TransitionRoutine(message, nextScene));
    }

    private IEnumerator TransitionRoutine(string message, string nextScene)
    {
        if (transitionCanvas != null) transitionCanvas.gameObject.SetActive(true);
        if (panel != null) panel.SetActive(true);
        if (messageText != null)
        {
            messageText.text = message;
            messageText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(displayTime);

        // Load next scene
        SceneManager.LoadScene(nextScene);

        // give one frame, then hide (if this object still exists after load)
        yield return null;

        if (transitionCanvas != null) transitionCanvas.gameObject.SetActive(false);
        if (panel != null) panel.SetActive(false);
    }
}