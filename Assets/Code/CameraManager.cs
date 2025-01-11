using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 dragOrigin; // To store the starting point of the drag
    public float dragSpeed = 2f; // Adjust this to control drag sensitivity
    public float zoomLevel = 5f; // Desired zoom level when clicking on an object
    public float zoomSpeed = 2f; // Speed of the zoom

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleMouseDrag();
        HandleMouseClick();
    }

    private void HandleMouseDrag()
    {
        // Detect mouse button down (start dragging)
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // Detect mouse movement while holding the button (dragging)
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mainCamera.transform.position += difference * dragSpeed;

            // Update drag origin to prevent abrupt movement
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                // Zoom to the clicked object's position
                Vector3 targetPosition = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, mainCamera.transform.position.z);
                StartCoroutine(SmoothZoomAndMove(targetPosition));
            }
        }
    }

    private System.Collections.IEnumerator SmoothZoomAndMove(Vector3 targetPosition)
    {
        float startSize = mainCamera.orthographicSize;
        float elapsedTime = 0f;
        float duration = 1f / zoomSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, elapsedTime / duration);
            mainCamera.orthographicSize = Mathf.Lerp(startSize, zoomLevel, elapsedTime / duration);
            yield return null;
        }

        // Snap to final position and zoom level
        mainCamera.transform.position = targetPosition;
        mainCamera.orthographicSize = zoomLevel;
    }
}
