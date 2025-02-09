using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Spawn robot
    RobotManager robotManager;

    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level")
        {
            robotManager = FindFirstObjectByType<RobotManager>();

            robotManager.SpawnRobot(spawnPoint);
            RobotCamera.FindRobot();
        }
    }
}
