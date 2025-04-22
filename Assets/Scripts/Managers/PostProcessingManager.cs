using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class PostProcessingManager : MonoBehaviour
{
    [SerializeField] private PostProcessProfile _profile1;
    [SerializeField] private PostProcessProfile _profile2;
    [SerializeField] private PostProcessProfile _profile3;

    [SerializeField] private PostProcessVolume _postProcessVolume;
    private Coroutine _blendCoroutine;
    [SerializeField] private float duration = 2f;
    public static PostProcessingManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);


        if (_postProcessVolume == null)
        {
            _postProcessVolume = GetComponent<PostProcessVolume>();
            Debug.LogError("PostProcessVolume component missing!", this);
        }
    }

    public void SetProfileByIndex(int index)
    {
        PostProcessProfile source = GetProfileByIndex(index);
        if (source == null)
        {
            Debug.LogWarning("Invalid profile index or null profile.");
            return;
        }

        _postProcessVolume.profile = source;
    }

    public void SetProfileByIndexSmooth(int index)
    {
        PostProcessProfile targetProfile = GetProfileByIndex(index);
        if (targetProfile == null)
        {
            Debug.LogWarning("Invalid profile index or null profile.");
            return;
        }

        if (_blendCoroutine != null)
            StopCoroutine(_blendCoroutine);

        _blendCoroutine = StartCoroutine(BlendToProfile(targetProfile, duration));
    }

    private PostProcessProfile GetProfileByIndex(int index)
    {
        return index switch
        {
            0 => _profile1,
            1 => _profile2,
            2 => _profile3,
            _ => null
        };
    }

    private IEnumerator BlendToProfile(PostProcessProfile targetProfile, float duration)
    {
        if (_postProcessVolume == null || _postProcessVolume.profile == null || targetProfile == null)
            yield break;

        PostProcessProfile currentProfile = _postProcessVolume.profile;

        float time = 0f;

        currentProfile.TryGetSettings(out Bloom currentBloom);
        targetProfile.TryGetSettings(out Bloom targetBloom);

        currentProfile.TryGetSettings(out DepthOfField currentDoF);
        targetProfile.TryGetSettings(out DepthOfField targetDoF);

        currentProfile.TryGetSettings(out AmbientOcclusion currentAO);
        targetProfile.TryGetSettings(out AmbientOcclusion targetAO);

        currentProfile.TryGetSettings(out ChromaticAberration currentCA);
        targetProfile.TryGetSettings(out ChromaticAberration targetCA);

        currentProfile.TryGetSettings(out MotionBlur currentMB);
        targetProfile.TryGetSettings(out MotionBlur targetMB);

        currentProfile.TryGetSettings(out ColorGrading currentCG);
        targetProfile.TryGetSettings(out ColorGrading targetCG);

        float startBloom = currentBloom?.intensity.value ?? 0f;
        float endBloom = targetBloom?.intensity.value ?? 0f;

        float startFocus = currentDoF?.focusDistance.value ?? 10f;
        float endFocus = targetDoF?.focusDistance.value ?? 10f;

        float startAO = currentAO?.intensity.value ?? 0f;
        float endAO = targetAO?.intensity.value ?? 0f;

        float startCA = currentCA?.intensity.value ?? 0f;
        float endCA = targetCA?.intensity.value ?? 0f;

        float startMB = currentMB?.shutterAngle.value ?? 0f;
        float endMB = targetMB?.shutterAngle.value ?? 0f;

        float startHueShift = currentCG?.hueShift.value ?? 0f;
        float endHueShift = targetCG?.hueShift.value ?? 0f;

        float startSat = currentCG?.saturation.value ?? 0f;
        float endSat = targetCG?.saturation.value ?? 0f;

        float startContrast = currentCG?.contrast.value ?? 0f;
        float endContrast = targetCG?.contrast.value ?? 0f;

        Color startColorFilter = currentCG?.colorFilter.value ?? Color.white;
        Color endColorFilter = targetCG?.colorFilter.value ?? Color.white;

        float startTemp = currentCG?.temperature.value ?? 0f;
        float endTemp = targetCG?.temperature.value ?? 0f;

        float startRedRed = currentCG?.mixerRedOutRedIn ?? 0f;
        float endRedRed = targetCG?.mixerRedOutRedIn ?? 0f;

        float startRedBlue = currentCG?.mixerRedOutBlueIn ?? 0f;
        float endRedBlue = targetCG?.mixerRedOutBlueIn ?? 0f;

        float startRedGreen = currentCG?.mixerRedOutGreenIn ?? 0f;
        float endRedGreen = targetCG?.mixerRedOutGreenIn ?? 0f;

        while (time < duration)
        {
            float t = time / duration;

            if (currentBloom != null) currentBloom.intensity.value = Mathf.Lerp(startBloom, endBloom, t);
            if (currentDoF != null) currentDoF.focusDistance.value = Mathf.Lerp(startFocus, endFocus, t);
            if (currentAO != null) currentAO.intensity.value = Mathf.Lerp(startAO, endAO, t);
            if (currentCA != null) currentCA.intensity.value = Mathf.Lerp(startCA, endCA, t);
            if (currentMB != null) currentMB.shutterAngle.value = Mathf.Lerp(startMB, endMB, t);

            if (currentCG != null)
            {
                currentCG.saturation.value = Mathf.Lerp(startSat, endSat, t);
                currentCG.contrast.value = Mathf.Lerp(startContrast, endContrast, t);
                currentCG.colorFilter.value = Color.Lerp(startColorFilter, endColorFilter, t);
                currentCG.temperature.value = Mathf.Lerp(startTemp, endTemp, t);
                currentCG.mixerRedOutRedIn.value = Mathf.Lerp(startRedRed, endRedRed, t);
                currentCG.mixerRedOutBlueIn.value = Mathf.Lerp(startRedBlue, endRedBlue, t);
                currentCG.mixerRedOutGreenIn.value = Mathf.Lerp(startRedGreen, endRedGreen, t);
                currentCG.hueShift.value = Mathf.Lerp(startHueShift, endHueShift, t);
            }

            time += Time.deltaTime;
            yield return null;
        }

        _blendCoroutine = null;
    }
}