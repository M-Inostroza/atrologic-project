using UnityEngine;

public class Sensor : Part
{
    private Vector2 direction = Vector2.right;
    private RaycastHit2D hit;

    private new void Update() // This is avoiding the part update to exe, check the wheel use to find solution, then keep testing the laser
    {
        base.Update();
        DetectObjectToRight();
    }

    private void DetectObjectToRight()
    {
        direction = transform.right; // Always points to the right

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, 10);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null) continue;

            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Resource"))
            {
                continue; // Ignore these objects
            }

            // ✅ Store the valid hit
            this.hit = hit;

            if (hit.collider.gameObject != gameObject && hit.collider.name == "Ground")
            {
                // ✅ Fix: Ensure hit.point is valid before calculations
                if (hit.point == Vector2.zero)
                {
                    Debug.LogWarning("Hit point is zero, skipping calculation.");
                    continue;
                }

                // ✅ Fix: Calculate the direction from sensor to hit point
                Vector2 hitDirection = ((Vector2)hit.point - (Vector2)transform.position).normalized;

                // ✅ Fix: Compare transform's right direction with the hit direction
                float angle = Vector2.SignedAngle(transform.right, hitDirection);

                Debug.Log($"Detected object: {hit.collider.name}, Angle: {angle} degrees");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Debug.DrawRay(transform.position, direction * 10, Color.red);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction * 10);

        if (hit.collider != null && hit.collider.name == "Ground")
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(hit.point, 0.2f);

            Debug.DrawRay(hit.point, Vector3.up * 0.5f, Color.green);
        }
    }

}