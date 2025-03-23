using UnityEngine;
using UnityEngine.UI;

public class CodeInterface : MonoBehaviour
{
    [SerializeField] Transform[] tabs;

    /*
     0 : Sensors
     */

    Core core;

    private void OnEnable()
    {
        core = FindFirstObjectByType<Core>();
        CheckSensors();
    }

    // Check sensors from core
    public void CheckSensors()
    {
        if (core.activeSensors.Count > 0)
        {
            // Load the ANG Sensor prefab from Resources (without the file extension)
            GameObject sensorPrefab = Resources.Load<GameObject>("Code Nodes/ANG Sensor");

            if (sensorPrefab != null && tabs.Length > 0)
            {
                // Instantiate the prefab as a child of the first tab
                GameObject newSensor = Instantiate(sensorPrefab, tabs[0]);
                newSensor.transform.localPosition = Vector3.zero; // Center it in the tab
            }
            else
            {
                Debug.LogWarning("Sensor prefab not found or no tabs available.");
            }
        }
    }

}
