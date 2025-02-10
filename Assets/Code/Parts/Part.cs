using System.Transactions;
using UnityEngine;

public enum PartType
{
    Ground,
    Air,
    Sensor,
    // Add other part types as needed
}

public class PartData
{
    public string name;
    public Vector3 localPosition;
    public Quaternion localRotation;
    public bool isAttached;
    public string attachmentPointName;
}

public class Part : MonoBehaviour
{
    public PartType partType;
    public Transform attachmentPoint;
    public bool isAttached;

    Core core;

    protected bool isDragging = false;
    private Vector3 offset;

    RectTransform recycleIcon;

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

    private bool IsOverUIElement()
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
                    if (IsOverUIElement())
                    {
                        Destroy(gameObject);
                    }
                    Debug.Log("Dropped.");
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
        Debug.Log("is detaching");
        core.SetAttachmentPointStatus(transform.parent.transform, false);
        transform.SetParent(null);
        isAttached = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
