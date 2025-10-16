using System;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    
    [SerializeField] private CutsceneManager cutsceneManager;
    [SerializeField] private ProgressTracker progressTracker;
    [SerializeField] private Cutscene cutscenePath;
    [SerializeField] private bool positivePath;
    [SerializeField] private int stageNum;
    [SerializeField] private Material unSelected;
    [SerializeField] private Material selected;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Selected()
    {
        meshRenderer.material = selected;
    }

    private void Unselected()
    {
        meshRenderer.material = unSelected;
    }

    private void Interacted()
    {
        cutsceneManager.cutscene = cutscenePath;
        cutsceneManager.StartCutscene();

        progressTracker.ChangeProgress(positivePath, stageNum - 1);
    }
}
