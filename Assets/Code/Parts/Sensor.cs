using UnityEngine;

public class Sensor : Part
{
    private Vector2 direction = Vector2.right;
    private RaycastHit2D hit;

    private new void Update() // This is avoiding the part update to exe, check the wheel use to find solution, then keep testing the laser
    {
        DetectObjectToRight();
    }

    private void DetectObjectToRight()
    {
        // Define the direction to the right
        direction = transform.right;

        // Perform the raycast
        hit = Physics2D.Raycast(transform.position, direction * 10);

        //Debug.Log(hit.collider.name);
        // Check if the raycast hit an object with the name "Ground"
        if (hit.collider != null && hit.collider.gameObject != gameObject && hit.collider.name == "Ground")
        {
            // Calculate the angle between the sensor and the detected object
            Vector2 toObject = hit.point - (Vector2)transform.position;
            float angle = Vector2.Angle(direction, toObject);

            // Log the detected object and the angle
            Debug.Log($"Detected object: {hit.collider.name}, Angle: {angle} degrees");
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the raycast in the Scene view and Game view
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction * 10); // Adjust the length as needed

        // Draw a sphere at the hit point
        if (hit.collider != null && hit.collider.name == "Ground")
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(hit.point, 0.2f); // Adjust the size as needed
        }
    }
}