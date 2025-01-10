using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 dragOrigin; // To store the starting point of the drag
    public float dragSpeed = 2f; // Adjust this to control drag sensitivity

    void Update()
    {
        HandleMouseDrag();
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
            Camera.main.transform.position += difference * dragSpeed;

            // Update drag origin to prevent abrupt movement
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
