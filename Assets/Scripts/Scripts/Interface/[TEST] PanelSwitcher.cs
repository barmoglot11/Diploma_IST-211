using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using BATTLE;

public class PanelSwitcher : MonoBehaviour
{
    public RectTransform panel1; // Панель 1
    public RectTransform panel2; // Панель 2
    public float transitionSpeed = 5f; // Скорость перехода

    public Image[] imagesInPanel1; // Изображения в панели 1
    public GameObject[] gameObjectsInPanel1; // GameObjects в панели 1
    public GameObject objectToActivate; 
    private Vector2 panel1TargetPosition;
    private Vector2 panel2TargetPosition;
    private Vector2 panel1TargetSize;
    private Vector2 panel2TargetSize;

    private bool isSwitching = false;
    private bool showElements = false;

    void Start()
    {
        // Инициализация целевых значений панелей
        panel1TargetPosition = panel1.anchoredPosition;
        panel2TargetPosition = panel2.anchoredPosition;
        panel1TargetSize = panel1.sizeDelta;
        panel2TargetSize = panel2.sizeDelta;
    }

    void Update()
    {
        if (isSwitching) // Если происходит переключение, обновляем позиции и размеры панелей
        {
            // Плавное перемещение и изменение размеров
            panel1.anchoredPosition = Vector2.Lerp(panel1.anchoredPosition, panel1TargetPosition, Time.deltaTime * transitionSpeed);
            panel2.anchoredPosition = Vector2.Lerp(panel2.anchoredPosition, panel2TargetPosition, Time.deltaTime * transitionSpeed);

            panel1.sizeDelta = Vector2.Lerp(panel1.sizeDelta, panel1TargetSize, Time.deltaTime * transitionSpeed);
            panel2.sizeDelta = Vector2.Lerp(panel2.sizeDelta, panel2TargetSize, Time.deltaTime * transitionSpeed);

            // Проверка завершения перехода
            if (Vector2.Distance(panel1.anchoredPosition, panel1TargetPosition) < 0.1f &&
                Vector2.Distance(panel2.anchoredPosition, panel2TargetPosition) < 0.1f &&
                Vector2.Distance(panel1.sizeDelta, panel1TargetSize) < 0.1f &&
                Vector2.Distance(panel2.sizeDelta, panel2TargetSize) < 0.1f)
            {
                isSwitching = false; // Завершение переключения
            }
        }
        else
        {
            // Обработка ввода для переключения панелей
            if (Input.GetKeyDown(KeyCode.Q)) // Клавиша "Q"
            {
                SwitchPanels(panel2.anchoredPosition, panel1.anchoredPosition, panel2.sizeDelta, panel1.sizeDelta);
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

        // Определяем, нужно ли показывать элементы
        showElements = !showElements; // Если панель 1 переходит на позицию панели 2
        Debug.Log($"Show elements: {showElements}");
        
        foreach (var image in imagesInPanel1)
        {
            StartCoroutine(FadeImage(image, showElements));
        }

        foreach (var gameObj in gameObjectsInPanel1)
        {
            StartCoroutine(FadeGameObject(gameObj, showElements));
        }
        
        // Активируем/деактивируем объект при переключении на панель 2
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(!objectToActivate.activeSelf);
            
            if(objectToActivate.activeSelf)
                objectToActivate.GetComponent<Gun>().LinkInput();
            else
                objectToActivate.GetComponent<Gun>().UnlinkInput();
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

        gameObj.SetActive(show); // Активируем или деактивируем объект в зависимости от состояния
    }
}
