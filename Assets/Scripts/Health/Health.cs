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


    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        enemyMoveScript = GetComponent<EnemyMove>(); // Obteniendo el componente EnemyMove
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
        // Desactivar el componente EnemyMove
        enemyMoveScript.enabled = false;
        anim.SetBool("dead", true);
    }
}
