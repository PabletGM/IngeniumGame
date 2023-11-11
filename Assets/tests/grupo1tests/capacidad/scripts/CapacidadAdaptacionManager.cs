using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CapacidadAdaptacionManager : MonoBehaviour
{
    static private CapacidadAdaptacionManager _instanceCapacidad;

    #region panelesJerarquiaCapacidad1
    [SerializeField]
    private GameObject menuPrincipal;

    [SerializeField]
    private GameObject panelesABC;

    [SerializeField]
    private GameObject PanelA;

    [SerializeField]
    private GameObject PanelB;

    [SerializeField]
    private GameObject PanelC;

    [SerializeField]
    private GameObject botonContinue;

    [SerializeField]
    private TMP_Text opcionElegidaResult1;

    #endregion

    #region argumentos Capacidad 1
        #region resultadoPruebaCapacidad1
        private string resultadoPruebaCapacidad1 = "";
            #pragma warning disable CS0414
            private int resultadoNumPruebaCapacidad1 = 0;
#pragma warning restore CS0414
    #endregion

        private string itemNameCapac_1 = "CAP_1";

        //cantidad de softskills que mide y nombres
        private string[] softSkillCapac_1;

        //tipo de test que es, si imagenes, preguntas escritas, etc
        private int typeCapac1 = 1;

        //cantidad de puntuaciones de todas las softskills que recoge
        private float[] puntuacionCapac_1;

    //numero de segundos por partida
    private int numSecsPartidaCap1;
    #endregion

    #region panelesJerarquiaCapacidad2
    [SerializeField]
    private GameObject menuPrincipal2;

    [SerializeField]
    private GameObject panelesABC2;

    [SerializeField]
    private GameObject PanelA2;

    [SerializeField]
    private GameObject PanelB2;

    [SerializeField]
    private GameObject PanelC2;

    [SerializeField]
    private GameObject botonContinue2;

    [SerializeField]
    private TMP_Text opcionElegidaResult2;

    #endregion

    #region argumentos Capacidad 2
        #region resultadoPruebaCapacidad2
        private string resultadoPruebaCapacidad2 = "";
            #pragma warning disable CS0414
            private int resultadoNumPruebaCapacidad2 = 0;
            #pragma warning restore CS0414
        #endregion
        

        private string itemNameCapac_2 = "CAP_2";

        //cantidad de softskills que mide y nombres
        private string[] softSkillCapac_2;

        //tipo de test que es, si imagenes, preguntas escritas, etc
        private int typeCapac2 = 1;

        //cantidad de puntuaciones de todas las softskills que recoge
        private float[] puntuacionCapac_2;
    //numero de segundos por partida
    private int numSecsPartidaCap2;
    #endregion

    [SerializeField]
    private GameObject test1Capacidad;

    [SerializeField]
    private GameObject test2Capacidad;

    [SerializeField]
    private GameObject timer;

    static public CapacidadAdaptacionManager GetInstanceCapacidadAdaptacionManager()
    {
        return _instanceCapacidad;
    }

    private void Awake()
    {

        //si la instancia no existe se hace este script la instancia
        if (_instanceCapacidad == null)
        {
            _instanceCapacidad = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }

    #region methods Capacidad1

    //metodo opcion A que abre panel opcion A de la pregunta 1
    public void OpenOptionA1()
    {
        //quitamos botonContinue
        botonContinue.SetActive(false);
        //cerramos menuPrincipal
        menuPrincipal.SetActive(false);
        //abrimos panelesABC y opcion PanelA
        panelesABC.SetActive(true);
        PanelA.SetActive(true);
    }

    //metodo opcion B que abre panel opcion B de la pregunta 1
    public void OpenOptionB1()
    {
        //quitamos botonContinue
        botonContinue.SetActive(false);
        //cerramos menuPrincipal
        menuPrincipal.SetActive(false);
        //abrimos panelesABC y opcion PanelA
        panelesABC.SetActive(true);
        PanelB.SetActive(true);
    }

    //metodo opcion B que abre panel opcion B de la pregunta 1
    public void OpenOptionC1()
    {
        //quitamos botonContinue
        botonContinue.SetActive(false);
        //cerramos menuPrincipal
        menuPrincipal.SetActive(false);
        //abrimos panelesABC y opcion PanelA
        panelesABC.SetActive(true);
        PanelC.SetActive(true);
    }

    //metodo que cierra las opciones y permite volver a menuPrincipal
    public void CerrarOpciones1()
    {
        //ponemos botonContinue
        botonContinue.SetActive(true);
        //abrimos menuPrincipal
        menuPrincipal.SetActive(true);
        //cerramos panelesABC, panelA, panelB y panelC
        panelesABC.SetActive(false);
        PanelA.SetActive(false);
        PanelB.SetActive(false);
        PanelC.SetActive(false);
    }

    //metodo que elige la opcion A de la prueba1 de capacidad
    public void ChooseOptionACapacidad1()
    {
        resultadoPruebaCapacidad1 = "a";
        opcionElegidaResult1.text = resultadoPruebaCapacidad1;

        //ponemos valor numerico segun el resultado
        resultadoNumPruebaCapacidad1 = 1;

    }

    //metodo que elige la opcion B de la prueba1 de capacidad
    public void ChooseOptionBCapacidad1()
    {
        resultadoPruebaCapacidad1 = "b";
        opcionElegidaResult1.text = resultadoPruebaCapacidad1;

        //ponemos valor numerico segun el resultado
        resultadoNumPruebaCapacidad1 = 2;
    }

    //metodo que elige la opcion C de la prueba1 de capacidad
    public void ChooseOptionCCapacidad1()
    {
        resultadoPruebaCapacidad1 = "c";
        opcionElegidaResult1.text = resultadoPruebaCapacidad1;

        //ponemos valor numerico segun el resultado
        resultadoNumPruebaCapacidad1 = 3;
    }

    //para pasar a siguiente test
    public void ContinueNextQuestionCapacidad()
    {
        //pasariamos el resultadoPruebaCapacidad1 como string y argumento al metodo que lo subiese a la base de datos

        //pasa al siguiente test
        test1Capacidad.SetActive(false);
        test2Capacidad.SetActive(true);
        //informamos al timer que ahora es test2Capacidad
        timer.GetComponent<TimeController>().CambiarDeTest1CapACap2("test2");
        //reiniciamos contador
        timer.GetComponent<TimeController>().ReiniciarContador();
    }

    //metodo que guardará el numero de segundos totales de partida que lleva
    public void NumSecsPartidaCapacidad1(int secsPartida)
    {
        numSecsPartidaCap1 = secsPartida;
    }

    public int TiempoPartidaCapacidad1()
    {
        return numSecsPartidaCap1;
    }

    #endregion

    #region methods Capacidad2

    //metodo opcion A que abre panel opcion A de la pregunta 1
    public void OpenOptionA2()
    {
        //quitamos botonContinue
        botonContinue2.SetActive(false);
        //cerramos menuPrincipal
        menuPrincipal2.SetActive(false);
        //abrimos panelesABC y opcion PanelA
        panelesABC2.SetActive(true);
        PanelA2.SetActive(true);
    }

    //metodo opcion B que abre panel opcion B de la pregunta 1
    public void OpenOptionB2()
    {
        //quitamos botonContinue
        botonContinue2.SetActive(false);
        //cerramos menuPrincipal
        menuPrincipal2.SetActive(false);
        //abrimos panelesABC y opcion PanelA
        panelesABC2.SetActive(true);
        PanelB2.SetActive(true);
    }

    //metodo opcion B que abre panel opcion B de la pregunta 1
    public void OpenOptionC2()
    {
        //quitamos botonContinue
        botonContinue2.SetActive(false);
        //cerramos menuPrincipal
        menuPrincipal2.SetActive(false);
        //abrimos panelesABC y opcion PanelA
        panelesABC2.SetActive(true);
        PanelC2.SetActive(true);
    }

    //metodo que cierra las opciones y permite volver a menuPrincipal
    public void CerrarOpciones2()
    {
        //ponemos botonContinue
        botonContinue2.SetActive(true);
        //abrimos menuPrincipal
        menuPrincipal2.SetActive(true);
        //cerramos panelesABC, panelA, panelB y panelC
        panelesABC2.SetActive(false);
        PanelA2.SetActive(false);
        PanelB2.SetActive(false);
        PanelC2.SetActive(false);
    }

    //metodo que elige la opcion A de la prueba1 de capacidad
    public void ChooseOptionACapacidad2()
    {
        resultadoPruebaCapacidad2 = "a";
        opcionElegidaResult2.text = resultadoPruebaCapacidad2;

        //ponemos valor numerico segun el resultado
        resultadoNumPruebaCapacidad2 = 1;

    }

    //metodo que elige la opcion B de la prueba1 de capacidad
    public void ChooseOptionBCapacidad2()
    {
        resultadoPruebaCapacidad2 = "b";
        opcionElegidaResult2.text = resultadoPruebaCapacidad2;

        //ponemos valor numerico segun el resultado
        resultadoNumPruebaCapacidad2 = 2;
    }

    //metodo que elige la opcion C de la prueba1 de capacidad
    public void ChooseOptionCCapacidad2()
    {
        resultadoPruebaCapacidad2 = "c";
        opcionElegidaResult2.text = resultadoPruebaCapacidad2;

        //ponemos valor numerico segun el resultado
        resultadoNumPruebaCapacidad2 = 3;
    }

    //para pasar a siguiente test
    public void ContinueNextQuestionCapacidad2()
    {
        //pasariamos el resultadoPruebaCapacidad1 como string y argumento al metodo que lo subiese a la base de datos

        //pasa al siguiente test
        SceneManager.LoadScene("finalTest1");
    }

    //metodo que guardará el numero de segundos totales de partida que lleva
    public void NumSecsPartidaCapacidad2(int secsPartida)
    {
        numSecsPartidaCap2 = secsPartida;
    }

    public int TiempoPartidaCapacidad2()
    {
        return numSecsPartidaCap2;
    }

        #endregion

        #region ArgumentosCapacidad1

        public string itemNameCAPAC_1()
        {
            return itemNameCapac_1;
        }

        public string[] softskillCAPAC_1()
        {
            softSkillCapac_1 = new string[1];
            softSkillCapac_1[0] = "capacidad de adaptacion";
            return softSkillCapac_1;
        }

        public int typeCapac_1()
        {

            return typeCapac1;
        }

        public float[] puntuacionCAPAC_1()
        {
            puntuacionCapac_1 = new float[1];
            puntuacionCapac_1[0] = resultadoNumPruebaCapacidad1;
            return puntuacionCapac_1;
        }
    #endregion

    #region ArgumentosCapacidad2

        public string itemNameCAPAC_2()
        {
            return itemNameCapac_2;
        }

        public string[] softskillCAPAC_2()
        {
            softSkillCapac_2 = new string[1];
            softSkillCapac_2[0] = "capacidad de adaptacion";
            return softSkillCapac_2;
        }

        public int typeCapac_2()
        {

            return typeCapac2;
        }

        public float[] puntuacionCAPAC_2()
        {
            puntuacionCapac_2 = new float[1];
            puntuacionCapac_2[0] = resultadoNumPruebaCapacidad2;
            return puntuacionCapac_2;
        }
    #endregion

    
}
