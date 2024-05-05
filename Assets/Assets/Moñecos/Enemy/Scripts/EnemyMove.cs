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
}
