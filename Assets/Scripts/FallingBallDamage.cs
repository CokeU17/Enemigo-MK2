using UnityEngine;

public class FallingBallDamage : MonoBehaviour
{
    public int damage = 1;
    public float pushForce = 5f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Aqu� puedes reducirle vida si tu jugador tiene una variable p�blica "vida"
            // collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);

            // Empujarlo
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }

            // Destruir la bola despu�s del impacto
            Destroy(gameObject);
        }
        else
        {
            // Si choca con el suelo o algo m�s, tambi�n se destruye
            Destroy(gameObject, 1f);
        }
    }
}
