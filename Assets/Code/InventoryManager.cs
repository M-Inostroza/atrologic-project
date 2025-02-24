using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<PartData> partList = new List<PartData>();

    private void Start()
    {
        //LoadInventory();
    }

    public void AddPart(PartData part)
    {
        if (part != null && !partList.Contains(part))
        {
            partList.Add(part);
            //SaveInventory();
            Debug.Log($"Added {part.partName} to inventory.");
        }
    }

    public void RemovePart(PartData part)
    {
        if (partList.Contains(part))
        {
            partList.Remove(part);
            //SaveInventory();
            Debug.Log($"Removed {part.partName} from inventory.");
        }
    }

    public List<PartData> GetParts()
    {
        return new List<PartData>(partList);
    }

    //public void SaveInventory()
    //{
    //    ES3.Save("inventory", partList);
    //    Debug.Log("Inventory saved.");
    //}

    //public void LoadInventory()
    //{
    //    if (ES3.KeyExists("inventory"))
    //    {
    //        partList = ES3.Load<List<PartData>>("inventory");
    //        Debug.Log("Inventory loaded.");
    //    }
    //}
}
