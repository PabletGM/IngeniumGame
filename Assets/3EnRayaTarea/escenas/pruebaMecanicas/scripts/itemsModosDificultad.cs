using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemsModosDificultad : MonoBehaviour
{



    private float valorTestModoDificil = -1;
    private float valorTestModoFacil = -1;
    private float valorTestModoMedio = -1;

    #region parametros item Modo facil
    [SerializeField]
    private GameObject buttonItemModoFacil;
    [SerializeField]
    private GameObject panelModoFacil;
    [SerializeField]
    private GameObject GameController;
    [SerializeField]
    private GameObject tresEnRaya;

    private bool IniciarJuego = false;

    #endregion

    #region ITEM MODO FACIL

    public void ActivarBotonQuitarItemModoFacil()
    {
        buttonItemModoFacil.SetActive(true);
    }

    public void QuitarItemModoFacil()
    {
        TweenDesaparecerItem();
        
    }

    public void TweenDesaparecerItem()
    {
        //1 segundo de tween 
        panelModoFacil.transform.DOScale(new Vector3(0.01f,0.01f,0.01f), 1f);
        Invoke("DesaparecerItem", 1f);
    }

    public void TweenAparecerItem()
    {
        //1 segundo de tween 
        panelModoFacil.transform.DOScale(new Vector3(1,1,1), 1f);
       
    }

    //DesaparecerItem
    public void DesaparecerItem()
    {
        panelModoFacil.SetActive(false);
        SetFuncionalidadJuegoModo(true);
    }

    public void SetFuncionalidadJuegoModo(bool set)
    {
        //activamos GO GameController
        GameController.SetActive(set);
        GameController.GetComponent<gamecontroller>().enabled = true;
        //activamos dentro del Canvas 3EnRaya
        tresEnRaya.SetActive(set);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //aparece
        TweenAparecerItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Botonn()
    {
        AudioManager3EnRaya.instance.PlaySFXDuracion("IniciarPartida", 1f);
    }

    #region TestModoDificil

    public void OpcionAModoDificil()
    {
        //ponemos puntuacion primer valor a array segun opcion
        valorTestModoDificil = 0f;
        
    }

    public void OpcionBModoDificil()
    {
        //ponemos puntuacion primer valor a array segun opcion
        valorTestModoDificil = 1f;
        
    }

    public void OpcionCModoDificil()
    {
        //ponemos puntuacion primer valor a array segun opcion
        valorTestModoDificil = 2;
        
    }

    //al darle a continue
    public void OpcionElegidaModoDificil()
    {
        
        //pasamos valor
        PuntuacionTest3EnRaya.GetInstanceGM().RellenarPrimerValorArrayPrimerTest(valorTestModoDificil);
    }

    #endregion

    #region TestModoFacil

    public void OpcionAModoFacil()
    {
        //ponemos puntuacion primer valor a array segun opcion
        valorTestModoFacil = 0f;
       
    }

    public void OpcionBModoFacil()
    {
        //ponemos puntuacion primer valor a array segun opcion
        valorTestModoFacil = 1f;
       
    }

    public void OpcionCModoFacil()
    {
        //ponemos puntuacion primer valor a array segun opcion
        valorTestModoFacil = 2;
        
    }

    //al darle a continue
    public void OpcionElegidaModoFacil()
    {
        //pasamos valor
        PuntuacionTest3EnRaya.GetInstanceGM().RellenarSegundorValorArraySegundoTest(valorTestModoFacil);
    }

    #endregion

    #region TestModoMedio

    public void OpcionAModoMedio()
    {
        //ponemos puntuacion primer valor a array segun opcion
        valorTestModoMedio = 0f;
        
    }

    public void OpcionBModoMedio()
    {
        //ponemos puntuacion primer valor a array segun opcion
        valorTestModoMedio = 1f;
        
    }

    public void OpcionCModoMedio()
    {
        //ponemos puntuacion primer valor a array segun opcion
        valorTestModoMedio = 2;
       
    }

    //al darle a continue
    public void OpcionElegidaModoMedio()
    {
        //pasamos valor
        PuntuacionTest3EnRaya.GetInstanceGM().RellenarTercerValorArrayTercerTest(valorTestModoMedio);
    }

    #endregion
}
