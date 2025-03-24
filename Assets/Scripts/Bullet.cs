using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime); // Destruir la bala tras cierto tiempo
    }

    void Update()
    {
        if (rb != null)
        {
            rb.linearVelocity = transform.right * speed;
        }
    }

    // Opcional: si quieres que se destruya al chocar con algo
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
