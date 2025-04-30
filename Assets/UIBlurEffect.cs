using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class UIBlurEffect : MonoBehaviour
{
    [SerializeField] private Camera backgroundCamera;
    [Range(0, 10)] public float blurAmount = 3f;
    [Range(0.1f, 1f)] public float resolutionScale = 0.5f;
    
    private RenderTexture renderTexture;
    private Material blurMaterial;
    private RawImage rawImage;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
        
        // Создаем материал для размытия
        blurMaterial = new Material(Shader.Find("UI/Blur"));
        blurMaterial.hideFlags = HideFlags.HideAndDontSave;
        
        // Создаем RenderTexture
        CreateRenderTexture();
    }

    private void CreateRenderTexture()
    {
        if (renderTexture != null)
        {
            renderTexture.Release();
        }
        
        int width = (int)(Screen.width * resolutionScale);
        int height = (int)(Screen.height * resolutionScale);
        
        renderTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32)
        {
            antiAliasing = 1,
            filterMode = FilterMode.Bilinear
        };
        
        if (backgroundCamera != null)
        {
            backgroundCamera.targetTexture = renderTexture;
        }
        
        rawImage.texture = renderTexture;
    }

    private void Update()
    {
        if (blurMaterial != null)
        {
            blurMaterial.SetFloat("_BlurAmount", blurAmount);
        }
        
        // Применяем материал размытия к RawImage
        rawImage.material = blurMaterial;
    }
    private void OnDisable()
    {
        if (renderTexture != null)
        {
            backgroundCamera.targetTexture = null;
            renderTexture.Release();
            Destroy(renderTexture);
        }
        
        if (blurMaterial != null)
        {
            Destroy(blurMaterial);
        }
    }
}