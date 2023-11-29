using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    public Camera camara; // Referencia a la cámara que deseas modificar
    private float aumentoMaximo = 12.0f; // Tamaño máximo al que quieres aumentar la cámara
    private float velocidadAumento = 0.5f; // Velocidad a la que aumenta el tamaño de la cámara


    

    private bool able = true;

    [SerializeField]
    private GameObject explosion;


    private void Update()
    {
        if(able)
        {
            // Aumenta el tamaño gradualmente mientras no haya alcanzado el límite
            if (camara.orthographicSize < aumentoMaximo)
            {
                float nuevoTamaño = Mathf.Lerp(camara.orthographicSize, aumentoMaximo, velocidadAumento * Time.deltaTime);
                camara.orthographicSize = nuevoTamaño;
            }
            //si ha alcanzado el limite
            if (camara.orthographicSize > 8)
            {
                able = false;
                //se activa boton
                //llamamos a play para iniciar animacion
                explosion.GetComponent<Explosion>().Play();
            }
        }
       
    }

}
