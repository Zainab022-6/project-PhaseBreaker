using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [Header("Transition")]
    public string nextSceneName;
    [TextArea] public string transitionMessage;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        // Find a SceneTransition component in the current scene (per-scene)
        SceneTransition sceneTransition = FindObjectOfType<SceneTransition>();
        if (sceneTransition != null)
        {
            sceneTransition.StartTransition(transitionMessage, nextSceneName);
        }
        else
        {
            Debug.LogError("LevelEnd: No SceneTransition component found in the scene. " +
                           "Create a GameObject with the SceneTransition script and assign the UI elements.");
        }
    }
}