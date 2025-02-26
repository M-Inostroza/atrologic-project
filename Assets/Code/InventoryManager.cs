using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<PartData> partList = new List<PartData>();

    [SerializeField] GameObject partCardPrefab;

    private void Start()
    {
        LoadInventory();
    }

    public void AddPartToInventory(PartData partData)
    {
        if (partData == null)
        {
            Debug.LogError("Attempted to add a null part to inventory.");
            return;
        }

        Debug.Log($"From add to inv: {partData.partName}");

        if (!partList.Exists(p => p.partID == partData.partID))
        {
            partList.Add(partData);
            CreateCard(partData);
            Debug.Log($"Added {partData.partName} {partData.partID} to inventory.");
        }
    }

    public void SaveInventory()
    {
        ES3.Save("inventory", partList);
        Debug.Log("Inventory saved.");
    }

    public void LoadInventory()
    {
        if (ES3.KeyExists("inventory"))
        {
            partList = ES3.Load<List<PartData>>("inventory");

            foreach (PartData partData in partList)
            {
                Debug.Log($"Loaded Part: {partData.partName}");
                CreateCard(partData); // Recreate UI Card
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
