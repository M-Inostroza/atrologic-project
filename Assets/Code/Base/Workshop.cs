
using UnityEngine;

public class Workshop : MonoBehaviour
{
    [SerializeField] RobotManager robotManager;
    [SerializeField] InventoryManager inventoryManager;

    // Coordinates for preview
    private float previewX = 13.46f;
    private float previewY = -5.131f;

    [SerializeField] private BoxCollider2D workshopCollider;
    [SerializeField] private Transform cardGrid;

    [SerializeField] GameObject partCardPrefab;

    private void OnEnable()
    {
        if (workshopCollider != null)
        {
            workshopCollider.enabled = false;
        }
        //PopulateGrid();
        robotManager.ShowPreview(previewX, previewY);
        
    }

    private void OnDisable()
    {
        robotManager.HidePreview();
        if (workshopCollider != null)
        {
            workshopCollider.enabled = true;
        }
    }

    public void AddPartCard(PartData partData)
    {
        Debug.Log("From part added");
        GameObject cardObj = Instantiate(partCardPrefab, cardGrid);
        Debug.Log(cardObj);
        PartCard newCard = cardObj.GetComponent<PartCard>();

        if (newCard != null)
        {
            cardObj.transform.SetParent(cardGrid);
            newCard.SetPart(partData);
        }
    }



    //public void PopulateGrid()
    //{
    //    foreach (Transform child in cardGrid)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    foreach (PartData part in inventoryManager.GetParts())
    //    {
    //        GameObject cardObj = Instantiate(partCardPrefab, cardGrid);
    //        PartCard newCard = cardObj.GetComponent<PartCard>();

    //        if (newCard != null)
    //        {
    //            newCard.InitializeCard(part);
    //        }
    //    }
    //}

}
