using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Transform[] attachmentPoints;
    private Dictionary<Transform, bool> attachmentPointStatus;
    public List<string> activeSensors = new List<string>();

    private void Awake()
    {
        attachmentPointStatus = new Dictionary<Transform, bool>();
        LoadAttachmentStatus();
    }

    public Transform GetClosestAttachmentPoint(Vector3 position)
    {
        Transform closestPoint = null;
        float closestDistance = float.MaxValue;

        foreach (Transform attachmentPoint in attachmentPoints)
        {
            if (attachmentPoint == null)
            {
                continue;
            }

            if (!attachmentPointStatus[attachmentPoint]) // Check if it's unoccupied
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

    void LoadAttachmentStatus()
    {
        if (ES3.KeyExists("AttachmentPointsStatus"))
        {
            var savedStatus = ES3.Load<Dictionary<string, bool>>("AttachmentPointsStatus");
            foreach (Transform attachmentPoint in attachmentPoints)
            {
                if (savedStatus.ContainsKey(attachmentPoint.name))
                {
                    attachmentPointStatus[attachmentPoint] = savedStatus[attachmentPoint.name];
                }
                else
                {
                    attachmentPointStatus[attachmentPoint] = false;
                }
            }

        }
        else
        {
            ResetPointStatus();
        }
    }

    public void SetAttachmentPointStatus(Transform attachmentPoint, bool status)
    {
        attachmentPointStatus[attachmentPoint] = status;
        SetStatusColors(attachmentPoint, status);
    }

    void SetStatusColors(Transform attachmentPoint, bool status)
    {
        SpriteRenderer spriteRenderer = attachmentPoint.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = status ? Color.red : Color.green;
        }
        else
        {
            Debug.LogWarning($"No SpriteRenderer found on {attachmentPoint.name}");
        }
    }

    public void ResetPointStatus()
    {
        foreach (Transform attachmentPoint in attachmentPoints)
        {
            attachmentPointStatus[attachmentPoint] = false;
        }
    }

    public Dictionary<string, bool> GetAttachmentPointsStatus()
    {
        Dictionary<string, bool> status = new Dictionary<string, bool>();
        foreach (var entry in attachmentPointStatus)
        {
            status[entry.Key.name] = entry.Value;
        }
        return status;
    }

    public void DebugAttachmentPointsStatus()
    {
        foreach (var entry in attachmentPointStatus)
        {
            Debug.Log($"Attachment Point: {entry.Key.name}, Status: {(entry.Value ? "Occupied" : "Free")}");
        }
    }
}
