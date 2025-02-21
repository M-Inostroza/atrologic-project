using UnityEngine;
using TMPro;
public class PartCard : MonoBehaviour
{
    [SerializeField] private Part part;
    [SerializeField] private int price;
    [SerializeField] private TMP_Text priceText;

    ResourceManager resourceManager;
    InventoryManager inventoryManager;

    [SerializeField]  GameObject buyBtn;
    [SerializeField]  GameObject deployBtn;

    string cardInstanceID;

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
            // Instantiate a new part
            Part newPart = Instantiate(part);
            newPart.transform.position = new Vector3(12.8f, -7f, 0f);
            newPart.gameObject.SetActive(false); // Keep inactive
            newPart.transform.parent = inventoryManager.transform;

            // Assign a unique ID to distinguish between similar parts
            newPart.instanceID = $"{part.name}_{System.Guid.NewGuid()}";

            cardInstanceID = newPart.instanceID;

            // Add to the inventory list
            inventoryManager.partList.Add(newPart);

            // Deduct cost
            resourceManager.RemoveScrap(price);

            Debug.Log($"Bought and stored: {newPart.name}");
        }
        else
        {
            Debug.Log("Not enough scrap to buy this part");
        }
    }

    public void ActivatePart()
    {
        foreach (Transform partTransform in inventoryManager.transform)
        {
            Part part = partTransform.GetComponent<Part>();
            Debug.Log(part.instanceID + " instance from inventory");
            Debug.Log(cardInstanceID + " instance from card");
            if (part.instanceID == cardInstanceID)
            {
                Debug.Log("from equall");
                part.gameObject.SetActive(true);
            }
        }
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