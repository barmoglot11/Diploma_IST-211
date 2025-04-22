using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ButtonImageToggle : MonoBehaviour
{
    [Header("Settings")] 
    public Image appearImage; // Изображение, которое будет появляться
    public List<Image> disappearImages; // Массив изображений, которые будут исчезать
    public float animationSpeed = 1f; // Скорость анимации

    // private void Start()
    // {
    //     // Инициализация изображений
    //     if (appearImage != null)
    //     {
    //         appearImage.gameObject.SetActive(false);
    //     }

    //     foreach (var image in disappearImages)
    //     {
    //         if (image != null)
    //         {
    //             image.gameObject.SetActive(false);
    //         }
    //     }
    // }

    public void OnButtonClick()
    {
        // Запуск анимации появления и исчезновения изображений
        StartCoroutine(AnimateImages());
    }

    private IEnumerator AnimateImages()
    {
        if(appearImage == null) yield break;
        // Исчезновение изображений
        foreach (var image in disappearImages)
        {
            if (image != null && image.gameObject.activeSelf)
            {
                StartCoroutine(ScaleDown(image));
            }
        }

        // Появление нового изображения
        if (appearImage != null)
        {
            appearImage.gameObject.SetActive(true);
            StartCoroutine(ScaleUp(appearImage));
        }

        yield return null;
    }

    // Увеличение изображения (появление)
    private IEnumerator ScaleUp(Image image)
    {
        float elapsed = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        image.transform.localScale = startScale;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * animationSpeed;
            image.transform.localScale = Vector3.Lerp(startScale, endScale, elapsed);
            yield return null;
        }

        image.transform.localScale = endScale;
    }

    // Уменьшение изображения (исчезновение)
    private IEnumerator ScaleDown(Image image)
    {
        float elapsed = 0f;
        Vector3 startScale = image.transform.localScale;
        Vector3 endScale = Vector3.zero;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * animationSpeed;
            image.transform.localScale = Vector3.Lerp(startScale, endScale, elapsed);
            yield return null;
        }

        image.transform.localScale = endScale;
        image.gameObject.SetActive(false);
    }
}
