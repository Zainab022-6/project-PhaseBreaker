using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [Tooltip("If you want a short message between levels, set this to the next scene name to load after message.")]
    public string nextSceneName = "Level_Layout";

    [Tooltip("If you use SceneTransition, set this message and delay. If empty, loads directly.")]
    public string transitionMessage = "System updated.";
    public float transitionDelay = 1.5f; // seconds shown before loading next scene

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // If SceneTransition exists and message is not empty, show message and load after delay
        if (!string.IsNullOrEmpty(transitionMessage) && SceneTransition.Instance != null)
        {
            SceneTransition.Instance.StartTransition(nextSceneName, transitionMessage, transitionDelay);
        }
        else
        {
            // direct load (fallback)
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }
    }
}