using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoomPuerta3EnRaya : MonoBehaviour
{
    public bool ZoomActive;
    public Camera cam;
    public float speed;
    public float zoomDuration = 1.0f; // Duración total del efecto de zoom
    public Transform zoomTarget; // La posición hacia la que se realizará el zoom
    private Vector3 originalPosition;
    private Vector3 zoomPosition;
    private float zoomOrthographicSize = 8.7f;
    private float originalOrthographicSize =15.98f;
    private float zoomStartTime;

    [SerializeField]
    private GameObject boton;

    private void Start()
    {
        cam = Camera.main;
        originalPosition = cam.transform.position;
        zoomPosition = zoomTarget.position;
        
    }

    public void ZoomIn()
    {
        ZoomActive = true;
        zoomStartTime = Time.time;

        
    }

    public void ZoomOut()
    {
        ZoomActive = false;
        zoomStartTime = Time.time;
    }

    private void LateUpdate()
    {
        if (ZoomActive)
        {
            float elapsedTime = Time.time - zoomStartTime;
            float t = Mathf.Clamp01(elapsedTime / zoomDuration);

            // Calcula la posición y el tamaño ortográfico en función del tiempo
            Vector3 lerpedPosition = Vector3.Lerp(originalPosition, zoomPosition, t);
            float lerpedOrthographicSize = Mathf.Lerp(originalOrthographicSize, zoomOrthographicSize, t);

            // Aplica la posición y el tamaño ortográfico a la cámara
            cam.transform.position = new Vector3(lerpedPosition.x, lerpedPosition.y, cam.transform.position.z);
            cam.orthographicSize = lerpedOrthographicSize;
            Invoke("ActivarBoton", 2f);
        }
    }

    public void ActivarBoton()
    {
        //boton.SetActive(true);
    }

    public void PasarEscenaJuego()
    {

        //SceneManager.LoadScene("circulosNave");
    }
}

