using System.Collections.Generic;
using UnityEngine;

public class AttachmentPoints : MonoBehaviour
{
    public Transform[] attachmentPoints;
    private Dictionary<Transform, bool> attachmentPointStatus;

    private void Awake()
    {
        attachmentPointStatus = new Dictionary<Transform, bool>();
        foreach (Transform attachmentPoint in attachmentPoints)
        {
            attachmentPointStatus[attachmentPoint] = false; // Initialize all attachment points as unoccupied
        }
    }

    public Transform GetAttachmentPoint(int index)
    {
        if (index < 0 || index >= attachmentPoints.Length) return null;
        return attachmentPoints[index];
    }

    public Transform GetClosestAttachmentPoint(Vector3 position)
    {
        Transform closestPoint = null;
        float closestDistance = float.MaxValue;

        foreach (Transform attachmentPoint in attachmentPoints)
        {
            if (!attachmentPointStatus[attachmentPoint]) // Check if the attachment point is unoccupied
            {
                float distance = Vector2.Distance(position, attachmentPoint.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = attachmentPoint;
                }
            }
        }

        return closestPoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Part part = other.GetComponent<Part>();
        if (part != null)
        {
            foreach (Transform attachmentPoint in attachmentPoints)
            {
                if (!attachmentPointStatus[attachmentPoint] && Vector2.Distance(part.transform.position, attachmentPoint.position) < 0.5f) // Adjust the threshold as needed
                {
                    part.Attach(attachmentPoint);
                    attachmentPointStatus[attachmentPoint] = true; // Mark the attachment point as occupied
                    Debug.Log($"Part {part.name} attached to {attachmentPoint.name}");
                    break;
                }
            }
        }
    }

    public void DetachPart(Transform attachmentPoint)
    {
        if (attachmentPointStatus.ContainsKey(attachmentPoint))
        {
            attachmentPointStatus[attachmentPoint] = false; // Mark the attachment point as unoccupied
            Debug.Log($"Attachment point {attachmentPoint.name} is now unoccupied");
        }
    }
}
