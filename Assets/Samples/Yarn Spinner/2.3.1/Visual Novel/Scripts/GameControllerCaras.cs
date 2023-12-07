using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerCaras : MonoBehaviour
{
    #region Zoom
    public float smoothSpeed; // Velocidad de suavizado

    public float targetX ; // Coordenada X del punto de enfoque
    public float targetY; // Coordenada Y del punto de enfoque
    public float targetZoom; // Tamaño de zoom deseado

    #endregion

    private Camera mainCamera;

    #region Tandas
    [Header("Tandas")]
    [SerializeField]
    private GameObject tanda1;
    [SerializeField]
    private GameObject tanda2;
    [SerializeField]
    private GameObject tanda3;
    #endregion


    #region Tanda1
    [Header("Tanda 1 - Foto, respuestas y diálogo")]
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

    //[SerializeField]
    //private GameObject botonContinue;

    [SerializeField]
    private GameObject dialoguePanel1;
    #endregion

    #region Tanda2
    [Header("Tanda 2 - Foto, respuestas y diálogo")]
    [SerializeField]
    private GameObject foto2;

    [SerializeField]
    private GameObject botonRespuesta21;
    [SerializeField]
    private GameObject botonRespuesta22;
    [SerializeField]
    private GameObject botonRespuesta23;
    [SerializeField]
    private GameObject botonRespuesta24;

    //[SerializeField]
    //private GameObject botonContinue2;

    [SerializeField]
    private GameObject dialoguePanel2;
    #endregion

    #region Tanda3
    [Header("Tanda 3 - Foto, respuestas y diálogo")]
    [SerializeField]
    private GameObject foto3;

    [SerializeField]
    private GameObject botonRespuesta31;
    [SerializeField]
    private GameObject botonRespuesta32;
    [SerializeField]
    private GameObject botonRespuesta33;
    [SerializeField]
    private GameObject botonRespuesta34;

    //[SerializeField]
    //private GameObject botonContinue3;

    [SerializeField]
    private GameObject dialoguePanel3;
    #endregion

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
            SetTamañoPanel(dialoguePanel1, true);
        }
        //rango especifico de fotos
        //comprobamos que es la escena tareaCaras2
        if (SceneManager.GetActiveScene().name == "tareaCaras2" && (Mathf.Abs(mainCamera.orthographicSize - targetZoom) <= 1f))
        {
            SetIniciarTanda1(true);
        }
    }

    public void SetIniciarTanda1(bool set)
    {
        //comprobacion extra
        tanda1.SetActive(true);
        tanda2.SetActive(false);
        tanda3.SetActive(false);
        //ponemos imagen  ajustamos tamaño 
        foto1.SetActive(set);
        foto1.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);
        //y botones de respuesta
        botonRespuesta1.SetActive(set);
        //botonRespuesta1.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);

        botonRespuesta2.SetActive(set);
        //botonRespuesta2.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);

        botonRespuesta3.SetActive(set);
        //botonRespuesta3.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);

        botonRespuesta4.SetActive(set);
        //botonRespuesta4.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);

        
    }

    private void SetTamañoPanel(GameObject dialoguePanel, bool set)
    {
        
        //si lo quieres hacer grande
        if(set)
        {
            dialoguePanel.transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 1f);
        }
        else
        {
            dialoguePanel.transform.DOScale(new Vector3(0.001f, 0.001f, 0.001f), 1f);
        }
        
    }

    public void SetIniciarTanda2(bool set)
    {
        Debug.Log("IniciarTanda2");
        //comprobacion extra
        tanda2.SetActive(true);
        tanda1.SetActive(false);
        tanda3.SetActive(false);
        //quitamos dialoguePanel1
        SetTamañoPanel(dialoguePanel1, false);
        //ponemos dialoguePanel2
        SetTamañoPanel(dialoguePanel2, true);
        //quitamos tanda1
        SetIniciarTanda1(false);

        //ponemos imagen  ajustamos tamaño 
        foto2.SetActive(set);
        foto2.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);
        //y botones de respuesta
        botonRespuesta21.SetActive(set);
        //botonRespuesta1.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);

        botonRespuesta22.SetActive(set);
        //botonRespuesta2.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);

        botonRespuesta23.SetActive(set);
        //botonRespuesta3.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);

        botonRespuesta24.SetActive(set);
        //botonRespuesta4.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);
    }

    public void SetIniciarTanda3(bool set)
    {
        Debug.Log("IniciarTanda3");
        //comprobacion extra
        tanda3.SetActive(true);
        tanda2.SetActive(false);
        tanda1.SetActive(false);
        //quitamos dialoguePanel2
        SetTamañoPanel(dialoguePanel2, false);
        //ponemos dialoguePanel3
        SetTamañoPanel(dialoguePanel3, true);
        //quitamos tanda2
        SetIniciarTanda2(false);

        //ponemos imagen  ajustamos tamaño 
        foto3.SetActive(set);
        foto3.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);
        //y botones de respuesta
        botonRespuesta31.SetActive(set);
        //botonRespuesta1.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);

        botonRespuesta32.SetActive(set);
        //botonRespuesta2.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);

        botonRespuesta33.SetActive(set);
        //botonRespuesta3.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);

        botonRespuesta34.SetActive(set);
        //botonRespuesta4.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);
    }

    public void NextScene()
    {
        //SceneManager.LoadScene("dd");
        Debug.Log("Next Scene");
    }

    //public void BotonContinue()
    //{
    //    botonContinue.SetActive(true);
    //}
}