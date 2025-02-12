using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{
    public RobotModel robotModel;
    private GameObject robotInstance;

    [SerializeField] private List<GameObject> availableParts;

    public void SpawnRobot(Transform spawnPoint)
    {
        if (robotInstance != null) return;

        if (ES3.KeyExists("RobotPosition"))
        {
            LoadRobotState(spawnPoint); // Load Robot from ES3
            ActivateRigidbody();
        }
        else
        {
            Debug.Log("No robot");
        }
    }

    public void ShowPreview(float x, float y)
    {
        if (ES3.KeyExists("RobotPosition"))
        {
            LoadRobotState();
        }
        else
        {
            Vector3 position = new Vector3(x, y, 0);
            robotInstance = Instantiate(robotModel.prefab, position, Quaternion.identity);
            robotModel.InitializeAttachmentPoints(robotInstance);

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
        ES3.Save("RobotPrefabPath", robotModel.prefab.name); // Save prefab

        SaveParts();
    }

    public void LoadRobotState(Transform spawnPosition = null)
    {
        if (!ES3.KeyExists("RobotPosition")) return;

        // Destroy any existing instance before loading
        if (robotInstance != null) Destroy(robotInstance);

        Vector3 position = ES3.Load<Vector3>("RobotPosition");

        // Instantiate the saved prefab with modifications
        if (spawnPosition != null) {
            robotInstance = Instantiate(robotModel.prefab, spawnPosition.position, Quaternion.identity);
        } else
        {
            robotInstance = Instantiate(robotModel.prefab, position, Quaternion.identity);
        }
        robotModel.InitializeAttachmentPoints(robotInstance);

        LoadParts();
    }

    private void SaveParts()
    {
        List<PartData> attachedParts = new List<PartData>();

        Core core = robotInstance.GetComponentInChildren<Core>();

        if (core != null)
        {
            var attachmentPointsStatus = core.GetAttachmentPointsStatus();
            ES3.Save("AttachmentPointsStatus", attachmentPointsStatus);
        }

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
        foreach (GameObject prefab in availableParts)
        {
            if (prefab.name == name)
                return prefab;
        }
        return null;
    }

    private void ActivateRigidbody()
    {
        if (robotInstance != null)
        {
            Rigidbody2D robotRb2D = robotInstance.GetComponent<Rigidbody2D>();
            if (robotRb2D == null)
            {
                robotRb2D = robotInstance.AddComponent<Rigidbody2D>();
            }

            Rigidbody2D[] rigidbodies = robotInstance.GetComponentsInChildren<Rigidbody2D>();
            foreach (Rigidbody2D rb2D in rigidbodies)
            {
                rb2D.simulated = true;
            }
        }
    }
}
