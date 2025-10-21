using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScreenShader : MonoBehaviour
{

    [SerializeField] private UniversalRendererData rendererData;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material material1;

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

    //private void Update()
    // {
    //     if (Input.GetKey(KeyCode.Alpha1))
    //     {
    //         Swap1();
    //     }

    //     if (Input.GetKey(KeyCode.Alpha0))
    //     {
    //         SwapDefault();
    //     }
    // }

    public void SwapDefault()
    {
        if (fullScreenPassFeature != null)
        {
            fullScreenPassFeature.passMaterial = defaultMaterial;
        }
    }

    public void Swap1()
    {
        if (fullScreenPassFeature != null)
        {
            fullScreenPassFeature.passMaterial = material1;
        }
    }
}
