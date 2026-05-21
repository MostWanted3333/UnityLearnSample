using System.Collections.Generic;
using UnityEngine;

public class SampleScriptRunner : MonoBehaviour
{
    [Header("Scripts To Run")]

    [SerializeField]
    [Tooltip("List of scripts that will be started together.")]
    private List<SampleScript> scripts = new List<SampleScript>();

    [ContextMenu("Use All")]
    public void UseAll()
    {
        foreach (SampleScript script in scripts)
        {
            if (script != null)
            {
                script.Use();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseAll();
        }
    }
}