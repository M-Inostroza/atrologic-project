using UnityEngine;

public class AttachmentPoints : MonoBehaviour
{
    public Transform[] attachmentPoints;

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
            float distance = Vector2.Distance(position, attachmentPoint.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = attachmentPoint;
            }
        }

        return closestPoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Wheel wheel = other.GetComponent<Wheel>();
        if (wheel != null)
        {
            foreach (Transform attachmentPoint in attachmentPoints)
            {
                if (Vector2.Distance(wheel.transform.position, attachmentPoint.position) < 0.5f) // Ajusta el umbral según sea necesario
                {
                    wheel.Attach(attachmentPoint);
                    break;
                }
            }
        }
    }
}
