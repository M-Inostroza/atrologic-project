using UnityEngine;

public class RobotManager : MonoBehaviour
{
    [SerializeField]
    private RobotModel[] robots = new RobotModel[3]; // Always 3 slots
    
    private static RobotModel activeRobot;

    public static RobotModel ActiveRobot
    {
        get { return activeRobot; }
        private set { activeRobot = value; }
    }

    private void Start()
    {
        if (robots.Length > 0)
        {
            SetActiveRobot(0); // Default to the first robot
        }
    }

    public void SetActiveRobot(int index)
    {
        if (index < 0 || index >= robots.Length) return;

        activeRobot = robots[index];
        Debug.Log($"Active robot is now: {activeRobot.robotName}");
    }
}
