using UnityEngine;
using UnityEngine.SceneManagement;

public class MoverCamaraArriba : MonoBehaviour
{
    public float velocidad = 2.0f; // Velocidad de movimiento de la cámara hacia arriba
    private float limiteSuperior; // Límite superior de movimiento de la cámara
    private Vector3 nuevaPosicion;

    private bool zoomOut = false;
    private float aumentoMaximo = 8f; // Tamaño máximo al que quieres aumentar la cámara
    private float velocidadAumento = 0.5f; // Velocidad a la que aumenta el tamaño de la cámara
    public Camera camara;


    [SerializeField]
    private GameObject spritePlaneta;


    [SerializeField]
    private GameObject naves;



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
        // Obtén la posición actual de la cámara
        Vector3 posicionCamara = transform.position;
        //si es salida del planeta
        if (SceneManager.GetActiveScene().name == "SalidaTierra")
        {
            // Calcula la nueva posición de la cámara
             nuevaPosicion = posicionCamara + Vector3.up * velocidad * Time.deltaTime;
            // Limita la posición de la cámara al límite superior
            nuevaPosicion.y = Mathf.Min(nuevaPosicion.y, limiteSuperior);

            
        }
        //si es llegada al planeta
        else
        {
            // Calcula la nueva posición de la cámara
            nuevaPosicion = posicionCamara - Vector3.up * velocidad * Time.deltaTime;
            // Limita la posición de la cámara al límite superior
            nuevaPosicion.y = Mathf.Max(nuevaPosicion.y, limiteSuperior);

            //si ha llegado a la parte de abajo del todo
            if (nuevaPosicion.y == limiteSuperior)
            { //haces zoom out
                ActivarZoomOut();
                //desactivar animacion
                naves.GetComponent<Animator>().enabled = false;
            }
        }
        // Establece la nueva posición de la cámara
        transform.position = nuevaPosicion;

        #endregion

        #region ZoomOut
            if(zoomOut)
            {
                // Aumenta el tamaño gradualmente mientras no haya alcanzado el límite
                if (camara.orthographicSize < aumentoMaximo)
                {
                    float nuevoTamaño = Mathf.Lerp(camara.orthographicSize, aumentoMaximo, velocidadAumento * Time.deltaTime);
                    camara.orthographicSize = nuevoTamaño;
                }
                //si ha alcanzado el limite
                if (camara.orthographicSize > 7.5)
                {
                    zoomOut = false;
                    //pasar siguiente escena
                    //activar boton nueva tierra interactuable
                    if(SceneManager.GetActiveScene().name == "LlegadaPlaneta")
                    {
                        ActivarNuevaTierraInteractuable();
                    }
                }
            }
        #endregion
    }


    public void ActivarZoomOut()
    {
        zoomOut = true;
    }

    public void ActivarNuevaTierraInteractuable()
    {

        //activamos animator
        spritePlaneta.SetActive(true);
        spritePlaneta.GetComponent<Animator>().enabled = true;
        spritePlaneta.GetComponent<PlanetaHabitableComportamiento>().enabled = true;
    }


}
