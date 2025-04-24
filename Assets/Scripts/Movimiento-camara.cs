using UnityEngine;

public class MovimientoCamara : MonoBehaviour 
{
    [Header("Configuración")]
    public float sensibilidad = 2f;
    public Transform playerBody;
    [Tooltip("Referencia a la cámara que se controlará")]
    public Camera camaraJugador;
    
    [Header("Límites de Rotación")]
    [Range(0f, 90f)]
    public float limiteSuperior = 80f;
    [Range(0f, 90f)]
    public float limiteInferior = 80f;
    
    private float rotacionX = 0f;
    private float rotacionY = 0f;

    void Start()
    {
        // Bloquear y ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // Si no se asignó una cámara, intentar obtener la cámara principal
        if (camaraJugador == null)
        {
            camaraJugador = Camera.main;
            Debug.LogWarning("No se asignó una cámara, usando la cámara principal");
        }
        
        // Inicializar rotaciones
        if (playerBody != null)
        {
            rotacionY = playerBody.eulerAngles.y;
        }
    }

    void Update()
    {
        if (camaraJugador == null) return;

        // Obtener el movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad;

        // Calcular la rotación vertical (con límites)
        rotacionX = Mathf.Clamp(rotacionX - mouseY, -limiteSuperior, limiteInferior);
        
        // Calcular la rotación horizontal
        rotacionY += mouseX;

        // Aplicar rotación a la cámara (solo X)
        camaraJugador.transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        
        // Rotar el cuerpo del jugador (solo Y)
        if (playerBody != null)
        {
            playerBody.rotation = Quaternion.Euler(0f, rotacionY, 0f);
        }
    }
}
