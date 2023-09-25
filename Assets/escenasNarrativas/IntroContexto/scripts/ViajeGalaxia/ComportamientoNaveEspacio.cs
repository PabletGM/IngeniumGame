using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComportamientoNaveEspacio : MonoBehaviour
{


    [SerializeField] private GameObject botonEscena;
    [SerializeField] private float amplitudMovimiento = 1f; // Amplitud del movimiento hacia arriba y hacia abajo
    [SerializeField] private float velocidadMovimiento = 0.4f; // Velocidad de movimiento
    [SerializeField] private float limiteSuperior = 2.0f; // L�mite superior de movimiento
    [SerializeField] private float limiteInferior = -2.0f; // L�mite inferior de movimiento
    [SerializeField] private float tiempoCambioDireccion = 2.0f; // Tiempo entre cambios de direcci�n (en segundos)
    [SerializeField] private float tiempoCambioAleatorio = 3.0f; // Tiempo entre cambios aleatorios (en segundos)

    private Vector3 posicionInicial;
    private bool moviendoHaciaArriba = true; // Variable de control de direcci�n
    private bool moviendoHaciaAdelante = true; // Variable de control de direcci�n

    private float tiempoUltimoCambioDireccion;
    private float tiempoUltimoCambioAleatorio;

    private void Start()
    {

        // Inicializa los tiempos de los �ltimos cambios de direcci�n y aleatorio
        tiempoUltimoCambioDireccion = Time.time;
        tiempoUltimoCambioAleatorio = Time.time;
        Invoke("DesbloquearBoton", 3f);
    }

    private void Update()
    {

        // Guarda la posici�n inicial del sprite
        posicionInicial = transform.position;

        // Calcula el tiempo desde el �ltimo cambio de direcci�n
        float tiempoDesdeUltimoCambioDireccion = Time.time - tiempoUltimoCambioDireccion;

        // Cambia de direcci�n si ha pasado el tiempo especificado
        if (tiempoDesdeUltimoCambioDireccion >= tiempoCambioDireccion)
        {
            moviendoHaciaArriba = !moviendoHaciaArriba; // Cambia la direcci�n
            tiempoUltimoCambioDireccion = Time.time; // Actualiza el tiempo del �ltimo cambio de direcci�n
        }

        // Calcula el tiempo desde el �ltimo cambio aleatorio
        float tiempoDesdeUltimoCambioAleatorio = Time.time - tiempoUltimoCambioAleatorio;

        // Cambia aleatoriamente la direcci�n si ha pasado el tiempo especificado
        if (tiempoDesdeUltimoCambioAleatorio >= tiempoCambioAleatorio)
        {
            moviendoHaciaAdelante = Random.value < 0.5f; // Cambia aleatoriamente la direcci�n hacia adelante o atr�s
            tiempoUltimoCambioAleatorio = Time.time; // Actualiza el tiempo del �ltimo cambio aleatorio
        }

        // Calcula la posici�n vertical basada en el tiempo y direcci�n
        float offsetVertical = Mathf.Sin(Time.time * velocidadMovimiento) * amplitudMovimiento;
        if (!moviendoHaciaArriba)
        {
            offsetVertical *= -1.0f; // Invierte la direcci�n si no se mueve hacia arriba
        }

        // Calcula la nueva posici�n vertical
        float nuevaPosicionY = posicionInicial.y + offsetVertical;

        // Limita la posici�n vertical dentro de los l�mites superior e inferior
        nuevaPosicionY = Mathf.Clamp(nuevaPosicionY, limiteInferior, limiteSuperior);

        // Calcula la nueva posici�n horizontal basada en la direcci�n aleatoria
        float nuevaPosicionX = posicionInicial.x;

        if (moviendoHaciaAdelante)
        {
            nuevaPosicionX += velocidadMovimiento * Time.deltaTime;
        }
        else
        {
            nuevaPosicionX -= velocidadMovimiento * Time.deltaTime;
        }

        // Actualiza la posici�n del sprite
        transform.position = new Vector3(nuevaPosicionX, nuevaPosicionY, posicionInicial.z);
    }

    private void DesbloquearBoton()
    {
        botonEscena.SetActive(true);
    }


    public void PasarEscena()
    {
        SceneManager.LoadScene("LlegadaPlaneta");
    }
}
