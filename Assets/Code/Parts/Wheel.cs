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
            if (GetComponent<Rigidbody2D>() == null)
            {
                gameObject.AddComponent<Rigidbody2D>();
            }

            Core core = FindObjectOfType<Core>();
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
                wheelJoint.connectedBody = GetComponent<Rigidbody2D>();
                wheelJoint.anchor = core.transform.InverseTransformPoint(transform.parent.position);
            }
        }
    }

    private void Update()
    {
        RotateWheel(5);
    }


    public void RotateWheel(float speed)
    {
        Debug.Log("Rotando");
        if (rb2D != null)
        {
            rb2D.AddTorque(-speed); // Aplica un torque negativo para rotar en el sentido de las agujas del reloj
        }
    }
}