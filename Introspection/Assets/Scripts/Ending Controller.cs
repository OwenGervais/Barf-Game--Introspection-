using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EndingController : MonoBehaviour
{
    [SerializeField] private CutsceneManager cutsceneManager;
    private Animator animator;
    [SerializeField] private Cutscene goodEnding;
    [SerializeField] private Cutscene neutralEnding;
    [SerializeField] private Cutscene badEnding;
    [SerializeField] private GameObject goodLUT;
    [SerializeField] private GameObject neutralLUT;
    [SerializeField] private GameObject badLUT;
    private GameObject currentLUT;
    private Volume volume;

    private bool bad = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (ProgressTracker.totalProgress >= 2) //good ending
        {
            Debug.Log("Good Ending Triggered");
            cutsceneManager.cutscene = goodEnding;
            StartCoroutine(LUTTransition(goodLUT));
        }
        else if (ProgressTracker.totalProgress < 0) //bad ending
        {
            Debug.Log("Bad Ending Triggered");
            animator.SetTrigger("Bad");
            bad = true;
            StartCoroutine(LUTTransition(badLUT));
            Invoke("playBadEnding", 15f);
        }
        else //neutral ending
        {
            Debug.Log("Neutral Ending Triggered");
            animator.SetTrigger("Neutral");
            StartCoroutine(LUTTransition(neutralLUT));
            cutsceneManager.cutscene = neutralEnding;
        }

        ProgressTracker.totalProgress = 0; //reset for next playthrough
    }

    private IEnumerator LUTTransition(GameObject LUT)
    {
        currentLUT = Instantiate(LUT);
        float timer = 0f;
        float duration = 15f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = timer / duration;

            if (currentLUT != null)
            {
                volume = currentLUT.GetComponent<Volume>();
                if (volume.profile.TryGet(out ColorLookup colorLookup))
                {
                    var cont = colorLookup;
                    cont.contribution.value = alpha;
                    Debug.Log(alpha);
                }
            }

            yield return null;
        }

        yield return null;
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
