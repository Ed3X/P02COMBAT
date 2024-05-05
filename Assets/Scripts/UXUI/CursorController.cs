using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    void Start()
    {
        // Ocultar y bloquear el cursor del ratón
        Cursor.lockState = CursorLockMode.Locked;
        // Opcionalmente, puedes ocultar el cursor visible
        Cursor.visible = false;
    }

    // Puedes volver a habilitar el cursor visible en cualquier momento si es necesario
    void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}