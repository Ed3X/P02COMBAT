using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    // Establecemos el punto de respawn del jugador
    public Transform respawnPoint;

    // Tenemos un script para controlar la salud del jugador
    private Health playerHealth;

    // Al inicio del script, obtenemos el componente de salud del jugador
    void Start()
    {
        // Obtenemos el componente de salud del jugador
        playerHealth = GetComponent<Health>();
    }

    // En cada frame de la ejecución del juego, verificamos si la salud del jugador llega a cero o menos
    void Update()
    {
        // Si la salud del jugador llega a cero o menos, procedemos a respawnear
        if (playerHealth.currentHealth <= 0)
        {
            // Respawnear al jugador
            RespawnPlayer();
        }
    }

    // Cuando necesitamos respawnear al jugador, ejecutamos este método
    void RespawnPlayer()
    {
        // Colocamos al jugador en el punto de respawn establecido
        transform.position = respawnPoint.position;

        // Reiniciamos la salud del jugador (y otras variables de estado si es necesario) al máximo
        playerHealth.currentHealth = playerHealth.maxHealth;
    }
}
