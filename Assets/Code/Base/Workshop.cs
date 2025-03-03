
using TMPro;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    [SerializeField] RobotManager robotManager;
    [SerializeField] InventoryManager inventoryManager;

    [SerializeField] private BoxCollider2D workshopCollider;
    [SerializeField] private Transform cardGrid;
    [SerializeField] private GameObject ModifyPanel;
    [SerializeField] private TMP_Text[] statPanels;

    private void OnEnable()
    {
        if (workshopCollider != null)
        {
            workshopCollider.enabled = false;
        }
        robotManager.ShowPreview();
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

    public void ActivateModifyPanel(int energyUsage)
    {
        ModifyPanel.SetActive(true);
        // Fill the stats panels
        statPanels[0].text = energyUsage.ToString() + "/s";
    }
}
