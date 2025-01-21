using UnityEngine;

public class GroundPart : Part
{
    // Additional properties specific to ground parts
    public float groundSpeed;

    void Start()
    {
        base.Start();
        // Initialize additional properties
        groundSpeed = 10f;
    }

    // Additional methods specific to ground parts
    public void Move()
    {
        // Implement movement logic for ground parts
    }
}
