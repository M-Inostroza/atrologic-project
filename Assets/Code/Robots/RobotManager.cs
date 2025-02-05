using UnityEngine;
using UnityEngine.Rendering;

public class RobotManager : MonoBehaviour
{
    public RobotModel robotModel;

    private GameObject robotInstance;

    public void ShowPreview(float x, float y)
    {
        Vector3 position = new Vector3(x, y, 0);
        robotInstance = Instantiate(robotModel.prefab, position, Quaternion.identity);

        // Initialize attachment points
        robotModel.InitializeAttachmentPoints(robotInstance);
    }

    public void HidePreview()
    {
        Destroy(robotInstance);
    }

    public void AttachPartToRobot(Part part, int attachmentPointIndex)
    {
        robotModel.AttachPart(part, attachmentPointIndex);
    }

    public void DetachPartFromRobot(Part part)
    {
        robotModel.DetachPart(part);
    }
}
