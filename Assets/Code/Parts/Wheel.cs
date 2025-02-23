using UnityEngine;

public class Wheel : Part
{
    private Rigidbody2D rb2D;
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private new void Start()
    {
        base.Start();
        SetWheel();
    }

    private new void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        RotateWheel(1.6f);
    }

    void SetWheel()
    {
        if (gameManager.GetCurrentState() == GameManager.GameState.Level)
        {
            // Add Rigidbody2D
            rb2D = GetComponent<Rigidbody2D>();
            if (rb2D == null)
            {
                rb2D = gameObject.AddComponent<Rigidbody2D>();
            }

            // Eliminate constrains
            rb2D.constraints = RigidbodyConstraints2D.None;

            Core core = FindFirstObjectByType<Core>();
            if (core != null)
            {
                Rigidbody2D coreRb2D = core.GetComponent<Rigidbody2D>();
                if (coreRb2D == null)
                {
                    coreRb2D = core.gameObject.AddComponent<Rigidbody2D>();
                }

                // Create WheelJoint2D for each Wheel
                WheelJoint2D wheelJoint = core.gameObject.AddComponent<WheelJoint2D>();

                // Set WheelJoint2D
                wheelJoint.connectedBody = rb2D;
                wheelJoint.anchor = core.transform.InverseTransformPoint(transform.parent.position);
            }
        }
    }
    public void RotateWheel(float speed)
    {
        if (rb2D != null)
        {
            rb2D.AddTorque(-speed);
        }
    }
}
