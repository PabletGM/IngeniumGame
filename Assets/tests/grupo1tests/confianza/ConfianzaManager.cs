using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfianzaManager : MonoBehaviour
{
    #region panelesJerarquiaConfianza1

    [SerializeField]
    private GameObject botonContinue1;

    [SerializeField]
    private TMP_Text opcionElegidaResult1;

    #endregion

    #region resultadoPruebaConfianza1
    private string resultadoPruebaConfianza1 = "";
    private int resultadoNumPruebaConfianza1 = 0;
    #endregion

    #region panelesJerarquiaConfianza2

    [SerializeField]
    private GameObject botonContinue2;

    [SerializeField]
    private TMP_Text opcionElegidaResult2;

    #endregion

    #region resultadoPruebaConfianza2
    private string resultadoPruebaConfianza2 = "";
    private int resultadoNumPruebaConfianza2 = 0;
    #endregion


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
    #endregion
}
