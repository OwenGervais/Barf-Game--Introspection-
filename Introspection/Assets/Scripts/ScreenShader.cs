using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScreenShader : MonoBehaviour
{

    [SerializeField] private UniversalRendererData rendererData;

    private FullScreenPassRendererFeature fullScreenPassFeature;

    void Start()
    {
        foreach (var feature in rendererData.rendererFeatures)
        {
            if (feature is FullScreenPassRendererFeature fsFeature)
            {
                fullScreenPassFeature = fsFeature;
                break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (fullScreenPassFeature != null)
            {
                fullScreenPassFeature.SetActive(true);
            }
        }

        if (Input.GetKey(KeyCode.Alpha0))
        {
            if (fullScreenPassFeature != null)
            {
                fullScreenPassFeature.SetActive(false);
            }
        }
    }
}
