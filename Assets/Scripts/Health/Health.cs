using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth; // Cambiado a public para que sea accesible desde otros scripts
    private Animator anim;
    private EnemyMove enemyMoveScript; // Referencia al script EnemyMove
    private NavMeshAgent agent;
    private BoxCollider col;
    private Health health;


    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        enemyMoveScript = GetComponent<EnemyMove>();
        agent = GetComponent<NavMeshAgent>();
        col = GetComponent<BoxCollider>();
    }

    public void TakeDamage(int damage)
    {   
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetBool("dead", true);
        enemyMoveScript.enabled = false;
        agent.enabled = false;
        col.enabled = false;
        health.enabled = false;
    }
}
