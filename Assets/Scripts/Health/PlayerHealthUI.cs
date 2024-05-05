using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Health playerHealth; // Referencia al script de salud del jugador

    void Start()
    {
        // Asegurarse de que el TextMeshPro esté configurado
        if (healthText == null)
        {
            Debug.LogError("No se ha asignado un TextMeshPro para mostrar la salud del jugador.");
            return;
        }

        // Asegurarse de que el script de salud del jugador esté configurado
        if (playerHealth == null)
        {
            Debug.LogError("No se ha asignado un script de salud para el jugador.");
            return;
        }

        // Actualizar el TextMeshPro con la salud actual del jugador al inicio
        UpdateHealthText();
    }

    void Update()
    {
        // Si la salud del jugador cambia, actualizar el TextMeshPro
        if (playerHealth.currentHealth != int.Parse(healthText.text))
        {
            UpdateHealthText();
        }
    }

    void UpdateHealthText()
    {
        // Actualizar el TextMeshPro con la salud actual del jugador
        healthText.text = playerHealth.currentHealth.ToString();
    }
}
