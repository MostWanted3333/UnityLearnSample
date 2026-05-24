using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class InteractiveBox : MonoBehaviour
{
    public InteractiveBox next;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    public void AddNext(InteractiveBox box)
    {
        next = box;
    }

    private void Update()
    {
        if (next != null)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = next.transform.position;
            Vector3 direction = endPos - startPos;
            float distance = direction.magnitude;

            // Рисуем линию через LineRenderer — видна в Game окне
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);

            // Используем RaycastAll чтобы найти ВСЕ объекты на пути
            RaycastHit[] hits = Physics.RaycastAll(startPos, direction.normalized, distance);

            foreach (RaycastHit hit in hits)
            {
                // Пропускаем сам объект и объект next
                if (hit.collider.gameObject == gameObject) continue;
                if (hit.collider.gameObject == next.gameObject) continue;

                ObstacleItem obstacle = hit.collider.GetComponent<ObstacleItem>();
                if (obstacle != null)
                {
                    obstacle.GetDamage(Time.deltaTime);
                }
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
