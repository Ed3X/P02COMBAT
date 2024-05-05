using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    // Prefab del pétalo y del enemigo
    public GameObject petalPrefab;
    public GameObject enemyPrefab;

    // Número de pétalos y radio de disposición radial
    public int numberOfPetals = 6;
    public float radius = 2f;

    // Retraso para spawnear al enemigo
    public float enemySpawnDelay = 10f;

    // Contador para controlar el tiempo desde el último spawn
    private float timeSinceLastSpawn = 0f;

    void Start()
    {
        // Al iniciar, spawneamos los pétalos
        SpawnPetals();
    }

    void Update()
    {
        // Contamos el tiempo desde el último spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Si ha pasado el tiempo de retraso, spawneamos un enemigo
        if (timeSinceLastSpawn >= enemySpawnDelay)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f; // Reiniciamos el contador de tiempo
        }
    }

    void SpawnPetals()
    {
        // Calculamos el ángulo entre cada pétalo
        float angleStep = 360f / numberOfPetals;

        // Instanciamos y posicionamos cada pétalo alrededor del centro
        for (int i = 0; i < numberOfPetals; i++)
        {
            float angle = i * angleStep;
            Vector3 spawnPosition = transform.position + Quaternion.Euler(0, angle, 0) * (Vector3.forward * radius);
            Instantiate(petalPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void SpawnEnemy()
    {
        // Instanciamos al enemigo en la posición del spawner
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
