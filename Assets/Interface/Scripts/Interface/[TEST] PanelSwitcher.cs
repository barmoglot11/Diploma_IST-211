using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelSwitcher : MonoBehaviour
{
    public RectTransform panel1; // Первая панель
    public RectTransform panel2; // Вторая панель
    public float transitionSpeed = 5f; // Скорость перехода

    public Image[] imagesInPanel1; // Image элементы в первой панели
    public GameObject[] gameObjectsInPanel1; // GameObjects в первой панели

    private Vector2 panel1TargetPosition;
    private Vector2 panel2TargetPosition;
    private Vector2 panel1TargetSize;
    private Vector2 panel2TargetSize;

    private bool isSwitching = false;

    void Start()
    {
        // Инициализация начальных позиций и размеров
        panel1TargetPosition = panel1.anchoredPosition;
        panel2TargetPosition = panel2.anchoredPosition;
        panel1TargetSize = panel1.sizeDelta;
        panel2TargetSize = panel2.sizeDelta;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Нажатие на "1"
        {
            SwitchPanels(panel2.anchoredPosition, panel1.anchoredPosition, panel2.sizeDelta, panel1.sizeDelta);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Нажатие на "2"
        {
            SwitchPanels(panel1.anchoredPosition, panel2.anchoredPosition, panel1.sizeDelta, panel2.sizeDelta);
        }

        if (isSwitching)
        {
            // Плавное перемещение и изменение размеров
            panel1.anchoredPosition = Vector2.Lerp(panel1.anchoredPosition, panel1TargetPosition, Time.deltaTime * transitionSpeed);
            panel2.anchoredPosition = Vector2.Lerp(panel2.anchoredPosition, panel2TargetPosition, Time.deltaTime * transitionSpeed);

            panel1.sizeDelta = Vector2.Lerp(panel1.sizeDelta, panel1TargetSize, Time.deltaTime * transitionSpeed);
            panel2.sizeDelta = Vector2.Lerp(panel2.sizeDelta, panel2TargetSize, Time.deltaTime * transitionSpeed);

            // Проверка завершения перехода
            if (Vector2.Distance(panel1.anchoredPosition, panel1TargetPosition) < 0.1f &&
                Vector2.Distance(panel2.anchoredPosition, panel2TargetPosition) < 0.1f)
            {
                isSwitching = false;
            }
        }
    }

    void SwitchPanels(Vector2 newPanel1Pos, Vector2 newPanel2Pos, Vector2 newPanel1Size, Vector2 newPanel2Size)
    {
        panel1TargetPosition = newPanel1Pos;
        panel2TargetPosition = newPanel2Pos;
        panel1TargetSize = newPanel1Size;
        panel2TargetSize = newPanel2Size;

        isSwitching = true;

        // Управление видимостью элементов в первой панели
        bool showElements = (newPanel1Pos == panel2.anchoredPosition); // Если панель 1 возвращается на своё место

        foreach (var image in imagesInPanel1)
        {
            StartCoroutine(FadeImage(image, showElements));
        }

        foreach (var gameObj in gameObjectsInPanel1)
        {
            StartCoroutine(FadeGameObject(gameObj, showElements));
        }
    }

    IEnumerator FadeImage(Image image, bool show)
    {
        float targetAlpha = show ? 1f : 0f;
        float currentAlpha = image.color.a;

        while (Mathf.Abs(currentAlpha - targetAlpha) > 0.01f)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * transitionSpeed);
            image.color = new Color(image.color.r, image.color.g, image.color.b, currentAlpha);
            yield return null;
        }
    }

    IEnumerator FadeGameObject(GameObject gameObj, bool show)
    {
        float targetAlpha = show ? 1f : 0f;
        CanvasGroup canvasGroup = gameObj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObj.AddComponent<CanvasGroup>();
        }

        float currentAlpha = canvasGroup.alpha;

        while (Mathf.Abs(currentAlpha - targetAlpha) > 0.01f)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * transitionSpeed);
            canvasGroup.alpha = currentAlpha;
            yield return null;
        }

        if (!show)
        {
            gameObj.SetActive(false);
        }
        else
        {
            gameObj.SetActive(true);
        }
    }
}