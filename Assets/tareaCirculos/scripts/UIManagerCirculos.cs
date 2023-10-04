using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerCirculos : MonoBehaviour
{

    //singleton
    static private UIManagerCirculos _instanceUICirculos;

    GameManagerCirculos GMCirculos;

    public TextMeshProUGUI puntuacionNumero;
    public TextMeshProUGUI puntuacionNumeroPanelRadar;

    [SerializeField]
    private GameObject panelGame;

    [SerializeField]
    private GameObject panelRonda;

    [SerializeField]
    private GameObject objetos;

    [SerializeField]
    private GameObject finalPartida;

    [SerializeField]
    private TextMeshProUGUI cronometroCanvas;

    private void Awake()
    {

        //si la instancia no existe se hace este script la instancia
        if (_instanceUICirculos == null)
        {
            _instanceUICirculos = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }

    static public UIManagerCirculos GetInstanceUI()
    {
        return _instanceUICirculos;
    }
    //se llama cada vez que se recarga escena
    void Start()
    {
        int puntuacionActual = GameManagerCirculos.GetInstanceGM().DevolverPuntuacionActual();
        CambiarPuntuacion(puntuacionActual.ToString());

        //antes de darle a start quita la jugabilidad por estetica
        SetObjetosJugabilidad(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CambiarPuntuacion(string nuevaPuntuacion)
    {
        //Modificar el texto del objeto
        puntuacionNumero.text = nuevaPuntuacion;

        //cambiamos ya que estamos la puntuacion que aparecerá en el panel ronda
        puntuacionNumeroPanelRadar.text = puntuacionNumero.text;
    }



    public void PasarLevel1()
    {
        SceneManager.LoadScene("circulosNave");
    }

    public void PasarLevel2()
    {
        SceneManager.LoadScene("circulosNaveNivel2");
    }

    public void PasarLevel3()
    {
        SceneManager.LoadScene("circulosNaveNivel3");
    }

    //se ha acabado la ronda y activas menu para ver a que nivel pasas
    public void ActivarPanelRonda()
    {
        //sonido panel ronda
        AudioManagerCirculos.instance.PlaySFX("panelRonda");
        //quitas panel game de canvas
        panelGame.SetActive(false);
        //activas panel ronda
        panelRonda.SetActive(true);
        //desactivas objetos para quitar asi la jugabilidad
        objetos.SetActive(false);
    }

    //quitar panel ronda para pasar de nivel
    public void DesactivarPanelRonda()
    {
        //pones panel game de canvas
        panelGame.SetActive(true);
        //quitas panel ronda
        panelRonda.SetActive(false);
    }

    //cuando se pulse al boton iniciar ronda se comienza el juego
    public void IniciarRonda()
    {
        GameManagerCirculos.GetInstanceGM().CronometroRonda();
        //activa game y desactiva panel ronda
        DesactivarPanelRonda();
    }

    public void SetObjetosJugabilidad(bool set)
    {
        //te permite jugabilidad activando o desactivando los objetos
        objetos.SetActive(set);
    }

    public void ActualizarCronometroCanvas(float tiempoRestante)
    {
        cronometroCanvas.text = tiempoRestante.ToString("F2");
    }

    public void LimiteRondas()
    {
        //en caso de que las haya superado debemos:
        //-Quitar jugabilidad, esto es desactivar objetos
        objetos.SetActive(false);
        //dentro del canvas desactivar todo el game, panelRonda y Start
        panelGame.SetActive(false);
        panelRonda.SetActive(false);
        //activar el panel de Canvas que diga finalPartida
        finalPartida.SetActive(true);
    }

    public void PasarEscenaFinalCirculos()
    {
        SceneManager.LoadScene("escenaFinal");
    }
}
