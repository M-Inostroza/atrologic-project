using UnityEngine;
using UnityEngine.SceneManagement;

public class Wheel : Part
{
    private Rigidbody2D rb2D;
    private void Awake()
    {
        partType = PartType.Ground;

        // TODO Cambiar a state machine from gamemanager
        if (SceneManager.GetActiveScene().name == "Level")
        {
            // Agregar un Rigidbody2D si no existe
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

                // Crear un nuevo WheelJoint2D para cada Wheel
                WheelJoint2D wheelJoint = core.gameObject.AddComponent<WheelJoint2D>();

                // Configurar el WheelJoint2D
                wheelJoint.connectedBody = rb2D;
                wheelJoint.anchor = core.transform.InverseTransformPoint(transform.parent.position);
            }
        }
    }

    private void Update()
    {
        base.Update();

        RotateWheel(0.2f);
    }


    public void RotateWheel(float speed)
    {
        if (rb2D != null)
        {
            rb2D.AddTorque(-speed);
        }
    }
}
