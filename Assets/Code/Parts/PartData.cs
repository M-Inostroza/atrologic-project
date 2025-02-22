using UnityEngine;

public class PartData
{
    public string name;
    public Vector3 localPosition;
    public Quaternion localRotation;
    public bool isAttached;
    public string attachmentPointName;
    public string instanceID;
    public bool isDeployed;
    public bool isActive; // NEW: Save active state

    public PartData(Part part)
    {
        name = part.name;
        localPosition = part.transform.localPosition;
        localRotation = part.transform.localRotation;
        isAttached = part.isAttached;
        attachmentPointName = part.attachmentPoint != null ? part.attachmentPoint.name : "";
        instanceID = part.instanceID;
        isDeployed = part.isDeployed;
        isActive = part.gameObject.activeSelf; // Save active state
    }
}