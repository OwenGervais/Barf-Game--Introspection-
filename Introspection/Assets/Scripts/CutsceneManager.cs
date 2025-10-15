using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private string nextScene;
    private AsyncOperation asyncOperation;

    private bool inCutscene = false;
    private bool loadingText = false;
    private bool transition = false;
    private int index = 0;

    [SerializeField] private GameObject player;
    private PlayerMove playerMove;
    private PlayerLook playerLook;

    [SerializeField] private Cutscene cutscene;
    [SerializeField] private float textSpeed;
    private Coroutine currentCoroutine = null;

    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private GameObject panel;
    private Animator panelAnimator, textAnimator;

    private void Awake()
    {
        playerMove = player.GetComponent<PlayerMove>();
        playerLook = player.GetComponentInChildren<PlayerLook>();

        panelAnimator = panel.GetComponent<Animator>();
        textAnimator = displayText.GetComponent<Animator>();
    }

    private void Update() //test features
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCutscene();
        }
    }

    public void StartCutscene()
    {
        if (!inCutscene)
        {
            playerMove.locked = true;
            playerLook.locked = true;

            inCutscene = true;
            loadingText = true;

            panel.SetActive(true);
            panelAnimator.SetTrigger("fadeIn");

            asyncOperation = SceneManager.LoadSceneAsync(nextScene);
            asyncOperation.allowSceneActivation = false;

            Invoke(nameof(StartText), 1f);
        }
    }

    private void StartText()
    {
        displayText.gameObject.SetActive(true);
        textAnimator.SetTrigger("fadeIn");

        currentCoroutine = StartCoroutine(LoadText());
    }

    private IEnumerator LoadText()
    {
        loadingText = true;

        String text = cutscene.cutsceneTexts[index];

        for (int i = 0; i <= text.Length; i++)
        {
            displayText.text = text.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
            Debug.Log(asyncOperation.progress + "% scene loaded");
        }

        loadingText = false;
    }

    private void LateUpdate()
    {
        if (inCutscene)
        {
            if (Input.GetMouseButtonDown(0) && !transition)
            {
                transition = true;
                StartCoroutine(NextText());
            }
        }
    }

    private IEnumerator NextText()
    {
        if (loadingText)
        {
            StopCoroutine(currentCoroutine);
            displayText.text = cutscene.cutsceneTexts[index];
            loadingText = false;
            yield return null;
        }
        else
        {
            index++;
            if (index >= cutscene.cutsceneTexts.Length)
            {
                textAnimator.SetTrigger("fadeOut");

                yield return new WaitForSeconds(0.5f);

                inCutscene = false;
                asyncOperation.allowSceneActivation = true;
            }
            else
            {
                currentCoroutine = StartCoroutine(LoadText());
            }
        }

        transition = false;
        yield return null;
    }
}
