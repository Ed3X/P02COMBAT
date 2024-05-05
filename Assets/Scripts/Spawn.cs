using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    public GameObject petalPrefab;
    public GameObject enemyPrefab; // Prefab del enemigo
    public int numberOfPetals = 6; // N�mero de p�talos
    public float radius = 2f; // Radio de la disposici�n radial
    public float enemySpawnDelay = 10f; // Retraso para spawnear al enemigo

    private float timeSinceLastSpawn = 0f;

    void Start()
    {
        SpawnPetals();
    }

    void Update()
    {
        // Contar el tiempo desde el �ltimo spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Si ha pasado el tiempo de retraso, spawneamos un enemigo
        if (timeSinceLastSpawn >= enemySpawnDelay)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f; // Reiniciar el contador de tiempo
        }
    }

    void SpawnPetals()
    {
        // Calcular el �ngulo entre cada p�talo
        float angleStep = 360f / numberOfPetals;

        // Instanciar y posicionar cada p�talo alrededor del centro
        for (int i = 0; i < numberOfPetals; i++)
        {
            float angle = i * angleStep;
            Vector3 spawnPosition = transform.position + Quaternion.Euler(0, angle, 0) * (Vector3.forward * radius);
            Instantiate(petalPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void SpawnEnemy()
    {
        // Instanciar el enemigo en la posici�n del spawner
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
