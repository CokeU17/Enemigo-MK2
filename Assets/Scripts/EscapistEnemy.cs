using UnityEngine;
using UnityEngine.AI;

public class EscapistEnemy : MonoBehaviour
{
    public Transform player; // El objetivo que evitamos
    public float escapeDistance = 5f; // Distancia a la que el enemigo empieza a huir
    public float moveRadius = 3f; // Cuánto se aleja

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < escapeDistance)
        {
            // Calculamos una dirección opuesta al jugador
            Vector3 escapeDirection = (transform.position - player.position).normalized;
            Vector3 newDestination = transform.position + escapeDirection * moveRadius;

            agent.SetDestination(newDestination);
        }
    }
}
