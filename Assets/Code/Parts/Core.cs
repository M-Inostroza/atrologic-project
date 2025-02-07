using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Transform[] attachmentPoints;
    private Dictionary<Transform, bool> attachmentPointStatus;

    private void Awake()
    {
        // Inicializa el diccionario para rastrear el estado de los puntos de unión
        attachmentPointStatus = new Dictionary<Transform, bool>();
        foreach (Transform attachmentPoint in attachmentPoints)
        {
            attachmentPointStatus[attachmentPoint] = false; // Inicializa todos los puntos de unión como desocupados
        }
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

        if (closestPoint != null)
        {
            attachmentPointStatus[closestPoint] = true; // Marca el punto de unión como ocupado
        }

        return closestPoint;
    }

    public void SetAttachmentPointStatus(Transform attachmentPoint, bool status)
    {
        attachmentPointStatus[attachmentPoint] = status;
    }
}
