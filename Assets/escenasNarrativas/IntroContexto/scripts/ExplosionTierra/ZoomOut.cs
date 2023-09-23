using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    public Camera camara; // Referencia a la c�mara que deseas modificar
    private float aumentoMaximo = 12.0f; // Tama�o m�ximo al que quieres aumentar la c�mara
    private float velocidadAumento = 0.5f; // Velocidad a la que aumenta el tama�o de la c�mara

    [SerializeField] private GameObject button;

    

    private bool able = true;


    private void Update()
    {
        if(able)
        {
            // Aumenta el tama�o gradualmente mientras no haya alcanzado el l�mite
            if (camara.orthographicSize < aumentoMaximo)
            {
                float nuevoTama�o = Mathf.Lerp(camara.orthographicSize, aumentoMaximo, velocidadAumento * Time.deltaTime);
                camara.orthographicSize = nuevoTama�o;
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
