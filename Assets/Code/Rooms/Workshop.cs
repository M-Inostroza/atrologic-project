using UnityEngine;

public class Workshop : MonoBehaviour
{
    [SerializeField]
    private Transform workshopTransform;
    [SerializeField]
    private SpriteRenderer workshopSpriteRenderer;

    private GameObject currentPreviewInstance;

    private void OnEnable()
    {
        ShowRobotModelPreview();
    }

    public void ShowRobotModelPreview()
    {
        if (currentPreviewInstance != null)
        {
            Destroy(currentPreviewInstance);
        }

        if (RobotManager.ActiveRobot != null && RobotManager.ActiveRobot.prefab != null)
        {
            currentPreviewInstance = Instantiate(RobotManager.ActiveRobot.prefab, workshopTransform);
            Bounds bounds = workshopSpriteRenderer.bounds;
            Vector3 firstHalfCenter = new Vector3(bounds.min.x + bounds.extents.x / 2, bounds.center.y, bounds.center.z);
            currentPreviewInstance.transform.localPosition = firstHalfCenter - workshopTransform.position;
            currentPreviewInstance.transform.localRotation = Quaternion.identity;
        }
    }
}
