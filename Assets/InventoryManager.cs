using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<Part> partsInventory;

    private void Start()
    {
        partsInventory = new List<Part>();
    }

    public void AddPart(Part part)
    {
        partsInventory.Add(part);
        Debug.Log($"Part {part.name} added to inventory.");
        for (int i = 0; i < partsInventory.Count; i++)
        {
            Debug.Log(partsInventory[i]);
        }
    }

    public List<Part> GetParts()
    {
        for (int i = 0; i < partsInventory.Count; i++)
        {
            Debug.Log(partsInventory[i]);
        }
        Debug.Log("Parts on inventory: " + partsInventory);
        return new List<Part>(partsInventory);
    }
}
