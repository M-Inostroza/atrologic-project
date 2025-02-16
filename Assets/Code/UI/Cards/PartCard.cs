using UnityEngine;
using TMPro;
public class PartCard : MonoBehaviour
{
    [SerializeField] private Part part;
    [SerializeField] private int price;
    [SerializeField] private TMP_Text priceText;

    ResourceManager resourceManager;
    InventoryManager inventoryManager;

    private void Start()
    {
        resourceManager = FindFirstObjectByType<ResourceManager>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        priceText.text = price.ToString();
    }

    //instantiate a new part
    public void InstantiatePart()
    {
        if (resourceManager.Scrap >= price)
        {
            resourceManager.RemoveScrap(price);
            Part newPart = Instantiate(part);
            newPart.transform.position = new Vector3(12.8f, -7f, 0f);
        }
        else
        {
            Debug.Log("Not enough scrap to buy this part");
        }
    }

    public void BuyPart(Part part)
    {
        if (resourceManager.Scrap >= price)
        {
            resourceManager.RemoveScrap(price);
            inventoryManager.AddPart(part);
        }
        else
        {
            Debug.Log("Not enough scrap to buy this part");
        }
    }
}