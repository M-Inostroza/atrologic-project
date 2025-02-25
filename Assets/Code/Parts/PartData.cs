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
}
