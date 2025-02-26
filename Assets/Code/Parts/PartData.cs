using UnityEngine;

[System.Serializable]
public class PartData
{
    public string partName;
    public bool isDeployed;
    public bool isActive;
    public Vector3 localPosition;
    public Quaternion localRotation;
    public string attachmentPointName;
    public string partID;

    // Constructor for new parts
    public PartData(string name)
    {
        partName = name;
        isDeployed = false;
        isActive = false;
        localPosition = Vector3.zero;
        localRotation = Quaternion.identity;
        attachmentPointName = "";
        partID = System.Guid.NewGuid().ToString();
    }
}
