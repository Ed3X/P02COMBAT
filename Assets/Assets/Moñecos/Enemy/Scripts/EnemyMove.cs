using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public Transform[] waypoints; // Array de waypoints
    private int currentWaypointIndex = 0; // Índice del waypoint actual

    public Animator ani;
    public NavMeshAgent agent;
    public float distancia_ataque;
    public GameObject target;
    public bool atacando;
    public float radio_vision;

    void Start()
    {
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;

        // Establecer el primer waypoint como destino inicial
        SetNextWaypoint();
    }

    void Update()
    {
        // Si estamos cerca del waypoint actual, avanzar al siguiente
        if (agent.remainingDistance <= agent.stoppingDistance && !atacando)
        {
            SetNextWaypoint();
        }

        if (Vector3.Distance(transform.position, target.transform.position) < radio_vision && !atacando)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            agent.enabled = true;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 200);
            transform.Translate(Vector3.forward * 1f * Time.deltaTime);
            atacando = false;
            ani.SetBool("attack", false);
            ani.SetBool("walk", false);
            ani.SetBool("run", true);
        }

        // Si estamos dentro del rango de ataque y no estamos atacando, atacar
        if (Vector3.Distance(transform.position, target.transform.position) < distancia_ataque)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 200);
            atacando = true;
            ani.SetBool("attack", true);
            ani.SetBool("walk", false);
            ani.SetBool("run", false);
            
        }
        else
        {
            agent.enabled = true;
            atacando = false;
            ani.SetBool("attack", false);
        }
    }

    void SetNextWaypoint()
    {
        ani.SetBool("walk", true);
        // Incrementar el índice del waypoint actual
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        // Establecer el siguiente waypoint como destino
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}
