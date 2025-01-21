using UnityEngine;

public class RobotManager : MonoBehaviour
{
    public RobotModel robotModel;

    private GameObject robotInstance;

    void Start()
    {
        // Instantiate the robot prefab
        robotInstance = Instantiate(robotModel.prefab);

        // Initialize attachment points
        robotModel.InitializeAttachmentPoints(robotInstance);
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
