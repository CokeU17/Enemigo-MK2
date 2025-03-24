using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform player;

    [Tooltip("Balas por segundo")]
    public float fireRate = 1f;

    [Tooltip("Velocidad de la bala")]
    public float bulletSpeed = 5f;

    private float fireCooldown = 0f;

    void Update()
    {
        // Temporizador de disparo autom�tico
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    public void Shoot()
    {
        // Validaciones de seguridad
        if (bulletPrefab == null || firePoint == null || player == null) return;

        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        fireCooldown = 1f / fireRate;
    }

    public bool CanShoot()
    {
        return fireCooldown <= 0f;
    }
}
