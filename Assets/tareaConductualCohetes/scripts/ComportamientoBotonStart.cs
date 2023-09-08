using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoBotonStart : MonoBehaviour
{
    GameManagerTareaBengalas _myGameManagerBengalas;

    private float restante;
    private bool enMarcha = false;
    //5 segundos es lo que tarda el cohete en llegar a las nubes
    private int tiempoMaximo= 5;

    // Start is called before the first frame update
    void Start()
    {
        _myGameManagerBengalas = GameManagerTareaBengalas.GetInstanceGM();
    }

    #region Pulsar boton
    //si se pulsa boton se inicia timer
    public void BotonPulsado()
    {
        //iniciamos timer
        enMarcha = true;
        //lanzamos cohete
        LanzamientoCohete();
    }

    #endregion


    #region Soltar boton
    //si se suelta el boton se para el timer
    public void BotonSoltado()
    {
        //timer para
        enMarcha = false;
        //explotamos cohete
        ExplosionCohete();
    }

    #endregion

    //mira todo el rato si el boton se ha pulsado o soltado
    void Update()
    {
        //si se pulsa boton click izq raton
        if (Input.GetMouseButtonDown(0))
        {
            BotonPulsado();
        }

        



        //cuando se pulse boton se inicia timer
        if (enMarcha)
        {
            //va sumando segundos
            restante += Time.deltaTime;

            //si ha superado el tiempo minimo de 1 segundo y suelta boton
            if(restante >= 1 && Input.GetMouseButtonUp(0))
            {
                //ya puede explotar el cohete
                BotonSoltado();
            }
            
            //en caso de superar el tiempo maximo (uno que se establezca) o
            //sino ha superado el tiempo minimo y suelta el boton
            if (restante > tiempoMaximo || restante < 1 && Input.GetMouseButtonUp(0))
            {
               
                //reiniciamos timer
                restante = 0;
                //ya puede explotar el cohete
                BotonSoltado();

            }
            Debug.Log("Button pressed: " + restante);
        }
    }


    public void LanzamientoCohete()
    {
        //inicialmente queremos que el cohete suba al pulsar el boton
        _myGameManagerBengalas.LanzamientoCohete();

        //vemos cuanto tiempo se ha pulsado el botón

        //aplicamos esa fuerza al cohete y a la explosion de este
    }



    public void ExplosionCohete()
    {
        //inicialmente queremos que el cohete suba al pulsar el boton
        _myGameManagerBengalas.ExplosionCohete();

        //vemos cuanto tiempo se ha pulsado el botón

        //aplicamos esa fuerza al cohete y a la explosion de este
    }
}
