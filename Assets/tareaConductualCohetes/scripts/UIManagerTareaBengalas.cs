using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerTareaBengalas : MonoBehaviour
{
    //singleton
    static private UIManagerTareaBengalas _instanceUITareaBengalas;

    GameManagerTareaBengalas GMTareaBengalas;


    [SerializeField]
    private Button botonStart;

    [SerializeField]
    private Image imagenBoton;

    private void Awake()
    {
       
        //si la instancia no existe se hace este script la instancia
        if (_instanceUITareaBengalas == null)
        {
            _instanceUITareaBengalas = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }

    static public UIManagerTareaBengalas GetInstanceUI()
    {
        return _instanceUITareaBengalas;
    }

    public void SetBoton(bool set)
    {
        //si set = true
        if(set)
        {
            //activamos boton
            botonStart.gameObject.SetActive(true);
            //desactivamos imagen del boton
            imagenBoton.gameObject.SetActive(false);
        }
        else
        {
            //desactivamos boton
            botonStart.gameObject.SetActive(false);
            //activamos imagen del boton
            imagenBoton.gameObject.SetActive(true);
        }
    }

    
}
