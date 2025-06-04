using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    [SerializeField] private TMP_Text partNameText;
    [SerializeField] private Image partIllustration;

    [SerializeField] private int price;
    [SerializeField] private TMP_Text priceText;

    [SerializeField] InventoryManager inventory;
    [SerializeField] GameObject buyBtn;

    [SerializeField] private ResourceManager resourceManager;

    private void Start()
    {
        if (resourceManager == null)
        {
            resourceManager = FindFirstObjectByType<ResourceManager>();
        }
    }

    public void BuyPart(string partName)
    {
        if (resourceManager.Scrap >= price)
        {
            PartData newPart = new PartData(partName);
            newPart.isDeployed = false;
            inventory.AddPartToInventory(newPart);
            inventory.SaveInventory();
            Debug.Log($"Bought and stored: {newPart.partName}");
        }
        else
        {
            Debug.Log("Not enough scrap");
        }
    }
}
