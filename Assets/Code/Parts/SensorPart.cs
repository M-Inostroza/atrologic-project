using UnityEngine;

public class SensorPart : Part
{
    // Additional properties specific to sensor parts
    public float detectionRange;

    void Start()
    {
        base.Start();
        // Initialize additional properties
        detectionRange = 15f;
    }

    // Additional methods specific to sensor parts
    public void Detect()
    {
        // Implement detection logic for sensor parts
    }
}
