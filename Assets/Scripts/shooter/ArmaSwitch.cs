using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaSwit : MonoBehaviour
{
    public GameObject[] awpAssets;
    public GameObject[] ak47Assets;

    private bool showingAWP = false;

    void Start()
    {
        // Al inicio, mostramos los activos de AK47 y ocultamos los de AWP
        MostrarArmaActual();
    }

    void Update()
    {
        // Cambiar entre AK47 y AWP al presionar la tecla asignada
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            showingAWP = false; // Mostrar AK47
            MostrarArmaActual(); // Mostrar el arma actual basado en el nuevo estado
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            showingAWP = true; // Mostrar AWP
            MostrarArmaActual(); // Mostrar el arma actual basado en el nuevo estado
        }
    }

    void MostrarArmaActual()
    {
        // Mostrar los activos de acuerdo al estado actual
        foreach (GameObject obj in awpAssets)
        {
            obj.SetActive(showingAWP);
        }
        foreach (GameObject obj in ak47Assets)
        {
            obj.SetActive(!showingAWP);
        }
    }
}
