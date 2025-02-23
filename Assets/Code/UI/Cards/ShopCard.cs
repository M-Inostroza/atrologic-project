using UnityEngine;
using TMPro;
public class ShopCard : MonoBehaviour
{
    [SerializeField] private TMP_Text partNameText;
    [SerializeField] private Sprite partIllustration;

    [SerializeField] private int price;
    [SerializeField] private TMP_Text priceText;

    [SerializeField] Workshop workshop;
    [SerializeField] GameObject buyBtn;

    [SerializeField] PartData part;

    private void Start()
    {
        SetCard();
    }

    void SetCard()
    {
        partNameText.text = part.partName;
        priceText.text = price.ToString();
    }
}