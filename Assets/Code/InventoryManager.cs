using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<PartData> partList = new List<PartData>();

    [SerializeField] GameObject partCardPrefab;

    private void Start()
    {
        //ResetInventory();
        LoadInventory();
    }

    //public void AddPartToInventory(PartData partData)
    //{
    //    if (partData == null)
    //    {
    //        Debug.LogError("Attempted to add a null part to inventory.");
    //        return;
    //    }

    //    // Create a new instance of PartData from the existing asset
    //    //PartData newPart = Instantiate(partData);

    //    // Assign unique ID
    //    newPart.partID = System.Guid.NewGuid().ToString();

    //    Debug.Log("From add to inv: " + newPart);

    //    if (!partList.Contains(newPart))
    //    {
    //        partList.Add(newPart);
    //        CreateCard(newPart);
    //        Debug.Log($"Added {newPart.partName} {newPart.partID} to inventory.");
    //    }
    //}


    public void ResetInventory()
    {
        if (ES3.KeyExists("inventory"))
        {
            ES3.DeleteKey("inventory");
            partList.Clear();
            Debug.Log("Inventory reset.");
        }
    }

    public void RemovePart(PartData partData)
    {
        if (partList.Contains(partData))
        {
            partList.Remove(partData);
            Debug.Log($"Removed {partData.partName} from inventory.");
        }
    }

    public List<PartData> GetParts()
    {
        Debug.Log("Returning parts." + partList);
        return new List<PartData>(partList);
    }

    public void SaveInventory()
    {
        List<PartData> serializableParts = new List<PartData>();

        foreach (PartData part in partList)
        {
            PartData partData = new PartData
            {
                partName = part.partName,
                isDeployed = part.isDeployed,
                isActive = part.isActive,
                localPosition = part.localPosition,
                localRotation = part.localRotation,
                attachmentPointName = part.attachmentPointName,
                partID = part.partID
            };

            serializableParts.Add(partData);
            Debug.Log($"Saving Part: {partData.partName}, Active: {partData.isActive}");
        }

        ES3.Save("inventory", serializableParts);
        Debug.Log("Inventory saved.");
    }

    private void LoadInventory()
    {
        if (ES3.KeyExists("inventory"))
        {
            List<PartData> savedParts = ES3.Load<List<PartData>>("inventory");

            foreach (PartData data in savedParts)
            {
                Debug.Log($"Loading Part: {data.partName}");

                // Load the correct prefab from Resources
                GameObject partPrefab = Resources.Load<GameObject>($"Parts/{data.partName}");

                if (partPrefab != null)
                {
                    GameObject newPart = Instantiate(partPrefab, transform);
                    PartData partComponent = newPart.GetComponent<PartData>();

                    if (partComponent == null)
                    {
                        Debug.LogError($"Loaded part {data.partName} but could not find PartData component! Skipping...");
                        Destroy(newPart);
                        continue;
                    }

                    // Restore part properties
                    partComponent.isDeployed = data.isDeployed;
                    partComponent.isActive = data.isActive;
                    newPart.transform.localPosition = data.localPosition;
                    newPart.transform.localRotation = data.localRotation;

                    partList.Add(partComponent);
                    Debug.Log($"Loaded Part: {data.partName}, Deployed: {data.isDeployed}");
                }
                else
                {
                    Debug.LogWarning($"Part prefab {data.partName} not found!");
                }
            }
        }
        else
        {
            Debug.LogWarning("No saved inventory found.");
        }
    }

    void CreateCard(PartData part)
    {
        GameObject cardObj = Instantiate(partCardPrefab, transform);
        PartCard newCard = cardObj.GetComponent<PartCard>();
        if (newCard != null)
        {
            newCard.SetPart(part);
        }
    }

    private void OnApplicationQuit()
    {
        SaveInventory();
    }
}
