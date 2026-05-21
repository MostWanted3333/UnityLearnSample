using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSampleScript : SampleScript
{
    [SerializeField, Min(0.01f)]
    private float rotationSpeed = 10f;

    [SerializeField]
    private Vector3 rotationAngles;

    public override void Use()
    {
        StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(rotationAngles);

        float angle = Quaternion.Angle(startRotation, targetRotation);
        float time = angle / rotationSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / time;

            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, progress);

            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
