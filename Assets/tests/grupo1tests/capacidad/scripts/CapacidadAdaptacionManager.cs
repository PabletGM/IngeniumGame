using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CapacidadAdaptacionManager : MonoBehaviour
{
    #region panelesJerarquia
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

    #region resultadoPrueba
    private string resultadoPruebaCapacidad1 = "";
    #endregion


    private void Start()
    {
        
    }

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

    }

    //metodo que elige la opcion B de la prueba1 de capacidad
    public void ChooseOptionBCapacidad1()
    {
        resultadoPruebaCapacidad1 = "b";
        opcionElegidaResult1.text = resultadoPruebaCapacidad1;
    }

    //metodo que elige la opcion C de la prueba1 de capacidad
    public void ChooseOptionCCapacidad1()
    {
        resultadoPruebaCapacidad1 = "c";
        opcionElegidaResult1.text = resultadoPruebaCapacidad1;
    }

    //para pasar a siguiente test
    public void ContinueNextQuestionCapacidad()
    {
        //pasariamos el resultadoPruebaCapacidad1 como string y argumento al metodo que lo subiese a la base de datos

        //pasa al siguiente test
    }
}
