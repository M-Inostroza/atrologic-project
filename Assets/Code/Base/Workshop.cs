
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

    private void OnEnable()
    {
        if (workshopCollider != null)
        {
            workshopCollider.enabled = false;
        }
        robotManager.ShowPreview(previewX, previewY);
    }

    private void OnDisable()
    {
        robotManager.HidePreview();
        if (workshopCollider != null)
        {
            workshopCollider.enabled = true;
        }

        DestroyRougueParts();
    }

    void DestroyRougueParts()
    {
        Part[] allParts = FindObjectsOfType<Part>();

        foreach (Part part in allParts)
        {
            if (part.transform.parent == null) // If it has no parent, destroy it
            {
                Debug.Log($"Destroying orphaned part: {part.name}");
                part.DestroyRougue();
            }
        }
    }
}
