using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    public Camera camara; // Referencia a la cámara que deseas modificar
    private float aumentoMaximo = 12.0f; // Tamaño máximo al que quieres aumentar la cámara
    private float velocidadAumento = 0.5f; // Velocidad a la que aumenta el tamaño de la cámara

    [SerializeField] private GameObject button;

    

    private bool able = true;


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
                //se activa boton
                button.SetActive(true);
                able = false;
            }
        }
       
    }

    public void QuitarButton()
    {
        button.SetActive(false);
    }
}
