using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public Transform[] waypoints; // Array de waypoints
    private Transform currentWaypoint; // Waypoint actual
    private Animator ani;
    private NavMeshAgent agent;
    public float distancia_ataque;
    public GameObject target;
    public float radio_vision;
    public float velocidad_patrulla = 0.5f;
    public float velocidad_run = 1f;
    private bool isPatrolling = true;

    // Variables para el disparo
    public float shootRate = 1f; // Frecuencia de disparo en segundos
    private float nextShootTime = 1f; // Momento en el que se podr� disparar nuevamente
    public float shootRange = 10f; // Rango m�ximo de disparo
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint; // Punto de origen del disparo
    public float bulletSpeed = 10f; // Velocidad de la bala

    void Start()
    {
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        currentWaypoint = GetRandomWaypoint();
        SetDestination(currentWaypoint.position);
    }

    void Update()
    {
        Vector3 targetPosition = target.transform.position + Vector3.up * 0.8f; // Ajustar la altura del objetivo
        Vector3 direction = (targetPosition - firePoint.position).normalized;

        // Si el jugador est� dentro del rango de visi�n
        if (Vector3.Distance(transform.position, target.transform.position) < radio_vision)
        {
            // Detener la patrulla
            isPatrolling = false;

            // Girar hacia el jugador
            Vector3 lookPos = target.transform.position - transform.position;
            lookPos.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 200f * Time.deltaTime);

            // Si el jugador est� dentro del rango de ataque
            if (Vector3.Distance(transform.position, target.transform.position) < distancia_ataque)
            {
                // Disparar al jugador
                ani.SetBool("attack", true);
                ani.SetBool("run", false);
                ani.SetBool("walk", false);
                if (Time.time > nextShootTime)
                {
                    Shoot(direction);
                    nextShootTime = Time.time + 2f / shootRate; // Calculamos el pr�ximo momento de disparo
                }
            }
            else
            {
                // Moverse hacia el jugador
                ani.SetBool("run", true);
                ani.SetBool("walk", false);
                ani.SetBool("attack", false);
                SetDestination(target.transform.position);
                agent.speed = velocidad_run;
            }
        }
        else
        {
            // Si el jugador est� fuera del rango de visi�n, continuar patrullando
            isPatrolling = true;
            agent.speed = velocidad_patrulla;
        }

        if (isPatrolling)
        {
            // Si est� patrullando, continuar con la patrulla
            ani.SetBool("walk", true);
            ani.SetBool("run", false);
            ani.SetBool("attack", false);

            if (ReachedWaypoint())
            {
                currentWaypoint = GetRandomWaypoint();
                SetDestination(currentWaypoint.position);
            }
        }
    }

    private bool ReachedWaypoint()
    {
        return Vector3.Distance(transform.position, currentWaypoint.position) <= agent.stoppingDistance;
    }

    private Transform GetRandomWaypoint()
    {
        return waypoints[Random.Range(0, waypoints.Length)];
    }

    private void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    private void Shoot(Vector3 direction)
    {
        // Instanciar una bala en el punto de disparo
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Obtener el componente Rigidbody de la bala
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();

        // Aplicar velocidad a la bala en direcci�n al jugador
        bulletRB.velocity = direction * bulletSpeed;
    }
}
