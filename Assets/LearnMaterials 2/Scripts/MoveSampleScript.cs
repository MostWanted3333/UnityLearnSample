using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSampleScript : SampleScript
{
    // Start is called before the first frame update
    [SerializeField, Min(0.01f)]
    private float moveSpeed = 1f;

    [SerializeField]
    private Vector3 targetPosition;

    public override void Use()
    {
        Debug.Log("Событие получено, движение начато!");
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = targetPosition;
    }
}
