using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Renderer))]
public class ObstacleItem : MonoBehaviour
{
    [Range(0f, 1f)]
    public float currentValue = 1f;
    public UnityEvent onDestroyObstacle;

    private Material objectMaterial;

    private void Start()
    {
        objectMaterial = GetComponent<Renderer>().material;
        UpdateColor();
    }

    public void GetDamage(float value)
    {
        currentValue -= value;
        UpdateColor();

        if (currentValue <= 0f)
        {
            onDestroyObstacle?.Invoke();
            Destroy(gameObject);
        }
    }

    private void UpdateColor()
    {
        if (objectMaterial != null)
        {
            objectMaterial.color = Color.Lerp(Color.red, Color.white, currentValue);
        }
    }
}
