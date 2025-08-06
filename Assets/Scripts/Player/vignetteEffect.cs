using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteEffect : MonoBehaviour
{
    private VolumeProfile volume;
    private Vignette vignette;
    private float targetIntensity = 0f;
    [SerializeField] private float transitionSpeed = 5f;
    [SerializeField] private float maxIntensity = 0.5f;

    void Start()
    {
        Volume vol = GameManager.Instance.mainCamera.GetComponentInChildren<Volume>();


        if (vol != null)
        {
            volume = vol.profile;
            volume.TryGet(out vignette);
            if (vignette != null)
            {
                vignette.intensity.overrideState = true;
                vignette.intensity.value = 0f;
            }
        }
        else
        {
            Debug.LogWarning("No Volume found in scene.");
        }
    }

    void Update()
    {
        if (vignette != null)
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, targetIntensity, Time.deltaTime * transitionSpeed);
        }
    }

    public void SetVignetteIntensity(float intensity)
    {
        intensity *= maxIntensity; // Normalize intensity to be between 0 and 1
        targetIntensity = Mathf.Clamp(intensity, 0f, maxIntensity);
    }
}
