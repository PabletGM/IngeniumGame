using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excavando : MonoBehaviour
{
    GameManager _myGameManager;
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
            //avisa al GameManager que se ha picado 1 vez más
            _myGameManager.ExcavacionExtra();
            //calculamos y ponemos por consola excavaciones extra
            int num = _myGameManager.NumExcavacionesTotales();
            Debug.Log(num);
        }
        
    }
    private void Start()
    {
        _myGameManager = GameManager.GetInstance();
    }
}
