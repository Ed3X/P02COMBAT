using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    public GameObject petalPrefab;
    public int numberOfPetals = 6; // Número de pétalos
    public float radius = 2f; // Radio de la disposición radial

    void Start()
    {
        SpawnPetals();
    }

    void SpawnPetals()
    {
        // Calcular el ángulo entre cada pétalo
        float angleStep = 360f / numberOfPetals;

        // Instanciar y posicionar cada pétalo alrededor del centro
        for (int i = 0; i < numberOfPetals; i++)
        {
            float angle = i * angleStep;
            Vector3 spawnPosition = transform.position + Quaternion.Euler(0, angle, 0) * (Vector3.forward * radius);
            Instantiate(petalPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
