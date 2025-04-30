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

    public GameObject pigClonePrefab;
    public int clonesPerCycle = 3;

    private bool disparando = false;
    private bool invocandoClones = false;
    private SpriteRenderer sr;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position);

        if (estadoActual == Estado.Persiguiendo)
        {
            sr.color = Color.white;

            if (distancia <= detectionRange)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);

                if (agent.velocity.magnitude > 0.1f)
                    tiempoCorriendo += Time.deltaTime;

                if (distancia <= rangedRange && distancia > meleeRange && !disparando)
                    StartCoroutine(DisparoRango());

                if (tiempoCorriendo >= tiempoMaximoCorriendo)
                    StartCoroutine(EntrarEnCansancio());
            }
            else
            {
                agent.isStopped = true;
            }
        }
        else if (estadoActual == Estado.Cansado)
        {
            sr.color = Color.red;
            agent.isStopped = true;

            if (!invocandoClones)
                StartCoroutine(InvocarClones());
        }
    }

    IEnumerator DisparoRango()
    {
        if (estadoActual != Estado.Persiguiendo) yield break;

        disparando = true;

        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bala = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Vector2 direccion = (player.position - transform.position).normalized;

            Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = direccion * 5f;

            Destroy(bala, 3f);
        }

        yield return new WaitForSeconds(1f);
        disparando = false;
    }

    IEnumerator EntrarEnCansancio()
    {
        estadoActual = Estado.Cansado;
        tiempoCorriendo = 0f;
        invocandoClones = false;

        yield return new WaitForSeconds(tiempoCansado);
        estadoActual = Estado.Persiguiendo;
    }

    IEnumerator InvocarClones()
    {
        invocandoClones = true;

        for (int i = 0; i < clonesPerCycle; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 2f;
            Vector2 spawnPosition = (Vector2)transform.position + offset;

            Instantiate(pigClonePrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }

        invocandoClones = false;
    }
}
