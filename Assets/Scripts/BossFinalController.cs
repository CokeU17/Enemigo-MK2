using UnityEngine;
using UnityEngine.AI;

public class BossFinalController : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 7f;
    public float meleeRange = 2f;
    public float rangedRange = 5f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);

            if (distance <= meleeRange)
            {
                Debug.Log("Ataque MELEE");
                // Aqu� puedes activar animaci�n o l�gica de ataque cuerpo a cuerpo
            }
            else if (distance <= rangedRange)
            {
                Debug.Log("Ataque RANGED");
                // Aqu� puedes activar animaci�n o l�gica de ataque a distancia
            }
        }
    }
}
