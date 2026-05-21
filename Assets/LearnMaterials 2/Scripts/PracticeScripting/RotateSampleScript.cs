using System.Collections;
using UnityEngine;

public class RotateSampleScript : SampleScript
{
    [Header("Rotate Settings")]

    [SerializeField]
    [Tooltip("Angle by which the object should rotate around each axis.")]
    private Vector3 rotationAngles = new Vector3(0f, 90f, 0f);

    [SerializeField]
    [Tooltip("Rotation speed in degrees per second.")]
    [Min(0.1f)]
    private float rotationSpeed = 10f;

    private Coroutine rotateCoroutine;

    public override void Use()
    {
        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }

        rotateCoroutine = StartCoroutine(RotateObject());
    }

    private IEnumerator RotateObject()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(rotationAngles);

        float angle = Quaternion.Angle(startRotation, targetRotation);
        float duration = angle / rotationSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null;
        }

        transform.rotation = targetRotation;
        rotateCoroutine = null;
    }
}