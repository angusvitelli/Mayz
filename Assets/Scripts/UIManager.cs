using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public float messageDuration = 3f;
    public float fadeDuration = 1f;
    public CanvasGroup canvasGroup;

    void Start()
    {
        // Initialize text with empty or default message
        if (canvasGroup != null)
        {
             canvasGroup.alpha = 0f;
        }
        
    }

    public IEnumerator FadeInThenOut()
    {
        yield return StartCoroutine(FadeInText());
        // Wait for the fade-in to complete before starting the fade-out
        yield return new WaitForSeconds(3f); // Optional delay between fade-in and fade-out
        yield return StartCoroutine(FadeOutText());
    }

    public void UpdateInfoText(string message)
    {
        if (infoText != null)
        {
            infoText.text = message;
            StartCoroutine(FadeInThenOut());
        }
        else
        {
            Debug.LogError("UIManager infoText reference is not set.");
        }
    }

    public void UpdateInfoTextForToggle(string message)
    {
        if (infoText != null)
        {
            infoText.text = message;
            StartCoroutine(FadeInThenOut());
            StartCoroutine(HideUIManagerAfterDelay(2f));
        }
        else
        {
            Debug.LogError("UIManager infoText reference is not set.");
        }
    }

    private IEnumerator FadeInText()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }

    private IEnumerator HideUIManagerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        infoText.gameObject.SetActive(false);
    }

}
