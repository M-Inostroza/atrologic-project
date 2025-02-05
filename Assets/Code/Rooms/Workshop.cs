using Unity.VisualScripting;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    [SerializeField] RobotManager robotManager;

    // Coordinates for preview
    private float previewX = 12.802f;
    private float previewY = -5.131f;

    [SerializeField] private BoxCollider2D workshopCollider;


    private void OnEnable()
    {
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
}
