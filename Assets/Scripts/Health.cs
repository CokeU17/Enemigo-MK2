using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth { get; private set; }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (this == null || gameObject == null) return;

        currentHealth -= amount;

        if (currentHealth <= 0 && gameObject != null)
        {
            Die();
        }
    }

    void Die()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
