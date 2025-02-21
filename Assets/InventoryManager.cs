using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<Part> partsInventory;

    private void Start()
    {
        GetParts();
    }

    public void AddPart(Part part)
    {
        partsInventory.Add(part);
        Debug.Log($"Part {part.name} added to inventory.");
        ES3.Save<List<Part>>("partsInventory", partsInventory);
    }

    public List<Part> GetParts()
    {
        if (ES3.KeyExists("partsInventory"))
        {
            partsInventory = ES3.Load<List<Part>>("partsInventory");
        }
        else
        {
            partsInventory = new List<Part>();
            Debug.LogWarning("partsInventory key not found. Initializing with an empty list.");
        }

        Debug.Log("Part count: " + partsInventory.Count);
        foreach (var part in partsInventory)
        {
            Debug.Log("Parts in inventory: " + part.name);
        }
        return new List<Part>(partsInventory);
    }
}
