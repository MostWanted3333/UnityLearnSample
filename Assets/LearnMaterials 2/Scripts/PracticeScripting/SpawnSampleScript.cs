using UnityEngine;

public class SpawnSampleScript : SampleScript
{
    [Header("Spawn Settings")]

    [SerializeField]
    [Tooltip("Prefab that will be copied.")]
    private GameObject prefab;

    [SerializeField]
    [Tooltip("Number of prefab copies.")]
    [Min(1)]
    private int count = 5;

    [SerializeField]
    [Tooltip("Distance between created objects.")]
    [Min(0.1f)]
    private float step = 1f;

    [SerializeField]
    [Tooltip("Direction in which copies will be created.")]
    private Vector3 direction = Vector3.right;

    public override void Use()
    {
        if (prefab == null)
        {
            Debug.LogWarning("Prefab is not assigned.", this);
            return;
        }

        Vector3 normalizedDirection = direction.normalized;

        if (normalizedDirection == Vector3.zero)
        {
            normalizedDirection = Vector3.right;
        }

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = transform.position + normalizedDirection * step * i;
            Instantiate(prefab, spawnPosition, transform.rotation);
        }
    }
}