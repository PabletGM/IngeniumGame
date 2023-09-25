using UnityEngine;
using UnityEngine.SceneManagement;

public class MoverCamaraArriba : MonoBehaviour
{
    public float velocidad = 2.0f; // Velocidad de movimiento de la c�mara hacia arriba
    private float limiteSuperior; // L�mite superior de movimiento de la c�mara
    private Vector3 nuevaPosicion;

    private bool zoomOut = false;
    private float aumentoMaximo = 8f; // Tama�o m�ximo al que quieres aumentar la c�mara
    private float velocidadAumento = 0.5f; // Velocidad a la que aumenta el tama�o de la c�mara
    public Camera camara;


    private void Start()
    {
        //si es salida del planeta
        if(SceneManager.GetActiveScene().name == "SalidaTierra")
        {
            limiteSuperior = 20;
        }
        //si es llegada al planeta
        if (SceneManager.GetActiveScene().name == "LlegadaPlaneta")
        {
            limiteSuperior = 2.2f;
        }
    }
    private void Update()
    {
        #region MoverCamara
        // Obt�n la posici�n actual de la c�mara
        Vector3 posicionCamara = transform.position;
        //si es salida del planeta
        if (SceneManager.GetActiveScene().name == "SalidaTierra")
        {
            // Calcula la nueva posici�n de la c�mara
             nuevaPosicion = posicionCamara + Vector3.up * velocidad * Time.deltaTime;
            // Limita la posici�n de la c�mara al l�mite superior
            nuevaPosicion.y = Mathf.Min(nuevaPosicion.y, limiteSuperior);
        }
        //si es llegada al planeta
        else
        {
            // Calcula la nueva posici�n de la c�mara
            nuevaPosicion = posicionCamara - Vector3.up * velocidad * Time.deltaTime;
            // Limita la posici�n de la c�mara al l�mite superior
            nuevaPosicion.y = Mathf.Max(nuevaPosicion.y, limiteSuperior);
        }
        // Establece la nueva posici�n de la c�mara
        transform.position = nuevaPosicion;

        #endregion

        #region ZoomOut
            if(zoomOut)
            {
                // Aumenta el tama�o gradualmente mientras no haya alcanzado el l�mite
                if (camara.orthographicSize < aumentoMaximo)
                {
                    float nuevoTama�o = Mathf.Lerp(camara.orthographicSize, aumentoMaximo, velocidadAumento * Time.deltaTime);
                    camara.orthographicSize = nuevoTama�o;
                }
                //si ha alcanzado el limite
                if (camara.orthographicSize > 7.5)
                {
                    zoomOut = false;
                    //pasar siguiente escena
                }
            }
        #endregion
    }


    public void ActivarZoomOut()
    {
        zoomOut = true;
    }
}
