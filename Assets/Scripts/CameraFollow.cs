using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // El objeto a seguir (jugador)
    public float smoothSpeed = 5f;  // Qué tan suave será el movimiento

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
