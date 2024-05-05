using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaSwit : MonoBehaviour
{
    public GameObject[] awpAssets;
    public GameObject[] ak47Assets;
    public GameObject[] karambitAssets;

    private bool showingAWP = false;
    private bool showingKarambit = false;

    void Start()
    {
        // Al inicio, mostramos los activos de AK47 y ocultamos los de AWP y karambit
        MostrarArmaActual();
    }

    void Update()
    {
        // Cambiar entre AK47, AWP y Karambit al presionar las teclas asignadas
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            showingAWP = false; // Mostrar AK47
            showingKarambit = false; // Ocultar Karambit
            MostrarArmaActual(); // Mostrar el arma actual basado en el nuevo estado
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            showingAWP = true; // Mostrar AWP
            showingKarambit = false; // Ocultar Karambit
            MostrarArmaActual(); // Mostrar el arma actual basado en el nuevo estado
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            showingAWP = false; // Ocultar AK47
            showingKarambit = true; // Mostrar Karambit
            MostrarKarambit(); // Mostrar el Karambit
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
            obj.SetActive(!showingAWP && !showingKarambit);
        }
        foreach (GameObject obj in karambitAssets)
        {
            obj.SetActive(showingKarambit);
        }
    }

    void MostrarKarambit()
    {
        // Mostrar los activos del karambit
        foreach (GameObject obj in karambitAssets)
        {
            obj.SetActive(showingKarambit);
        }

        // Ocultar los activos de las otras armas
        foreach (GameObject obj in awpAssets)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in ak47Assets)
        {
            obj.SetActive(false);
        }
    }
}
