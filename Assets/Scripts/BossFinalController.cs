using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossFinalController : MonoBehaviour
{
    public enum Estado { Persiguiendo, Cansado }
    private Estado estadoActual = Estado.Persiguiendo;

    public Transform player;
    public float detectionRange = 7f;
    public float meleeRange = 2f;
    public float rangedRange = 5f;

    private NavMeshAgent agent;
    private float tiempoCorriendo = 0f;
    public float tiempoMaximoCorriendo = 5f;
    public float tiempoCansado = 4f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public GameObject fallingBallPrefab;
    public float spawnInterval = 1f;
    public int ballsPerCycle = 5;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    private bool disparando = false;
    private bool invocandoBolas = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position);

        if (estadoActual == Estado.Persiguiendo)
        {
            if (distancia <= detectionRange)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);

                // Solo cuenta tiempo si se está moviendo efectivamente
                if (agent.velocity.magnitude > 0.1f)
                    tiempoCorriendo += Time.deltaTime;

                // Disparo solo si está en rango y no muy cerca
                if (distancia <= rangedRange && distancia > meleeRange && !disparando)
                {
                    StartCoroutine(DisparoRango());
                }

                // Si alcanzó el tiempo máximo, cambiar a cansado
                if (tiempoCorriendo >= tiempoMaximoCorriendo)
                {
                    StartCoroutine(EntrarEnCansancio());
                }
            }
            else
            {
                agent.isStopped = true;
            }
        }

        else if (estadoActual == Estado.Cansado)
        {
            agent.isStopped = true;
            if (!invocandoBolas)
                StartCoroutine(InvocarBolas());
        }
    }

    IEnumerator DisparoRango()
    {
        // Solo si está persiguiendo
        if (estadoActual != Estado.Persiguiendo) yield break;

        disparando = true;

        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bala = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Vector2 direccion = (player.position - transform.position).normalized;
            bala.GetComponent<Rigidbody2D>().linearVelocity = direccion * 5f;
        }

        yield return new WaitForSeconds(1f);
        disparando = false;
    }

    IEnumerator EntrarEnCansancio()
    {
        estadoActual = Estado.Cansado;
        tiempoCorriendo = 0f;
        invocandoBolas = false;
        yield return new WaitForSeconds(tiempoCansado);
        estadoActual = Estado.Persiguiendo;
    }

    IEnumerator InvocarBolas()
    {
        invocandoBolas = true;

        for (int i = 0; i < ballsPerCycle; i++)
        {
            Vector2 posicionAleatoria = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            Instantiate(fallingBallPrefab, posicionAleatoria, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
