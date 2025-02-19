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

    private void Start()
    {
        resourceManager = FindFirstObjectByType<ResourceManager>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();

        priceText.text = price.ToString();
    }

    // instantiate part
    public void InstantiatePart()
    {
        Part newPart = Instantiate(part);
        newPart.transform.position = new Vector3(12.8f, -7f, 0f);
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