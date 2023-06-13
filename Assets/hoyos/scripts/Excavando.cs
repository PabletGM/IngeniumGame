using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excavando : MonoBehaviour
{
    //segun el hoyo será diferente
    [SerializeField]
    private int numeroPicadasMaximasPorHoyo;
    //cuenta numero de picadas del hoyo que se han hecho
    private int numeroPicadasHoyo =0;

    //para bajar la cantidad 1 nivel se le suma -0.5 a y
    private float cantidadDesplazable = -0.5f;



   
    //metodo que quita profundidad cada vez que se hace click al boton click
    public void QuitarProfundidad()
    {
        
        //Desplazamos mientras que el numero de picadas sea menor que maximas
        if(numeroPicadasHoyo < numeroPicadasMaximasPorHoyo)
        {
            transform.position = transform.position+ new Vector3(0, cantidadDesplazable, 0);
            //aumenta numero de picadas
            numeroPicadasHoyo++;
        }
        
    }
}
