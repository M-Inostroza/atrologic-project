using UnityEngine;

[CreateAssetMenu(fileName = "NewRobot", menuName = "Robots/Robot")]
public class RobotModel : ScriptableObject
{
    public string robotName;

    public GameObject prefab;

    // Modifiable stats
    public int currentHP;

    // Base stats
    public int baseHP;

    // Attachment points
    Transform[] attachmentPoints;


    // Initialize attachment points at runtime
    public void InitializeAttachmentPoints(GameObject robotInstance)
    {
        Core corePoints = robotInstance.GetComponent<Core>();
        if (corePoints != null)
        {
            attachmentPoints = corePoints.attachmentPoints;
        }
    }
}
