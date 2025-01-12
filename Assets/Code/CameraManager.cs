using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    private Vector3 dragOrigin;
    private float dragSpeed = 1f;
    private float zoomSpeed = 0.8f;

    private bool isZoomed;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        isZoomed = false;
    }

    void Update()
    {
        HandleMouseDrag();
        HandleMouseClick();
    }

    private void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mainCamera.transform.position += difference * dragSpeed;

            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (!isZoomed)
            {
                if (hit.collider != null)
                {
                    // Zoom to the clicked object's position
                    Vector3 targetPosition = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, mainCamera.transform.position.z);
                    transform.DOMove(targetPosition, zoomSpeed).SetEase(Ease.OutCirc);
                    mainCamera.DOOrthoSize(2.5f, zoomSpeed);
                    isZoomed = true;
                }
            } else
            {
                mainCamera.DOOrthoSize(5.5f, zoomSpeed);
                isZoomed = false;
            }
        }
    }
}
