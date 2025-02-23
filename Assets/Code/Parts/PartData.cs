using UnityEngine;

public class PartData
{
    public string name;
    public Vector3 localPosition;
    public Quaternion localRotation;
    public bool isAttached;
    public string attachmentPointName;
    public string instanceID;
    public bool isDeployed; // Tracks if the part is currently attached
    public bool isActive; // Tracks if part is active in the scene

    public PartData(Part part)
    {
        name = part.name;
        localPosition = part.transform.localPosition;
        localRotation = part.transform.localRotation;
        isAttached = part.isAttached;
        attachmentPointName = part.attachmentPoint != null ? part.attachmentPoint.name : "";
        instanceID = part.instanceID;
        isDeployed = part.isDeployed; // Important for tracking deployment state
        isActive = part.gameObject.activeSelf;
    }
}
