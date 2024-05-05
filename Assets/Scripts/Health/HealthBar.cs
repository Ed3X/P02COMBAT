    using System.Collections;
    using System.Collections.Generic;
    using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health healthScript; // Referencia al script de salud del enemigo
    private Image healthBarImage; // Referencia a la imagen de la barra de salud

    private void Start()
    {
        // Obtener la referencia al componente Image
        healthBarImage = GetComponent<Image>();

        // Asegurarse de que se proporciona la referencia al script de salud
        if (healthScript == null)
        {
            Debug.LogError("Falta la referencia al script de salud en el objeto HealthBar.");
        }
    }

    private void Update()
    {
        // Asegurarse de que la referencia al script de salud no sea nula
        if (healthScript != null)
        {
            // Calcular el fill amount basado en la salud actual y máxima
            float fillAmount = (float)healthScript.currentHealth / healthScript.maxHealth;

            // Establecer el fill amount en la barra de salud
            healthBarImage.fillAmount = fillAmount;
        }
    }
}
