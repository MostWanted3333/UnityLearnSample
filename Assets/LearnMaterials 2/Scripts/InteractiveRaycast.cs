using UnityEngine;

public class InteractiveRaycast : MonoBehaviour
{
    public GameObject prefab;
    private InteractiveBox selectedBox;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleLeftClick();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick();
        }
    }

    private void HandleLeftClick()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("InteractivePlane"))
            {
                Vector3 spawnPosition = hit.point + hit.normal * 0.5f;
                Instantiate(prefab, spawnPosition, Quaternion.identity);
                selectedBox = null;
            }
            else
            {
                InteractiveBox clickedBox = hit.collider.GetComponent<InteractiveBox>();
                if (clickedBox != null)
                {
                    if (selectedBox == null)
                    {
                        selectedBox = clickedBox;
                    }
                    else if (selectedBox != clickedBox)
                    {
                        selectedBox.AddNext(clickedBox);
                        selectedBox = null;
                    }
                }
                else
                {
                    selectedBox = null;
                }
            }
        }
    }

    private void HandleRightClick()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            InteractiveBox clickedBox = hit.collider.GetComponent<InteractiveBox>();
            if (clickedBox != null)
            {
                if (selectedBox == clickedBox) selectedBox = null;
                Destroy(clickedBox.gameObject);
            }
        }
    }
}
