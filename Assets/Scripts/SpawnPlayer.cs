using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public Transform respawnPoint;
    private Health playerHealth; // Suponiendo que tienes un script para el control de la salud del jugador

    void Start()
    {
        // Obtener el componente de salud del jugador
        playerHealth = GetComponent<Health>();
    }

    void Update()
    {
        // Si la salud del jugador llega a cero o menos, respawnear
        if (playerHealth.currentHealth <= 0)
        {
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        // Establecer la posición del jugador en el punto de respawn
        transform.position = respawnPoint.position;

        // Reiniciar la salud del jugador (u otras variables de estado si es necesario)
        playerHealth.currentHealth = playerHealth.maxHealth;
    }
}
