using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float currentTime = 0f;
    private int minutes = 0;
    private int seconds = 0;

    void Start()
    {
        // Iniciar el contador cuando el juego comienza
        currentTime = 0f;

        // Mostrar el tiempo inicial en el formato 0:00
        UpdateTimeText();
    }

    void Update()
    {
        // Incrementar el tiempo actual
        currentTime += Time.deltaTime;

        // Calcular minutos y segundos
        minutes = Mathf.FloorToInt(currentTime / 60f);
        seconds = Mathf.FloorToInt(currentTime % 60f);

        // Actualizar el texto mostrando el tiempo
        UpdateTimeText();
    }

    void UpdateTimeText()
    {
        // Formatear el texto mostrando el tiempo
        timeText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}
