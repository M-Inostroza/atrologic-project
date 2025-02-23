using UnityEngine;

[CreateAssetMenu(fileName = "NewPart", menuName = "Parts/Part")]
public class PartData : ScriptableObject
{
    public string partName;
    public bool isAttached;
    public bool isActive;
    public Vector3 localPosition;
    public Quaternion localRotation;
    public string attachmentPointName;

    // Additional properties
    public Sprite partIcon;
    public GameObject partPrefab;
}