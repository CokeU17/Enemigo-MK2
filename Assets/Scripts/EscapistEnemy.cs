using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EscapistEnemy : MonoBehaviour
{
    public Transform player;
    public float escapeDistance = 5f;
    public float moveRadius = 3f;
    public float activationTime = 3f;  // Tiempo para pasar de Cansado a Activo
    public float exhaustionTime = 5f;  // Tiempo para pasar de Activo a Cansado

    private NavMeshAgent agent;
    private EnemyShooter shooter;
    private SpriteRenderer spriteRenderer;

    private enum EnemyState { Activo, Cansado }
    private EnemyState currentState;

    private Coroutine shootingCoroutine;
    private Coroutine stateCoroutine;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        shooter = GetComponent<EnemyShooter>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        EnterActiveState();
    }

    private void Update()
    {
        if (currentState == EnemyState.Activo)
        {
            // Huir del jugador
            Vector3 direction = (transform.position - player.position).normalized;
            Vector3 targetPosition = transform.position + direction * moveRadius;
            agent.SetDestination(targetPosition);
        }
        else if (currentState == EnemyState.Cansado)
        {
            agent.ResetPath();
        }
    }

    private void EnterActiveState()
    {
        currentState = EnemyState.Activo;

        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;

        if (shootingCoroutine != null)
            StopCoroutine(shootingCoroutine);
        shootingCoroutine = StartCoroutine(ShootWhileActive());

        if (stateCoroutine != null)
            StopCoroutine(stateCoroutine);
        stateCoroutine = StartCoroutine(SwitchStateAfterSeconds(EnemyState.Cansado, exhaustionTime));
    }

    private void EnterTiredState()
    {
        currentState = EnemyState.Cansado;

        if (spriteRenderer != null)
            spriteRenderer.color = Color.gray;

        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }

        if (stateCoroutine != null)
            StopCoroutine(stateCoroutine);
        stateCoroutine = StartCoroutine(SwitchStateAfterSeconds(EnemyState.Activo, activationTime));
    }

    private IEnumerator SwitchStateAfterSeconds(EnemyState nextState, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (nextState == EnemyState.Activo)
            EnterActiveState();
        else
            EnterTiredState();
    }

    private IEnumerator ShootWhileActive()
    {
        while (currentState == EnemyState.Activo)
        {
            if (shooter != null)
            {
                shooter.Shoot();
            }
            yield return new WaitForSeconds(shooter.fireRate);
        }
    }
}
