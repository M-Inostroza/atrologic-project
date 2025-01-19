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
}
