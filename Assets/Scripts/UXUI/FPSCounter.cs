using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public float updateInterval = 0.5f; // Intervalo de actualización en segundos
    private float accum = 0.0f; // Acumulador de frames por segundo
    private int frames = 0; // Número de frames
    private float timeleft; // Tiempo restante para la próxima actualización
    private TextMeshProUGUI fpsText; // Referencia al TextMeshProUGUI

    private void Start()
    {
        // Obtener la referencia al TextMeshProUGUI
        fpsText = GetComponent<TextMeshProUGUI>();
        if (fpsText == null)
        {
            Debug.LogError("FPSCounter necesita un componente TextMeshProUGUI adjuntado al mismo GameObject.");
            enabled = false;
            return;
        }

        timeleft = updateInterval;
    }

    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        // Actualizar el texto de FPS si es necesario
        if (timeleft <= 0.0)
        {
            int fps = Mathf.RoundToInt(accum / frames);
            fpsText.text = fps.ToString() + " FPS";

            // Reiniciar variables
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
}
