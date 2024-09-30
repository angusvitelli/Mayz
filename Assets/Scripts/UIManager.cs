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
        if (canvasGroup != null)
        {
             canvasGroup.alpha = 0f;
        }
        
    }

    public IEnumerator FadeInThenOut()
    {
        yield return StartCoroutine(FadeInText());
        yield return new WaitForSeconds(3f);
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
