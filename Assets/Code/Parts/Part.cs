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
    }

    public void Detach()
    {
        transform.SetParent(null);
        attachmentPoint = null;
        isAttached = false;
    }
}
