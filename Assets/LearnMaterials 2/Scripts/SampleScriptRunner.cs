using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScriptRunner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private SampleScript[] scripts;

    public void UseAll()
    {
        foreach (SampleScript script in scripts)
        {
            script.Use();
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
