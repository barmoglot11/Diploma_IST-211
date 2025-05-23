using System.Collections;
using UnityEngine;

public class TogglePanelOnE : MonoBehaviour
{
    [Header("Canvas Settings")]
    [Tooltip("Основной UI-элемент (панель)")]
    public CanvasGroup canvas1;  // Оригинальное название
    
    [Tooltip("Вторичный UI-элемент")]
    public CanvasGroup canvas2;  // Оригинальное название
    
    [Header("Animation Settings")]
    public float fadeDuration = 1f;

    [Header("Input Settings")]
    public InputStatus inputStatus;

    private bool isCanvas1Visible = false;
    private bool isFading = false;
    
    public bool IsDialogueController = false;

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.E) && !isFading)
    //     {
    //         ToggleCanvasesAction();
    //     }
    // }

    /// <summary>
    /// Переключает видимость UI-панелей
    /// </summary>
    public void ToggleCanvasesAction()
    {
        if (isFading) return;

        isCanvas1Visible = !isCanvas1Visible;
        StartCoroutine(TransitionCanvases());
    }

    private IEnumerator TransitionCanvases()
    {
        isFading = true;

        // Определяем параметры анимации
        var fadeOut = isCanvas1Visible ? canvas2 : canvas1;
        var fadeIn = isCanvas1Visible ? canvas1 : canvas2;

        yield return StartCoroutine(FadeCanvas(fadeOut, 0f, false));
        yield return StartCoroutine(FadeCanvas(fadeIn, 1f, true));

        UpdateCursorState();
        UpdateInputStatus();

        isFading = false;
    }

    private IEnumerator FadeCanvas(CanvasGroup canvas, float targetAlpha, bool activateAfter)
    {
        if (targetAlpha > 0) canvas.gameObject.SetActive(true);

        float startAlpha = canvas.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvas.alpha = targetAlpha;
        canvas.gameObject.SetActive(activateAfter);
    }

    private void UpdateCursorState()
    {
        Cursor.visible = isCanvas1Visible;
        Cursor.lockState = isCanvas1Visible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void UpdateInputStatus()
    {
        if(IsDialogueController) return;
        if (isCanvas1Visible)
        {
            InputManager.Instance.ChangeInputStatus(inputStatus);
        }
        else
        {
            InputManager.Instance.ReturnToPreviousStatus();
        }
    }
}
