using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [Header("UI")]
    public Canvas canvas;              // TutorialCanvas
    public GameObject panel;           // Panel (IMPORTANT)
    public Text messageText;            // MessageText
    public float messageDuration = 5f;

    private void Start()
    {
        if (canvas != null && panel != null && messageText != null)
        {
            ShowMessage(
                messageText.text,
                messageDuration
            );
        }
    }

    public void ShowMessage(string message, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(message, duration));
    }

    private IEnumerator ShowRoutine(string message, float duration)
    {
        canvas.gameObject.SetActive(true);
        panel.SetActive(true);

        messageText.text = message;
        messageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        messageText.gameObject.SetActive(false);
        panel.SetActive(false);
    }
}
