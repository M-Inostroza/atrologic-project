using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Spawn robot
    RobotManager robotManager;

    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        robotManager = FindFirstObjectByType<RobotManager>();

        robotManager.SpawnRobot(spawnPoint);
    }
}
