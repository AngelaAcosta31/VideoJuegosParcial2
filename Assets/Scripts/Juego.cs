using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juego : MonoBehaviour
{

    public delegate void CambioDeImagenVueltasHandler(int numeroImagen);
    public static event CambioDeImagenVueltasHandler OnCambioDeImagenVueltas;

    public delegate void CambioDeImagenPosicionHandler(int numeroImagen);
    public static event CambioDeImagenPosicionHandler OnCambioDeImagenPosicion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Lógica para determinar el número de imagen por vueltas
        int numeroImagenVueltas = CalcularNumeroImagenVueltas(); // Implementa tu lógica aquí

        // Emitir evento de cambio de imagen por vueltas
        if (OnCambioDeImagenVueltas != null)
        {
            OnCambioDeImagenVueltas(numeroImagenVueltas);
        }

        // Lógica para determinar el número de imagen por posición del jugador
        int numeroImagenPosicion = CalcularNumeroImagenPosicion(); // Implementa tu lógica aquí

        // Emitir evento de cambio de imagen por posición del jugador
        if (OnCambioDeImagenPosicion != null)
        {
            OnCambioDeImagenPosicion(numeroImagenPosicion);
        }
    }

    int CalcularNumeroImagenVueltas()
    {
        // lógica para calcular el número de imagen por vueltas aquí
        return 1; 
    }

    int CalcularNumeroImagenPosicion()
    {
        // lógica para calcular el número de imagen por posición del jugador aquí
        return 2; 
    }
}
