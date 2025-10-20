using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AnxietyTrigger : MonoBehaviour
{
    private Volume postProcessingVolume;
    private ChromaticAberration chromaticAberration;
    private Vignette Vignette;

    [SerializeField] private bool insideBox;

    [SerializeField] float speedEnter;
    [SerializeField] float speedExit;
    [SerializeField] float maxChrIntensity;
    [SerializeField] float minChrIntensity;
    [SerializeField] float maxVigIntensity;
    [SerializeField] float minVigIntensity;

    private void Awake()
    {
        postProcessingVolume = GetComponent<Volume>();
        postProcessingVolume.profile.TryGet(out chromaticAberration);
        postProcessingVolume.profile.TryGet(out Vignette);
    }

    private void Update()
    {
        if (!insideBox)
        {
            float currentIntensity = chromaticAberration.intensity.value;
            float newIntensity = Mathf.Lerp(currentIntensity, minChrIntensity, speedExit * Time.deltaTime);
            chromaticAberration.intensity.Override(newIntensity);

            float currentVigIntensity = Vignette.intensity.value;
            float newVigIntensity = Mathf.Lerp(currentVigIntensity, minVigIntensity, speedExit * Time.deltaTime);
            Vignette.intensity.Override(newVigIntensity);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        insideBox = true;
        float currentIntensity = chromaticAberration.intensity.value;
        float newIntensity = Mathf.Lerp(currentIntensity, maxChrIntensity, speedEnter * Time.deltaTime);
        chromaticAberration.intensity.Override(newIntensity);

        float currentVigIntensity = Vignette.intensity.value;
        float newVigIntensity = Mathf.Lerp(currentVigIntensity, maxVigIntensity, speedEnter * Time.deltaTime);
        Vignette.intensity.Override(newVigIntensity);
    }

    private void OnTriggerExit(Collider other)
    {
        insideBox = false;
    }
}
