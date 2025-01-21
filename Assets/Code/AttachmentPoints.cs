using UnityEngine;

public class AttachmentPoints : MonoBehaviour
{
    public Transform[] attachmentPoints;

    public Transform GetAttachmentPoint(int index)
    {
        if (index < 0 || index >= attachmentPoints.Length) return null;
        return attachmentPoints[index];
    }
}
