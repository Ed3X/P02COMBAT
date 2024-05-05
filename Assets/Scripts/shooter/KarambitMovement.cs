using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarambitMovement : MonoBehaviour
{
    public float rotationAngle = 45f; // Ángulo de rotación
    public float rotationDuration = 0.5f; // Duración de la rotación

    private bool isRotating = false;

    void Update()
    {
        // Detecta clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0) && !isRotating)
        {
            // Inicia la rotación
            StartCoroutine(RotateObject());
        }
    }

    IEnumerator RotateObject()
    {
        isRotating = true;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, rotationAngle);

        float timeElapsed = 0;

        while (timeElapsed < rotationDuration)
        {
            // Interpolación entre las rotaciones inicial y final
            Quaternion newRotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, rotationAngle, timeElapsed / rotationDuration));
            transform.rotation = startRotation * newRotation;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que la rotación finalice correctamente
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        yield return new WaitForSeconds(0.1f); // Espera pequeña para evitar problemas de "stuck"

        // Rotación de vuelta
        targetRotation = Quaternion.Euler(0, 0, 0);

        timeElapsed = 0;

        while (timeElapsed < rotationDuration)
        {
            // Interpolación entre las rotaciones inicial y final
            Quaternion newRotation = Quaternion.Euler(0, 0, Mathf.Lerp(rotationAngle, 0, timeElapsed / rotationDuration));
            transform.rotation = startRotation * newRotation;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que la rotación finalice correctamente
        transform.rotation = startRotation;

        isRotating = false;
    }
}
