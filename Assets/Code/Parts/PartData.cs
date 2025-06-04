using UnityEngine;

[System.Serializable]
public class PartData
{
    public string partID;
    public string partName;
    public string partType;
    public string attachmentPointName;

    public bool isDeployed;
    public bool isAttached;
    public bool isActive;

    public Vector3 localPosition;
    public Quaternion localRotation;
    
    public float energyUsage;

    // Constructor
    public PartData(string name)
    {
        partName = name;
        partType = "";
        attachmentPointName = "";
        isDeployed = false;
        isAttached = false;
        isActive = false;
        localPosition = Vector3.zero;
        localRotation = Quaternion.identity;
        energyUsage = 0;
        partID = System.Guid.NewGuid().ToString();
    }
}
