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
    private float nextShootTime = 1f; // Momento en el que se podrá disparar nuevamente
    public float shootRange = 10f; // Rango máximo de disparo
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
        Vector3 targetPosition = target.transform.position + Vector3.up * 1f; // Ajustar la altura del objetivo
        Vector3 direction = (targetPosition - firePoint.position).normalized;

        // Dibujar la línea del raycast en la dirección del disparo (siempre visible)
        Debug.DrawRay(firePoint.position, direction * shootRange, Color.red);

        // Si el jugador está dentro del rango de visión
        if (Vector3.Distance(transform.position, target.transform.position) < radio_vision)
        {
            // Detener la patrulla
            isPatrolling = false;

            // Girar hacia el jugador
            Vector3 lookPos = target.transform.position - transform.position;
            lookPos.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 200f * Time.deltaTime);

            // Si el jugador está dentro del rango de ataque
            if (Vector3.Distance(transform.position, target.transform.position) < distancia_ataque)
            {
                // Disparar al jugador
                ani.SetBool("attack", true);
                ani.SetBool("run", false);
                ani.SetBool("walk", false);
                if (Time.time > nextShootTime)
                {
                    Shoot(); // Pasar la dirección al método Shoot
                    nextShootTime = Time.time + 2f / shootRate; // Calculamos el próximo momento de disparo
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
            // Si el jugador está fuera del rango de visión, continuar patrullando
            isPatrolling = true;
            agent.speed = velocidad_patrulla;
        }

        if (isPatrolling)
        {
            // Si está patrullando, continuar con la patrulla
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

    private void Shoot()
    {
        Vector3 targetPosition = target.transform.position + Vector3.up * 0.8f; // Ajustar la altura del objetivo
        Vector3 direction = (targetPosition - firePoint.position).normalized;

        // Crear un raycast para detectar al jugador
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, direction, out hit, shootRange))
        {
            Debug.Log("Colision");
            // Comprobar si el objeto golpeado es el jugador
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Colision1");
                // Instanciar una bala en el punto de disparo
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

                // Calcular la rotación de la bala hacia el jugador
                Quaternion rotation = Quaternion.LookRotation(targetPosition - bullet.transform.position);
                bullet.transform.rotation = rotation;

                // Obtener el componente Rigidbody de la bala
                Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();

                // Aplicar velocidad a la bala en dirección al jugador
                bulletRB.velocity = direction * bulletSpeed;
            }
        }
    }

}