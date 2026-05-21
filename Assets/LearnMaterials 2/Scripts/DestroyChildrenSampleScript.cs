using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChildrenSampleScript : SampleScript
{
    [SerializeField]
    private Transform target;

    [SerializeField, Min(0.01f)]
    private float shrinkSpeed = 1f;

    private void Awake()
    {
        if (target == null)
        {
            target = transform;
        }
    }

    public override void Use()
    {
        if (target == null)
        {
            Debug.LogError("Target не назначен.");
            return;
        }

        StartCoroutine(DestroyChildrenCoroutine());
    }

    private IEnumerator DestroyChildrenCoroutine()
    {
        while (target.childCount > 0)
        {
            Transform child = target.GetChild(0);

            while (child != null && child.localScale.magnitude > 0.01f)
            {
                child.localScale = Vector3.MoveTowards(
                    child.localScale,
                    Vector3.zero,
                    shrinkSpeed * Time.deltaTime
                );

                yield return null;
            }

            if (child != null)
            {
                Destroy(child.gameObject);
            }

            yield return null;
        }
    }
}
