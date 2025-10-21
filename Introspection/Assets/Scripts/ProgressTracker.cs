using UnityEditor.SceneManagement;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    public static int totalProgress; //-5 to +5

    public static bool[] stageProgress = new bool[] { false, false}; //false = fail, true = pass

    public void ChangeProgress(bool progress, int stage)
    {
        if (progress)
        {
            totalProgress++;
            stageProgress[stage] = progress;
        }
        else
        {
            totalProgress--;
        }

        Debug.Log(totalProgress);
        Debug.Log(stageProgress[stage]);
    }
}