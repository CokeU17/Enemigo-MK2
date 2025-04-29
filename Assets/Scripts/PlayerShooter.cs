using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bulletPrefab;    // Prefab de la bala
    public Transform firePoint;        // Punto de origen del disparo
    public float bulletSpeed = 10f;    // Velocidad de la bala

    private Vector2 lastMoveDirection = Vector2.right; // Por defecto hacia la derecha

    void Update()
    {
        // Leer la dirección del movimiento
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 inputDirection = new Vector2(moveX, moveY);

        if (inputDirection != Vector2.zero)
        {
            lastMoveDirection = inputDirection.normalized;
        }

        if (Input.GetMouseButtonDown(0)) // Click izquierdo o botón de disparo
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = lastMoveDirection * bulletSpeed;
            }
        }
    }
}
