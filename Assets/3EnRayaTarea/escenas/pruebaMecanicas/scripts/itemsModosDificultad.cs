using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemsModosDificultad : MonoBehaviour
{

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
}
