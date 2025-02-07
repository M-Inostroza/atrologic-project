using UnityEngine;

[CreateAssetMenu(fileName = "NewRobot", menuName = "Robots/Robot")]
public class RobotModel : ScriptableObject
{
    public string robotName;
    public Sprite icon; // For UI representation
    public GameObject prefab; // For in-game representation

    // Modifiable stats
    public int currentHP;
    public int currentPower;

    // Base stats
    public int baseHP;
    public int basePowerCapacity;

    // Attachment points
    Transform[] attachmentPoints;

    // Methods for upgrades
    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, baseHP);
    }

    public void RechargePower(int amount)
    {
        currentPower = Mathf.Min(currentPower + amount, basePowerCapacity);
    }

    public void ResetStats()
    {
        currentHP = baseHP;
        currentPower = basePowerCapacity;
    }

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
