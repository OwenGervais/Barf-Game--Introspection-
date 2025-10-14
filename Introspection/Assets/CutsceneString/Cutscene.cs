using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Cutscene Text", menuName = "Scriptable Objects/Cutscene Text")]
public class Cutscene : ScriptableObject
{
    [TextArea(10, 20)]
    public String[] cutsceneTexts = new String[0];
}
