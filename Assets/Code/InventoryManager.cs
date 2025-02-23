using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<Part> partList = new List<Part>();

    private void Start()
    {
        LoadInventory();
    }

    public void AddPart(Part part)
    {
        if (part != null && !partList.Contains(part))
        {
            partList.Add(part);
            SaveInventory(); // Save inventory after adding a part
            Debug.Log($"Part {part.name} added to inventory.");
        }
    }

    public void RemovePart(Part part)
    {
        if (partList.Contains(part))
        {
            partList.Remove(part);
            SaveInventory();
            Debug.Log($"Part {part.name} removed from inventory.");
        }
        else
        {
            Debug.LogWarning($"Part {part.name} not found in inventory.");
        }
    }

    public List<Part> GetParts()
    {
        return new List<Part>(partList);
    }

    public void ClearInventory()
    {
        partList.Clear();
        SaveInventory(); // Save the empty inventory
        Debug.Log("Inventory cleared.");
    }

    public void SaveInventory()
    {
        List<PartData> partDataList = new List<PartData>();

        foreach (Part part in partList)
        {
            if (part == null) continue; // Avoid null parts

            string cleanName = part.name.Replace("(Clone)", "").Trim();
            PartData partData = new PartData(part)
            {
                name = cleanName,
                isDeployed = part.isDeployed // Save the deployed state
            };

            partDataList.Add(partData);
            Debug.Log($"Saving Part: {cleanName}, Deployed: {partData.isDeployed}");
        }

        ES3.Save("inventory", partDataList);
        Debug.Log("Inventory saved.");
    }

    private void LoadInventory()
    {
        if (ES3.KeyExists("inventory"))
        {
            List<PartData> savedParts = ES3.Load<List<PartData>>("inventory");

            foreach (PartData data in savedParts)
            {
                string cleanName = data.name.Replace("(Clone)", "").Trim();
                GameObject partPrefab = Resources.Load<GameObject>($"Parts/{cleanName}");

                if (partPrefab != null)
                {
                    GameObject newPart = Instantiate(partPrefab);
                    Part partComponent = newPart.GetComponent<Part>();

                    if (partComponent == null)
                    {
                        Debug.LogError($"Loaded part {cleanName} but could not find Part component! Skipping...");
                        Destroy(newPart);
                        continue;
                    }

                    // Restore part properties
                    partComponent.instanceID = data.instanceID;
                    partComponent.isDeployed = data.isDeployed; // Restore deployment state
                    newPart.transform.localPosition = data.localPosition;
                    newPart.transform.localRotation = data.localRotation;

                    // Set parent to InventoryManager
                    newPart.transform.SetParent(this.transform);

                    // Restore active state
                    newPart.SetActive(data.isActive);

                    partList.Add(partComponent);
                    Debug.Log($"Loaded Part: {cleanName}, Deployed: {data.isDeployed}");
                }
                else
                {
                    Debug.LogWarning($"Part prefab {cleanName} not found in Resources! Skipping...");
                }
            }
        }
        else
        {
            Debug.LogWarning("No saved inventory found.");
        }
    }
}
