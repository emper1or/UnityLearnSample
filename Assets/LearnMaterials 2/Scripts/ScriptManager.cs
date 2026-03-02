using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    [SerializeField] private List<SampleScript> allScripts = new List<SampleScript>();

    [ContextMenu("Run All Scripts")]
    public void RunAll()
    {
        foreach (var script in allScripts)
        {
            if (script != null) script.Use();
        }
    }
}