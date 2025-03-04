using UnityEngine;

[System.Serializable]
public class PartData
{
    public string partName;
    public bool isDeployed;
    public bool isAtached;
    public bool isActive;
    public Vector3 localPosition;
    public Quaternion localRotation;
    public string attachmentPointName;
    public string partID;
    public float energyUsage;


    // Constructor for new parts
    public PartData(string name)
    {
        partName = name;
        isDeployed = false;
        isAtached = false;
        isActive = false;
        localPosition = Vector3.zero;
        localRotation = Quaternion.identity;
        energyUsage = 0;
        attachmentPointName = "";
        partID = System.Guid.NewGuid().ToString();
    }
}
