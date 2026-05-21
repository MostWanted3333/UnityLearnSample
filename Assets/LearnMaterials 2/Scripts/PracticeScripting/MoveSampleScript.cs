using System.Collections;
using UnityEngine;

public class MoveSampleScript : SampleScript
{
    [Header("Move Settings")]

    [SerializeField]
    [Tooltip("Point where the object should move.")]
    private Transform targetPoint;

    [SerializeField]
    [Tooltip("Movement speed in units per second.")]
    [Min(0.1f)]
    private float moveSpeed = 1f;

    private Coroutine moveCoroutine;

    public override void Use()
    {
        if (targetPoint == null)
        {
            Debug.LogWarning("Target point is not assigned.", this);
            return;
        }

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(transform.position, targetPoint.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPoint.position,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = targetPoint.position;
        moveCoroutine = null;
    }
}