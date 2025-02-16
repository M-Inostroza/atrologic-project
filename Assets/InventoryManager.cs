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
    }

    public List<Part> GetParts()
    {
        Debug.Log("Parts on inventory: " + partsInventory);
        return new List<Part>(partsInventory);
    }
}
