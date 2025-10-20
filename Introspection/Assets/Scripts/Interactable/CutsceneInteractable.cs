using UnityEngine;

public class CutsceneInteractable : InteractableObj
{
    [SerializeField] private CutsceneManager cutsceneManager;
    [SerializeField] private ProgressTracker progressTracker;
    [SerializeField] private Cutscene cutscenePath;
    [SerializeField] private bool positivePath;
    [SerializeField] private int stageNum;

    public override void Interacted()
    {
        base.Interacted();
        
        cutsceneManager.cutscene = cutscenePath;
        cutsceneManager.StartCutscene();

        if (stageNum != 0)
        {
            progressTracker.ChangeProgress(positivePath, stageNum - 1);
        }
    }
}
