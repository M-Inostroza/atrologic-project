using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Spawn robot
    [SerializeField] private RobotManager robotManager;

    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level")
        {
            if (robotManager == null)
            {
                robotManager = FindFirstObjectByType<RobotManager>();
            }

            robotManager.SpawnRobot(spawnPoint);
            RobotCamera.FindRobot();
        }
    }
}
