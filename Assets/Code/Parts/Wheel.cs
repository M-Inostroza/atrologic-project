using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Wheel : Part
{
    private Rigidbody2D rb2D;
    private GameManager gameManager;
    private ResourceManager resourceManager;

    private float rotationSpeed = 1.6f;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        resourceManager = FindFirstObjectByType<ResourceManager>();
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
        RotateWheel(rotationSpeed);
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

            // Eliminate constraints
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

    // power mode: if enery is less than 20% of the allowed level, the wheel will rotate slower

    public void RotateWheel(float speed)
    {
        if (rb2D != null && resourceManager.Energy >= 1)
        {
            rb2D.AddTorque(-speed);
            resourceManager.RemoveEnergy(1);
        }
    }
}




