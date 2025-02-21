using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    private Vector3 dragOrigin;
    private float dragSpeed = 1f;
    private float zoomSpeed = 0.8f;

    private bool isZoomed;
    private static bool canMove;

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
        if (CanMove)
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
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!isZoomed)
            {
                if (hit.collider != null && hit.collider.CompareTag("Room"))
                {
                    string roomName = hit.collider.name;
                    // Zoom to the clicked object's position
                    Vector3 targetPosition = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, mainCamera.transform.position.z);
                    transform.DOMove(targetPosition, zoomSpeed).SetEase(Ease.OutCirc);
                    mainCamera.DOOrthoSize(2.5f, zoomSpeed);
                    isZoomed = true;

                    RoomEvents.ToggleRoom(roomName, true);
                }
            }
        }
    }

    public void ZoomOut()
    {
        Vector3 targetPosition = new Vector3(0, 0, mainCamera.transform.position.z);
        transform.DOMove(targetPosition, zoomSpeed).SetEase(Ease.OutCirc);
        mainCamera.DOOrthoSize(5f, zoomSpeed);
        isZoomed = false;
        canMove = true;
    }


    // G&S
    public static bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }
}
