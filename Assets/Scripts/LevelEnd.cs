using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [Header("Optional End Message")]
    public GameObject endCanvas;   // Can be NULL

    [Header("Optional Scene Transition")]
    public string nextSceneName;   // Can be EMPTY

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        // 1️⃣ Show end message (if assigned)
        if (endCanvas != null)
        {
            endCanvas.SetActive(true);
            Time.timeScale = 0f;
        }

        // 2️⃣ Load next scene (if provided)
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextSceneName);
        }
    }
}