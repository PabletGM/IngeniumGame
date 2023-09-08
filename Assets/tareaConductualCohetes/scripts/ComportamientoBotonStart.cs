using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoBotonStart : MonoBehaviour
{
    GameManagerTareaBengalas _myGameManagerBengalas;

    private float restante;
    private bool enMarcha = false;
    private int tiempoMaximo;

    // Start is called before the first frame update
    void Start()
    {
        _myGameManagerBengalas = GameManagerTareaBengalas.GetInstanceGM();
    }

    #region Pulsar boton
    private void OnMouseDown()
    {
        BotonPulsado();
    }

    //si se pulsa boton se inicia timer
    public void BotonPulsado()
    {
        enMarcha = true;
    }

    #endregion


    #region Soltar boton

    private void OnMouseUp()
    {
        BotonSoltado();
    }

    //si se suelta el boton se para el timer
    public void BotonSoltado()
    {
        enMarcha = false;
    }

    #endregion

    //mira todo el rato si el boton se ha pulsado o soltado
    void Update()
    {
        //cuando se pulse boton se inicia timer
        if (enMarcha)
        {
            //va sumando segundos
            restante += Time.deltaTime;

            //en caso de superar el tiempo maximo (uno que se establezca)
            if (restante > tiempoMaximo)
            {
                enMarcha = false;
            }
            //cambiamos texto
            string timePressed = restante.ToString();
            Debug.Log("Button pressed: " + timePressed);
        }
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
