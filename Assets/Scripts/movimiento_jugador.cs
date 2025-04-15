using UnityEngine;

public class Movimiento_jugador : MonoBehaviour
{
    [Header("Movimiento Básico")]
    public float speed = 5f;
    public float jumpForce = 5f;

    private float verticalVelocity = 0f;
    private bool hasJumped = false; // Controla si ya se realizó el salto

    void Update()
    {
        MovimientoPersonaje();
    }

    private void MovimientoPersonaje()
    {
        // Get Input Axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Aplicar salto solo cuando se presiona space y no ha saltado aún
        if (Input.GetKeyDown(KeyCode.Space) && !hasJumped)
        {
            verticalVelocity = jumpForce;
            hasJumped = true;
        }

        // Resetear la velocidad vertical si no está saltando
        if (!Input.GetKey(KeyCode.Space))
        {
            verticalVelocity = 0f;
            hasJumped = false;
        }

        // Crear el vector de movimiento
        Vector3 movement = new Vector3(horizontalInput, verticalVelocity, verticalInput);
        
        // Mover el objeto
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
