using UnityEngine;

public class MovimientoCamara : MonoBehaviour 
{
    [Header("Configuración")]
    public float sensibilidad = 2f;
    public Transform playerBody;
    
    private float rotacionX = 0f;
    private float rotacionY = 0f;

    void Start()
    {
        // Bloquear y ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // Inicializar rotación Y con la rotación actual del jugador
        if (playerBody != null)
        {
            rotacionY = playerBody.eulerAngles.y;
        }
    }

    void Update()
    {
        // Obtener el movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad;

        // Actualizar rotación vertical (cámara)
        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

        // Actualizar rotación horizontal (jugador completo)
        rotacionY += mouseX;
        playerBody.rotation = Quaternion.Euler(0f, rotacionY, 0f);
    }
}
