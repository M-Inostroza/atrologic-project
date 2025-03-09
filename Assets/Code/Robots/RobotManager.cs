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
            LoadRobotState(spawnPoint);
            ActivateRigidbody();
        }
        else
        {
            Debug.Log("No robot");
        }
    }

    public void ShowPreview()
    {
        if (ES3.KeyExists("RobotPosition"))
        {
            LoadRobotState();
            robotInstance.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            Vector3 position = new Vector3(0, 0, 0);
            robotInstance = Instantiate(robotModel.prefab, position, Quaternion.identity);
            robotModel.InitializeAttachmentPoints(robotInstance);
            robotInstance.GetComponent<BoxCollider2D>().enabled = false;

            SaveRobotState();
        }
    }

    public void HidePreview()
    {
        if (robotInstance != null)
        {
            SaveRobotState();
            Destroy(robotInstance);
        }
    }

    private void OnApplicationQuit()
    {
        SaveRobotState();
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

        Debug.Log("From Load State: " + robotInstance);
        // Destroy any existing instance before loading
        if (robotInstance != null) Destroy(robotInstance);

        Vector3 position = new Vector3(13.46f, -5.131f, 0);

        if (spawnPosition != null) {
            robotInstance = Instantiate(robotModel.prefab, spawnPosition.position, Quaternion.identity);
            Debug.Log("Spawn position found: " + robotInstance);
        } else
        {
            robotInstance = Instantiate(robotModel.prefab, position, Quaternion.identity);
            Debug.Log("Spawn position not found: " + robotInstance + "deploying on position: " + position);
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
            foreach (Transform partTransform in point)
            {
                Part partComponent = partTransform.GetComponent<Part>();

                if (partComponent != null)
                {
                    // Store data
                    PartData data = new PartData(partComponent.name)
                    {
                        isDeployed = true,
                        partID = partComponent.GetPrefabID(),
                        isActive = partComponent.gameObject.activeSelf,
                        localPosition = partTransform.localPosition,
                        localRotation = partTransform.localRotation,
                        attachmentPointName = point.name
                    };

                    attachedParts.Add(data);
                }
            }
        }

        ES3.Save("RobotAttachedTransforms", attachedParts);
        Debug.Log("Robot parts saved.");
    }


    private void LoadParts()
    {
        if (!ES3.KeyExists("RobotAttachedTransforms")) return;

        List<PartData> attachedParts = ES3.Load<List<PartData>>("RobotAttachedTransforms");
        Debug.Log($"Loading {attachedParts.Count} parts...");
        foreach (PartData data in attachedParts)
        {
            Transform parent = FindChildByName(robotInstance.transform, data.attachmentPointName);
            if (parent != null)
            {
                string cleanName = data.partName.Replace("(Clone)", "").Trim();
                GameObject partPrefab = Resources.Load<GameObject>($"Parts/{cleanName}");

                if (partPrefab != null)
                {
                    GameObject newPart = Instantiate(partPrefab, parent);
                    newPart.transform.localPosition = data.localPosition;
                    newPart.transform.localRotation = data.localRotation;

                    Part partComponent = newPart.GetComponent<Part>();
                    if (partComponent != null)
                    {
                        partComponent.SetPrefabID(data.partID);
                        partComponent.isAttached = true;
                    }
                    else
                    {
                        Debug.LogWarning($"Prefab '{data.partName}' does not have a Part component!");
                    }
                }
                else
                {
                    Debug.LogWarning($"Part prefab '{data.partName}' not found in Resources!");
                }
            }
        }
        Debug.Log("Robot parts loaded.");
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

    private GameObject FindPartPrefabByName(string name)
    {
        foreach (GameObject prefab in availableParts)
        {
            if (prefab.name.Equals(name, System.StringComparison.OrdinalIgnoreCase))
                return prefab;
        }

        Debug.LogWarning($"Part prefab '{name}' not found in availableParts list!");
        return null;
    }
}
