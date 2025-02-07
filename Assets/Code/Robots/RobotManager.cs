using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{
    public RobotModel robotModel;
    private GameObject robotInstance;

    [SerializeField] private List<GameObject> availableParts;

    public void ShowPreview(float x, float y)
    {
        // Check if a robot is already saved
        if (ES3.KeyExists("RobotPosition"))
        {
            LoadRobotState(); // Load existing robot instead of instantiating a new one
        }
        else
        {
            Vector3 position = new Vector3(x, y, 0);
            robotInstance = Instantiate(robotModel.prefab, position, Quaternion.identity);
            robotModel.InitializeAttachmentPoints(robotInstance);

            // Save the new instance so it persists
            SaveRobotState();
        }
    }

    public void HidePreview()
    {
        if (robotInstance != null)
        {
            SaveRobotState(); // Save modifications before destroying
            Destroy(robotInstance);
        }
    }

    public void SaveRobotState()
    {
        if (robotInstance == null) return;

        ES3.Save("RobotPosition", robotInstance.transform.position);
        ES3.Save("RobotPrefabPath", robotModel.prefab.name); // Save prefab reference

        SaveParts();

        Debug.Log("Robot state saved!");
    }

    public void LoadRobotState()
    {
        if (!ES3.KeyExists("RobotPosition")) return;

        // Destroy any existing instance before loading
        if (robotInstance != null) Destroy(robotInstance);

        Vector3 position = ES3.Load<Vector3>("RobotPosition");

        // Instantiate the saved prefab with modifications
        robotInstance = Instantiate(robotModel.prefab, position, Quaternion.identity);
        robotModel.InitializeAttachmentPoints(robotInstance);

        LoadParts();

        Debug.Log("Robot state loaded!");
    }

    private void SaveParts()
    {
        List<PartData> attachedParts = new List<PartData>();

        foreach (Transform point in robotInstance.transform)
        {
            foreach (Transform part in point) // Go one level deeper
            {
                PartData data = new PartData
                {
                    name = part.name,
                    localPosition = part.localPosition,
                    localRotation = part.localRotation,
                    isAttached = part.GetComponent<Part>().isAttached,
                    attachmentPointName = point.name
                };
                attachedParts.Add(data);
            }
        }

        ES3.Save("RobotAttachedTransforms", attachedParts);
    }

    private void LoadParts()
    {
        if (!ES3.KeyExists("RobotAttachedTransforms")) return;

        List<PartData> attachedParts = ES3.Load<List<PartData>>("RobotAttachedTransforms");

        foreach (PartData data in attachedParts)
        {
            Transform parent = FindChildByName(robotInstance.transform, data.attachmentPointName);
            if (parent != null)
            {
                string cleanName = data.name.Replace("(Clone)", "").Trim();
                Debug.Log($"Loading part clean '{cleanName}' to '{parent.name}'");

                GameObject partPrefab = FindPartPrefabByName(cleanName);
                if (partPrefab != null)
                {
                    GameObject newPart = Instantiate(partPrefab, parent);
                    Part partComponent = newPart.GetComponent<Part>();

                    if (partComponent != null)
                    {
                        partComponent.isAttached = data.isAttached;
                        newPart.transform.localPosition = data.localPosition;
                        newPart.transform.localRotation = data.localRotation;
                    }
                    else
                    {
                        Debug.LogWarning($"Prefab '{data.name}' does not have a Part component!");
                    }
                }
                else
                {
                    Debug.LogWarning($"Part prefab '{data.name}' not found!");
                }
            }
        }
    }

    private Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;
        }
        return null;
    }

    private GameObject FindPartPrefabByName(string name)
    {
        foreach (GameObject prefab in availableParts) // Make sure availableParts is a list of prefabs
        {
            if (prefab.name == name)
                return prefab;
        }
        return null;
    }
}
