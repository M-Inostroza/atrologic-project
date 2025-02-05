using UnityEngine;

public enum PartType
{
    Ground,
    Air,
    Sensor,
    // Add other part types as needed
}

public class Part : MonoBehaviour
{
    public PartType partType;
    public Transform attachmentPoint;
    public bool isAttached;

    protected bool isDragging = false;
    private Vector3 offset;

    public void Start()
    {
        isAttached = false;
    }

    public void Attach(Transform newAttachmentPoint)
    {
        attachmentPoint = newAttachmentPoint;
        transform.SetParent(attachmentPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        isAttached = true;
        Debug.Log($"Part {name} attached to {attachmentPoint.name}");
    }

    public void Detach()
    {
        if (attachmentPoint != null)
        {
            AttachmentPoints attachmentPoints = attachmentPoint.GetComponentInParent<AttachmentPoints>();
            if (attachmentPoints != null)
            {
                attachmentPoints.DetachPart(attachmentPoint);
            }
        }
        transform.SetParent(null);
        attachmentPoint = null;
        isAttached = false;
        Debug.Log($"Part {name} detached");
    }

    public void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }

    public void OnMouseUp()
    {
        isDragging = false;

        // Find the closest attachment point and snap to it
        AttachmentPoints attachmentPoints = FindObjectOfType<AttachmentPoints>();
        if (attachmentPoints != null)
        {
            Transform closestAttachmentPoint = attachmentPoints.GetClosestAttachmentPoint(transform.position);
            if (closestAttachmentPoint != null)
            {
                Attach(closestAttachmentPoint);
            }
        }
    }

    public void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
