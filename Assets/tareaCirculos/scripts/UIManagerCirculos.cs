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

    [SerializeField]
    private GameObject panelGame;

    [SerializeField]
    private GameObject panelRonda;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CambiarPuntuacion(string nuevaPuntuacion)
    {
        //Modificar el texto del objeto
        puntuacionNumero.text = nuevaPuntuacion;
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
        //quitas panel game de canvas
        panelGame.SetActive(false);
        //activas panel ronda
        panelRonda.SetActive(true);
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
}
