using System.Collections;
using UnityEngine;

public class DestroyChildrenSampleScript : SampleScript
{
    [Header("Destroy Children Settings")]

    [SerializeField]
    [Tooltip("Object whose children will be destroyed.")]
    private Transform target;

    [SerializeField]
    [Tooltip("Time during which children shrink before destruction.")]
    [Range(0.1f, 5f)]
    private float shrinkDuration = 1f;

    private Coroutine destroyCoroutine;

    public override void Use()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned.", this);
            return;
        }

        if (destroyCoroutine != null)
        {
            StopCoroutine(destroyCoroutine);
        }

        destroyCoroutine = StartCoroutine(ShrinkAndDestroyChildren());
    }

    private IEnumerator ShrinkAndDestroyChildren()
    {
        int childCount = target.childCount;

        Transform[] children = new Transform[childCount];
        Vector3[] startScales = new Vector3[childCount];

        for (int i = 0; i < childCount; i++)
        {
            children[i] = target.GetChild(i);
            startScales[i] = children[i].localScale;
        }

        float elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / shrinkDuration;

            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] != null)
                {
                    children[i].localScale = Vector3.Lerp(startScales[i], Vector3.zero, t);
                }
            }

            yield return null;
        }

        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] != null)
            {
                Destroy(children[i].gameObject);
            }
        }

        destroyCoroutine = null;
    }
}