using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 2f;

    void Start()
    {
        // Destruye la bala automáticamente después de cierto tiempo
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Mueve la bala hacia la derecha constantemente
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
