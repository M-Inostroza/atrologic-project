using UnityEngine;

public enum PartType
{
    Ground,
    Air,
    Sensor,
}

public class Part : MonoBehaviour
{
    public PartType partType;
    public Transform attachmentPoint;
    public bool isAttached;
    public bool isDeployed = false;
    public string instanceID;

    protected bool isDragging = false;
    private Vector3 offset;
    

    RectTransform recycleIcon;
    Core core;

    public void Start()
    {
        core = FindFirstObjectByType<Core>();

        CheckInitialAttachment();
    }

    public void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    public void CheckInitialAttachment()
    {
        if (transform.parent != null && transform.parent.CompareTag("Point"))
        {
            isAttached = true;
        }
        else
        {
            isAttached = false;
        }
    }

    private bool IsOverRecycle()
    {
        recycleIcon = GameObject.Find("Recycle").GetComponent<RectTransform>();
        if (recycleIcon == null) return false;
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return RectTransformUtility.RectangleContainsScreenPoint(recycleIcon, screenPosition);
    }

    public void OnMouseDown()
    {
        isDragging = true;

        if (isAttached)
        {
            if (core != null)
            {
                Detach();
            }
        }
        else
        {
            offset = transform.position - GetMouseWorldPosition();
        }
    }

    public void OnMouseUp()
    {
        isDragging = false;

        if (core != null)
        {
            Transform closestAttachmentPoint = core.GetClosestAttachmentPoint(transform.position);
            if (closestAttachmentPoint != null)
            {
                float distance = Vector3.Distance(transform.position, closestAttachmentPoint.position);
                float maxSnapDistance = .5f; // Set maximum snap distance

                if (distance <= maxSnapDistance)
                {
                    Attach(closestAttachmentPoint);
                    core.SetAttachmentPointStatus(closestAttachmentPoint, true);
                }
                else
                {
                    if (IsOverRecycle())
                    {
                        Recycle();
                    }
                }
            }
        }
    }

    public void Attach(Transform newAttachmentPoint)
    {
        attachmentPoint = newAttachmentPoint;
        transform.SetParent(attachmentPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        isAttached = true;
    }

    public void Detach()
    {
        core.SetAttachmentPointStatus(transform.parent.transform, false);
        transform.SetParent(null);
        isAttached = false;
    }

    void Recycle()
    {
        isDeployed = false;

        gameObject.SetActive(false);
        gameObject.transform.position = new Vector3(12.8f, -7f, 0f);

        FindFirstObjectByType<Workshop>().PopulateGrid();

        Debug.Log($"{name} was recycled and can now be deployed again.");
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
