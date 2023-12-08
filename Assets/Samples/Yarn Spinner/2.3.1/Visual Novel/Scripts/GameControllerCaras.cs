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

    private bool stopTanda1 = false;

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
        if(SceneManager.GetActiveScene().name == "tareaCaras2" && (Mathf.Abs(mainCamera.orthographicSize - targetZoom) <= 0.3f)&& !stopTanda1)
        {
            //ponemos texto de pregunta
            SetTamañoPanel(dialoguePanel1, true);
        }
        //rango especifico de fotos
        //comprobamos que es la escena tareaCaras2
        if (SceneManager.GetActiveScene().name == "tareaCaras2" && (Mathf.Abs(mainCamera.orthographicSize - targetZoom) <= 1f) &&!stopTanda1)
        {
            SetIniciarTanda1(true);
            stopTanda1 = true;
        }
    }

    #region Tanda1
    public void SetIniciarTanda1(bool set)
    {
        //activamos tanda1
        SetTandaChosen(true, false, false);
        //quitamos dialoguePanel1
        SetTamañoPanel(dialoguePanel1, true);
        //ponemos imagen  ajustamos tamaño 
        SetImagenBigSize(foto1);
        //y botones de respuesta
        SetActiveBotonesOpciones(botonRespuesta1, botonRespuesta2, botonRespuesta3, botonRespuesta4, true);
    }

    #endregion

    #region Tanda2
    public void SetIniciarTanda2(bool set)
    {
        Debug.Log("IniciarTanda2");

        //activamos tanda 2
        SetTandaChosen(false,true, false);

        //quitamos dialoguePanel1
        SetTamañoPanel(dialoguePanel1, false);
        //ponemos dialoguePanel2  a grande
        SetTamañoPanel(dialoguePanel2, true);
        //activamos dialogo 2
        SetDialoguePanel(false, true, false);

        //poner imagen 2 a grande
        SetImagenBigSize(foto2);

        //y botones de respuesta
        SetActiveBotonesOpciones(botonRespuesta21, botonRespuesta22, botonRespuesta23, botonRespuesta24, true);
    }

    #endregion

    #region Tanda3
    public void SetIniciarTanda3(bool set)
    {
        Debug.Log("IniciarTanda3");

        //activamos tanda3
        SetTandaChosen(false, false, true);

        //quitamos dialoguePanel2
        SetTamañoPanel(dialoguePanel2, false);
        //ponemos dialoguePanel3
        SetTamañoPanel(dialoguePanel3, true);
        //activamos dialogo3
        SetDialoguePanel(false, false, true);

        //ponemos imagen  ajustamos tamaño 
        SetImagenBigSize(foto3);
        //y botones de respuesta
        SetActiveBotonesOpciones(botonRespuesta31, botonRespuesta32, botonRespuesta33, botonRespuesta34, true);
    }

    #endregion

    private void SetImagenBigSize(GameObject foto)
    {
        foto.SetActive(true);
        foto.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);
    }

    private void SetTandaChosen(bool setTanda1, bool setTanda2, bool setTanda3)
    {
        //comprobacion extra
        tanda1.SetActive(setTanda1);
        tanda2.SetActive(setTanda2);
        tanda3.SetActive(setTanda3);
    }


    private void SetTamañoPanel(GameObject dialoguePanel, bool set)
    {
        dialoguePanel.SetActive(set);
        //si lo quieres hacer grande
        if (set)
        {
            
            dialoguePanel.transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 1f);
        }
        else
        {
            dialoguePanel.transform.DOScale(new Vector3(0.001f, 0.001f, 0.001f), 1f);
        }

    }

    private void SetActiveBotonesOpciones(GameObject boton1, GameObject boton2, GameObject boton3, GameObject boton4, bool set)
    {
        boton1.SetActive(set);
        boton2.SetActive(set);
        boton3.SetActive(set);
        boton4.SetActive(set);
    }

    private void SetDialoguePanel(bool setTanda1, bool setTanda2, bool setTanda3)
    {
        dialoguePanel1.SetActive(setTanda1);
        dialoguePanel2.SetActive(setTanda2);
        dialoguePanel3.SetActive(setTanda3);
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