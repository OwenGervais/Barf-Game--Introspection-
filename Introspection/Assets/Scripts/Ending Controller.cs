using System;
using UnityEngine;

public class EndingController : MonoBehaviour
{
    [SerializeField] private CutsceneManager cutsceneManager;
    private Animator animator;
    [SerializeField] private Cutscene goodEnding;
    [SerializeField] private Cutscene neutralEnding;
    [SerializeField] private Cutscene badEnding;

    private bool bad = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (ProgressTracker.totalProgress >= 2) //good ending
        {
            Debug.Log("Good Ending Triggered");
            cutsceneManager.cutscene = goodEnding;
        }
        else if (ProgressTracker.totalProgress < 0) //bad ending
        {
            Debug.Log("Bad Ending Triggered");
            animator.SetTrigger("Bad");
            bad = true;
            Invoke("playBadEnding", 15f);
        }
        else //neutral ending
        {
            Debug.Log("Neutral Ending Triggered");
            animator.SetTrigger("Neutral");
            cutsceneManager.cutscene = neutralEnding;
        }
    }

    private void playBadEnding()
    {
        cutsceneManager.cutscene = badEnding;
        cutsceneManager.StartCutscene();
    }

    public void Selected()
    {
        //nothing here cause im lazy to change the message system
    }

    public void Unselected()
    {
        //nothing here cause im lazy to change the message system
    }

    public void Interacted()
    {
        if (bad) return;
        cutsceneManager.StartCutscene();
    }
}
