using UnityEngine;
using TMPro;
public class PartCard : MonoBehaviour
{
    [SerializeField] private Part part;
    [SerializeField] private int price;
    [SerializeField] private TMP_Text priceText;

    ResourceManager resourceManager;
    InventoryManager inventoryManager;
    [SerializeField] Workshop workshop;

    [SerializeField]  GameObject buyBtn;
    [SerializeField]  GameObject deployBtn;
    [SerializeField] GameObject modifyBtn;

    public string CardInstanceID { get; private set; }

    private void Start()
    {
        resourceManager = FindFirstObjectByType<ResourceManager>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();

        priceText.text = price.ToString();
    }

    public void BuyPart(Part part)
    {
        if (resourceManager.Scrap >= price)
        {
            resourceManager.RemoveScrap(price);
            Part newPart = Instantiate(part);
            newPart.transform.position = new Vector3(12.8f, -7f, 0f);
            newPart.gameObject.SetActive(false);
            newPart.transform.parent = inventoryManager.transform;

            // Assign unique ID and ensure it starts as not deployed
            newPart.instanceID = $"{part.name}_{System.Guid.NewGuid()}";
            newPart.isDeployed = false;

            Debug.Log("part instance: " + newPart.instanceID);

            // Add to inventory & save it
            inventoryManager.AddPart(newPart);
            inventoryManager.SaveInventory(); // Save immediately

            workshop.PopulateGrid();

            Debug.Log($"Bought and stored: {newPart.name}");
        }
        else
        {
            Debug.Log("Not enough scrap to buy this part");
        }
    }

    public void InitializeCard(Part assignedPart)
    {
        if (assignedPart == null)
        {
            Debug.LogError("Attempted to initialize PartCard with a null part! Skipping...");
            return;
        }

        part = assignedPart;
        CardInstanceID = part.instanceID;
        priceText.text = price.ToString();

        if (part.isDeployed)
        {
            // Show Modify Button, Hide Deploy Button
            deployBtn.SetActive(false);
            modifyBtn.SetActive(true);
        }
        else
        {
            // Show Deploy Button, Hide Modify Button
            deployBtn.SetActive(true);
            modifyBtn.SetActive(false);
        }
    }

    public void ActivatePart()
    {
        foreach (Part part in inventoryManager.GetParts())
        {
            if (part.instanceID == CardInstanceID && !part.isDeployed)
            {
                part.gameObject.SetActive(true);
                part.isDeployed = true;
                Debug.Log($"Activated part: {part.name} (Deployed)");

                deployBtn.SetActive(false);
                return;
            }
        }

        Debug.LogWarning($"Part with ID {CardInstanceID} not found or already deployed.");
    }

    public void DeployMode(bool deploy)
    {
        if (deploy)
        {
            buyBtn.SetActive(false);
            deployBtn.SetActive(true);
        }
        else
        {
            buyBtn.SetActive(true);
            deployBtn.SetActive(false);
        }
    }
}