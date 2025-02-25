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

    [SerializeField] PartData partData;

    ResourceManager resourceManager;


    private void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        SetCard();
    }

    void SetCard()
    {
        Debug.Log("Name from data: " + partData.partName);
        partNameText.text = partData.partName;
        priceText.text = price.ToString();
    }

    //public void BuyPart()
    //{
    //    if (resourceManager.Scrap >= price)
    //    {
    //        inventory.AddPartToInventory(partData);
    //    } else {
    //        Debug.Log("Not enough scrap");
    //    }
    //}
}