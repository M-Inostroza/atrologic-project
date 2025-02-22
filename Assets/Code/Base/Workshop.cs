using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    [SerializeField] RobotManager robotManager;
    [SerializeField] InventoryManager inventoryManager;

    [SerializeField] GameObject partCardPrefab;

    // Coordinates for preview
    private float previewX = 13.46f;
    private float previewY = -5.131f;

    [SerializeField] private BoxCollider2D workshopCollider;
    [SerializeField] private Transform cardGrid;

    private void OnEnable()
    {
        PopulateGrid();
        robotManager.ShowPreview(previewX, previewY);
        if (workshopCollider != null)
        {
            workshopCollider.enabled = false;
        }
    }

    private void OnDisable()
    {
        robotManager.HidePreview();
        if (workshopCollider != null)
        {
            workshopCollider.enabled = true;
        }
    }

    public void PopulateGrid()
    {
        foreach (Transform child in cardGrid)
        {
            Destroy(child.gameObject);
        }

        foreach (Part part in inventoryManager.GetParts())
        {
            GameObject cardObj = Instantiate(partCardPrefab, cardGrid);
            PartCard newCard = cardObj.GetComponent<PartCard>();

            if (newCard != null)
            {
                newCard.InitializeCard(part);
            }
        }
    }

}
