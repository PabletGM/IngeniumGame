using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComportamientoNaveEspacio : MonoBehaviour
{


    [SerializeField] private GameObject botonEscena;
    [SerializeField] private float amplitudMovimiento = 1f; // Amplitud del movimiento hacia arriba y hacia abajo
    [SerializeField] private float velocidadMovimiento = 0.4f; // Velocidad de movimiento
    [SerializeField] private float limiteSuperior = 2.0f; // Límite superior de movimiento
    [SerializeField] private float limiteInferior = -2.0f; // Límite inferior de movimiento
    [SerializeField] private float tiempoCambioDireccion = 2.0f; // Tiempo entre cambios de dirección (en segundos)
    [SerializeField] private float tiempoCambioAleatorio = 3.0f; // Tiempo entre cambios aleatorios (en segundos)

    private Vector3 posicionInicial;
    private bool moviendoHaciaArriba = true; // Variable de control de dirección
    private bool moviendoHaciaAdelante = true; // Variable de control de dirección

    private float tiempoUltimoCambioDireccion;
    private float tiempoUltimoCambioAleatorio;

    private void Start()
    {

        // Inicializa los tiempos de los últimos cambios de dirección y aleatorio
        tiempoUltimoCambioDireccion = Time.time;
        tiempoUltimoCambioAleatorio = Time.time;
        Invoke("DesbloquearBoton", 3f);
    }

    private void Update()
    {

        // Guarda la posición inicial del sprite
        posicionInicial = transform.position;

        // Calcula el tiempo desde el último cambio de dirección
        float tiempoDesdeUltimoCambioDireccion = Time.time - tiempoUltimoCambioDireccion;

        // Cambia de dirección si ha pasado el tiempo especificado
        if (tiempoDesdeUltimoCambioDireccion >= tiempoCambioDireccion)
        {
            moviendoHaciaArriba = !moviendoHaciaArriba; // Cambia la dirección
            tiempoUltimoCambioDireccion = Time.time; // Actualiza el tiempo del último cambio de dirección
        }

        // Calcula el tiempo desde el último cambio aleatorio
        float tiempoDesdeUltimoCambioAleatorio = Time.time - tiempoUltimoCambioAleatorio;

        // Cambia aleatoriamente la dirección si ha pasado el tiempo especificado
        if (tiempoDesdeUltimoCambioAleatorio >= tiempoCambioAleatorio)
        {
            moviendoHaciaAdelante = Random.value < 0.5f; // Cambia aleatoriamente la dirección hacia adelante o atrás
            tiempoUltimoCambioAleatorio = Time.time; // Actualiza el tiempo del último cambio aleatorio
        }

        // Calcula la posición vertical basada en el tiempo y dirección
        float offsetVertical = Mathf.Sin(Time.time * velocidadMovimiento) * amplitudMovimiento;
        if (!moviendoHaciaArriba)
        {
            offsetVertical *= -1.0f; // Invierte la dirección si no se mueve hacia arriba
        }

        // Calcula la nueva posición vertical
        float nuevaPosicionY = posicionInicial.y + offsetVertical;

        // Limita la posición vertical dentro de los límites superior e inferior
        nuevaPosicionY = Mathf.Clamp(nuevaPosicionY, limiteInferior, limiteSuperior);

        // Calcula la nueva posición horizontal basada en la dirección aleatoria
        float nuevaPosicionX = posicionInicial.x;

        if (moviendoHaciaAdelante)
        {
            nuevaPosicionX += velocidadMovimiento * Time.deltaTime;
        }
        else
        {
            nuevaPosicionX -= velocidadMovimiento * Time.deltaTime;
        }

        // Actualiza la posición del sprite
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
