using Unity.VisualScripting;
using UnityEngine;

public class Part : MonoBehaviour
{
    public Transform attachmentPoint;
    public bool isAttached;
    string prefabID;

    protected bool isDragging = false;
    private Vector3 offset;

    InventoryManager inventoryManager;
    RectTransform recycleIcon;
    Core core;

    public void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();
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

        Debug.Log("This is the prefab id: " + prefabID);

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

        PartCard partCard = FindPartCardByID(prefabID);
        if (partCard != null)
        {
            partCard.AttachToRobot(attachmentPoint);
        }
    }

    public void Detach()
    {
        core.SetAttachmentPointStatus(transform.parent.transform, false);
        transform.SetParent(null);
        isAttached = false;

        PartCard partCard = FindPartCardByID(prefabID);
        if (partCard != null)
        {
            partCard.DetachFromRobot();
        }
    }

    void Recycle()
    {
        Debug.Log($"{name} was recycled and can now be deployed again.");

        PartCard partCard = FindPartCardByID(prefabID);

        if (partCard != null)
        {
            partCard.Undeploy();
        }

        Destroy(gameObject);
    }

    private PartCard FindPartCardByID(string targetID)
    {
        if (inventoryManager == null)
        {
            Debug.LogError("Inventory Manager is null!");
            return null;
        }

        foreach (Transform child in inventoryManager.transform)
        {
            PartCard partCard = child.GetComponent<PartCard>();

            if (partCard != null && partCard.GetIDFromCard() == targetID)
            {
                Debug.Log($"PartCard with ID {targetID} found.");
                return partCard;
            }
        }

        Debug.LogWarning($"PartCard with ID {targetID} not found.");
        return null;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void SetPrefabID(string id)
    {
        prefabID = id;
    }

    public string GetPrefabID()
    {
        return prefabID;
    }

    public void DestroyRougue()
    {
        PartCard partCard = FindPartCardByID(prefabID);

        if (partCard != null)
        {
            partCard.Undeploy();
        }
        if (!isAttached)
        {
            Destroy(gameObject);
        }
    }
}
