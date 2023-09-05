using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoBotonStart : MonoBehaviour
{
    GameManagerTareaBengalas _myGameManagerBengalas;

    // Start is called before the first frame update
    void Start()
    {
        _myGameManagerBengalas = GameManagerTareaBengalas.GetInstanceGM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //metodo que hace el comportamiento del boton start
    public void PressBotonStart()
    {
        //inicialmente queremos que el cohete suba al pulsar el boton
        _myGameManagerBengalas.LanzamientoCohete();

        //vemos cuanto tiempo se ha pulsado el botón

        //aplicamos esa fuerza al cohete y a la explosion de este
    }
}
