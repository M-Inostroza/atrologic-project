using TMPro;
using UnityEngine;

public class PowerGenerator : MonoBehaviour
{
    [SerializeField] private GameObject energyPrefab;

    private GameObject currentEnergyInstance;
    private float spawnInterval = 2f; // 2m
    private float timer;

    public static bool energySpawned;
    [SerializeField] private TMP_Text energyTimer;

    private void Start()
    {
        energySpawned = false;
        timer = spawnInterval;
    }

    private void Update()
    {
        RunTimer();
    }

    void RunTimer()
    {
        if (!energySpawned)
        {
            timer -= Time.deltaTime;
            energyTimer.text = "Next: " + timer.ToString("F0");

            if (timer <= 0f)
            {
                if (currentEnergyInstance == null)
                {
                    Vector3 randomPosition = new Vector3(Random.Range(-3f, 3f), -6.5f, 0f);
                    currentEnergyInstance = Instantiate(energyPrefab, randomPosition, Quaternion.identity);
                }
                else
                {
                    Debug.Log("Energy prefab already instantiated.");
                }
                timer = spawnInterval;
                energySpawned = true;
            }
        }
    }
}
