using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerTareaBengalas : MonoBehaviour
{
    //singleton
    static private UIManagerTareaBengalas _instanceUITareaBengalas;

    GameManagerTareaBengalas GMTareaBengalas;

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

    
}
