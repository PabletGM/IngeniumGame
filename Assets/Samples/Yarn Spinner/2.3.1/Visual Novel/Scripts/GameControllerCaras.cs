using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerCaras : MonoBehaviour
{
    public float smoothSpeed; // Velocidad de suavizado

    public float targetX ; // Coordenada X del punto de enfoque
    public float targetY; // Coordenada Y del punto de enfoque
    public float targetZoom; // Tamaño de zoom deseado

    private Camera mainCamera;

    [SerializeField]
    private GameObject canvasPlayer;

    [SerializeField]
    private GameObject foto1;

    [SerializeField]
    private GameObject botonRespuesta1;
    [SerializeField]
    private GameObject botonRespuesta2;
    [SerializeField]
    private GameObject botonRespuesta3;
    [SerializeField]
    private GameObject botonRespuesta4;

    [SerializeField]
    private GameObject botonContinue;

    

    void Start()
    {
        mainCamera = Camera.main; // Obtén la cámara principal
    }

    void Update()
    {
        // Calcula la posición objetivo para el zoom
        Vector3 targetPos = new Vector3(targetX, targetY, mainCamera.transform.position.z);

        // Usa Lerp para suavizar el movimiento de la cámara hacia la posición objetivo
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetZoom, smoothSpeed * Time.deltaTime);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPos, smoothSpeed * Time.deltaTime);

        //rango especifico de texto
        //comprobamos que es la escena tareaCaras2
        if(SceneManager.GetActiveScene().name == "tareaCaras2" && (Mathf.Abs(mainCamera.orthographicSize - targetZoom) <= 0.3f))
        {
            //ponemos texto de pregunta
            canvasPlayer.SetActive(true);
            canvasPlayer.transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 1f);
        }
        //rango especifico de fotos
        //comprobamos que es la escena tareaCaras2
        if (SceneManager.GetActiveScene().name == "tareaCaras2" && (Mathf.Abs(mainCamera.orthographicSize - targetZoom) <= 1f))
        {
            //ponemos imagen  ajustamos tamaño 
            foto1.SetActive(true);
            foto1.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);
            //y botones de respuesta
            botonRespuesta1.SetActive(true);
            //botonRespuesta1.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);

            botonRespuesta2.SetActive(true);
            //botonRespuesta2.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);

            botonRespuesta3.SetActive(true);
            //botonRespuesta3.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);

            botonRespuesta4.SetActive(true);
            //botonRespuesta4.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);
        }
    }

    public void BotonContinue()
    {
        botonContinue.SetActive(true);
    }
}