using UnityEngine;

public class AirPart : Part
{
    // Additional properties specific to air parts
    public float thrustPower;

    void Start()
    {
        base.Start();
        // Initialize additional properties
        thrustPower = 10f;
    }

    // Additional methods specific to air parts
    public void Fly()
    {
        // Implement flight logic for air parts
    }
}
