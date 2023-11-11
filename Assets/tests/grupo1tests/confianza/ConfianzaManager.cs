using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfianzaManager : MonoBehaviour
{
    static private ConfianzaManager _instanceConfianzaManager;

    #region panelesJerarquiaConfianza1

    [SerializeField]
    private GameObject botonContinue1;

    [SerializeField]
    private TMP_Text opcionElegidaResult1;

    #endregion

    #region argumentos confianza1
        #region resultadoPruebaConfianza1
        private string resultadoPruebaConfianza1 = "";
            #pragma warning disable CS0414
            private int resultadoNumPruebaConfianza1 = 0;
            #pragma warning restore CS0414
        #endregion

        private string itemNameConf_1 = "CONF_1";
        
        //cantidad de softskills que mide y nombres
        private string[] softSkillConf_1;

        //tipo de test que es, si imagenes, preguntas escritas, etc
        private int type = 1;

        //cantidad de puntuaciones de todas las softskills que recoge
        private float[] puntuacionConf_1;
    //numero de segundos por partida
    private int numSecsPartidaConf1 = 0;


    #endregion


    #region panelesJerarquiaConfianza2

    [SerializeField]
    private GameObject botonContinue2;

    [SerializeField]
    private TMP_Text opcionElegidaResult2;

    #endregion

    #region argumentos confianza2

        #region resultadoPruebaConfianza2
    private string resultadoPruebaConfianza2 = "";
        #pragma warning disable CS0414
        private int resultadoNumPruebaConfianza2 = 0;
#pragma warning restore CS0414
    #endregion

        private string itemNameConf_2 = "CONF_2";

        //cantidad de softskills que mide y nombres
        private string[] softSkillConf_2;

        //tipo de test que es, si imagenes, preguntas escritas, etc
        private int type2 = 1;

        //cantidad de puntuaciones de todas las softskills que recoge
        private float[] puntuacionConf_2;
    //numero de segundos por partida
    private int numSecsPartidaConf2 = 0;
    #endregion

    [SerializeField]
    private GameObject timer;



    static public ConfianzaManager GetInstanceConfianzaManager()
    {
        return _instanceConfianzaManager;
    }

    private void Awake()
    {
    
        //si la instancia no existe se hace este script la instancia
        if (_instanceConfianzaManager == null)
        {
            _instanceConfianzaManager = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }


    #region TestEntero
    [SerializeField]
    private GameObject test1Confianza;

    [SerializeField]
    private GameObject test2Confianza;
    #endregion


    #region methods Confianza1

    //metodo que elige la opcion A de la prueba1 de capacidad
    public void ChooseOptionAConfianza1()
    {
        resultadoPruebaConfianza1 = "a";
        opcionElegidaResult1.text = resultadoPruebaConfianza1;

        //ponemos valor numerico segun el resultado
        resultadoNumPruebaConfianza1 = 1;

    }

    //metodo que elige la opcion B de la prueba1 de capacidad
    public void ChooseOptionBConfianza1()
    {
        resultadoPruebaConfianza1 = "b";
        opcionElegidaResult1.text = resultadoPruebaConfianza1;

        //ponemos valor numerico segun el resultado
        resultadoNumPruebaConfianza1 = 2;
    }

    //metodo que elige la opcion C de la prueba1 de capacidad
    public void ChooseOptionCConfianza1()
    {
        resultadoPruebaConfianza1 = "c";
        opcionElegidaResult1.text = resultadoPruebaConfianza1;

        //ponemos valor numerico segun el resultado
        resultadoNumPruebaConfianza1 = 3;
    }

    //para pasar a siguiente test
    public void ContinueNextQuestionConfianza()
    {
        //pasariamos el resultadoPruebaCapacidad1 como string y argumento al metodo que lo subiese a la base de datos

        //pasa al siguiente test
        test1Confianza.SetActive(false);
        test2Confianza.SetActive(true);
        //informamos al timer que ahora es test2Capacidad
        timer.GetComponent<TimeController>().CambiarDeTest1ConfAConf2("test2");
        //reiniciamos contador
        timer.GetComponent<TimeController>().ReiniciarContador();
    }

    //metodo que guardará el numero de segundos totales de partida que lleva
    public void NumSecsPartidaConfianza1(int secsPartida)
    {
        numSecsPartidaConf1 = secsPartida;
    }

    public int TiempoPartidaConfianza1()
    {
        return numSecsPartidaConf1;
    }

    #endregion

    #region ArgumentosConfianza1

    public string itemNameCONF_1()
    {
        return itemNameConf_1;
    }

    public string[] softskillCONF_1()
    {
        softSkillConf_1 = new string[1];
        softSkillConf_1[0] = "confianza";
        return softSkillConf_1;
    }

    public int typeCONF_1()
    {

        return type;
    }

    public float[] puntuacionCONF_1()
    {
        puntuacionConf_1 = new float[1];
        puntuacionConf_1[0] = resultadoNumPruebaConfianza1;
        return puntuacionConf_1;
    }


    #endregion

    #region ArgumentosConfianza2

    public string itemNameCONF_2()
    {
        return itemNameConf_2;
    }

    public string[] softskillCONF_2()
    {
        softSkillConf_2 = new string[1];
        softSkillConf_2[0] = "confianza";
        return softSkillConf_2;
    }

    public int typeCONF_2()
    {

        return type2;
    }

    public float[] puntuacionCONF_2()
    {
        puntuacionConf_2 = new float[1];
        puntuacionConf_2[0] = resultadoNumPruebaConfianza2;
        return puntuacionConf_2;
    }


    #endregion

    #region methods Confianza2
    //metodo que elige la opcion A de la prueba1 de capacidad
    public void ChooseOptionAConfianza2()
    {
        resultadoPruebaConfianza2 = "a";
        opcionElegidaResult2.text = resultadoPruebaConfianza2;

        //ponemos valor numerico segun el resultado
        resultadoNumPruebaConfianza2 = 1;

    }

    //metodo que elige la opcion B de la prueba1 de capacidad
    public void ChooseOptionBConfianza2()
    {
        resultadoPruebaConfianza2 = "b";
        opcionElegidaResult2.text = resultadoPruebaConfianza2;

        //ponemos valor numerico segun el resultado
        resultadoNumPruebaConfianza2 = 2;
    }

    //metodo que elige la opcion C de la prueba1 de capacidad
    public void ChooseOptionCConfianza2()
    {
        resultadoPruebaConfianza2 = "c";
        opcionElegidaResult2.text = resultadoPruebaConfianza2;

        //ponemos valor numerico segun el resultado
        resultadoNumPruebaConfianza2 = 3;
    }

    //para pasar a siguiente test
    public void ContinueNextQuestionConfianza2()
    {
        //pasariamos el resultadoPruebaCapacidad1 como string y argumento al metodo que lo subiese a la base de datos

        //pasa al siguiente test o escena capacidad
        SceneManager.LoadScene("capacidadDeAdaptacion");
    }

    //metodo que guardará el numero de segundos totales de partida que lleva
    public void NumSecsPartidaConfianza2(int secsPartida)
    {
        numSecsPartidaConf2 = secsPartida;
    }

    public int TiempoPartidaConfianza2()
    {
        return numSecsPartidaConf2;
    }
    #endregion
}
