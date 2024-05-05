using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    public int damageAmount = 10; // Cantidad de daño que se aplicará
    public float damageInterval = 1f; // Intervalo de tiempo entre cada daño
    public float damageDuration = 2f; // Duración total de daño

    private bool isDamaging = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDamaging)
        {
            isDamaging = true;
            InvokeRepeating("ApplyDamage", 0f, damageInterval); // Comenzar a aplicar daño repetidamente
            Invoke("StopDamage", damageDuration); // Detener el daño después de la duración especificada
        }
    }

    private void ApplyDamage()
    {
        // Aplicar daño al jugador
        Health playerHealth = FindObjectOfType<Health>(); // Encuentra el script de salud del jugador
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }

    private void StopDamage()
    {
        isDamaging = false;
        CancelInvoke("ApplyDamage"); // Detener la aplicación repetida de daño
    }
}
