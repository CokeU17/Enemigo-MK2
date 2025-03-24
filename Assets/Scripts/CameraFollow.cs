using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Asigna el jugador en el Inspector
    public float smoothSpeed = 5f; // Ajusta la velocidad de seguimiento

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
