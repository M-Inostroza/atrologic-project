using Unity.VisualScripting;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    [SerializeField] RobotManager robotManager;

    // Coordinates for preview
    private float previewX = 12.802f;
    private float previewY = -5.131f;

    private void OnEnable()
    {
        robotManager.ShowPreview(previewX, previewY);
    }

    private void OnDisable()
    {
        robotManager.HidePreview();
    }
}
