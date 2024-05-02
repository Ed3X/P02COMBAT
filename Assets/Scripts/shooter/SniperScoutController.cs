using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperScopeController : MonoBehaviour
{
    public GameObject sniperScope;
    public GameObject awp;
    public Camera playerCamera;
    public RawImage aimDefault;
    private float originalFOV = 45f;
    private float zoomedFOV = 20f;

    void Start()
    {
        // Al inicio, asegúrate de que el SniperScope esté oculto
        sniperScope.SetActive(false);
    }

    void Update()
    {
        // Si se hace clic derecho, cambia la visibilidad del SniperScope
        if (Input.GetMouseButtonDown(1))
        {
            bool scopeActive = !sniperScope.activeSelf;
            sniperScope.SetActive(scopeActive);

            // Si el scope está activo, oculta el GameObject "awp", de lo contrario, muéstralo
            awp.SetActive(!scopeActive);

            // Cambia el campo de visión de la cámara
            if (playerCamera != null)
            {
                playerCamera.fieldOfView = scopeActive ? zoomedFOV : originalFOV;
            }

            // Oculta o muestra el Raw Image "AimDefault" según el estado del scope
            if (aimDefault != null)
            {
                aimDefault.enabled = !scopeActive;
            }
        }
    }
}
