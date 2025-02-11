using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PartCard : MonoBehaviour
{
    [SerializeField] private Part part;
    [SerializeField] private int price;
    [SerializeField] private TMP_Text priceText;

    ResourceManager resourceManager;


    private void Start()
    {
        resourceManager = FindFirstObjectByType<ResourceManager>();
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
}