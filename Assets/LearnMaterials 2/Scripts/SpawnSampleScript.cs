using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSampleScript : SampleScript
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject prefab;

    [SerializeField, Min(1)]
    private int count = 3;

    [SerializeField, Min(0f)]
    private float step = 2f;

    public override void Use()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = transform.position + transform.forward * step * i;
            Instantiate(prefab, spawnPosition, transform.rotation);
        }
    }
}
