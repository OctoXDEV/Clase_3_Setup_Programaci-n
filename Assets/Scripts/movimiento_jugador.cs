using UnityEngine;
using System.Collections;

public class Movimiento_jugador : MonoBehaviour
{
    [Header("Movimiento Básico")]
    public float speed = 5f;
    public float jumpForce = 5f;

    [Header("Sistema de Disparo")]
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform shootPoint; // Punto de disparo (Empty)
    public Material[] bulletMaterials; // Array de materiales para las balas
    [Range(10f, 100f)]
    [Tooltip("Fuerza inicial del disparo")]
    public float shootForce = 20f; // Fuerza del disparo
    [Range(0f, 50f)]
    [Tooltip("Fuerza adicional aplicada a la bala")]
    public float additionalForce = 10f; // Fuerza adicional
    public float fireRate = 0.5f; // Tiempo entre disparos
    [Tooltip("Tiempo en segundos que la bala permanecerá en la escena")]
    public float bulletLifetime = 3f; // Tiempo de vida de la bala

    private float nextTimeToFire = 0f;
    private float verticalVelocity = 0f;
    private bool hasJumped = false;

    void Update()
    {
        MovimientoPersonaje();
        HandleShooting();
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

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        // Crear la bala
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        
        // Cambiar material aleatoriamente
        if (bulletMaterials != null && bulletMaterials.Length > 0)
        {
            Renderer bulletRenderer = bullet.GetComponent<Renderer>();
            if (bulletRenderer != null)
            {
                Material randomMaterial = bulletMaterials[Random.Range(0, bulletMaterials.Length)];
                bulletRenderer.material = randomMaterial;
            }
        }

        // Obtener el Rigidbody existente
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Aplicar fuerza directamente
            rb.AddForce(shootPoint.forward * (shootForce + additionalForce), ForceMode.Impulse);
        }

        // Destruir la bala después del tiempo especificado
        Destroy(bullet, bulletLifetime);
    }
}
