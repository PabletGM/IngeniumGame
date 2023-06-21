using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField]
    private ParticleSystem vfxExcavacion;

    [SerializeField]
    private TerminarAnterior term;

    //a la que cerramos la mina accede a picarAnimacion para cambiar num clicksInstantaneos
    [SerializeField]
    private PicarAnimacion clicksInstantaneos;

    //para ver en que toque de cada hoyo hay agua, si es 0 es que no hay en ese hoyo
    [SerializeField]
    private int numeroToquesAgua;

    private bool picarMas = true;

   
   
  

    //metodo que quita profundidad cada vez que se hace click al boton click
    public void QuitarProfundidad()
    {
        //para ver si se puede picarMas
        if(picarMas)
        {

            //aumenta numero de picadas antes de comprobar
            numeroPicadasHoyo++;
            //cada vez que aumenta numero de picadas modificamos el numero total de picadas de este hoyo en concreto
            //en el array
            _myGameManager.ModificarArrayPicadasTotalesCadaHoyo(numeroPicadasHoyo);
            //avisa al GameManager que se ha picado 1 vez más
            _myGameManager.ExcavacionExtra();
            //picar efecto
            transform.position = transform.position + new Vector3(0, cantidadDesplazable, 0);


            //Desplazamos mientras que el numero de picadas sea menor que maximas
            if (numeroPicadasHoyo < numeroPicadasMaximasPorHoyo)
            {

                //quedan picadas por hacer y avisamos
                _myGameManager.QuedanPicadasHoyo(true);
                //hacemos vfx
                vfxExcavacion.Play();
                //calculamos y ponemos por consola excavaciones extra
                int num = _myGameManager.NumExcavacionesTotales();
                Debug.Log(num);
            }
            //cambio el estado a los 2 ticks del hoyo actual ya que ya has acabado y quito letras
            else
            {
                
                picarMas = false;
                //ya no quedan picadas por hacer y avisamos para que no se pongan letras excavar y se cambia el boton
                _myGameManager.QuedanPicadasHoyo(false);
                //pasamos al siguiente boton y lo hacemos selected inmediatamente
                _myGameManager.PasarAlSiguienteHoyo();
                //vemos que numero de boton es el actual
                int i = _myGameManager.AveriguarHoyoDevolverNumero();
                //así podemos saber el boton anterior al actual, que es el que queremos cambiar
                SelectedButton buttonAnterior = _myGameManager.botonDevueltoIndice(i - 1);
                GameObject go = buttonAnterior.gameObject;
                go.GetComponentInChildren<TextMeshProUGUI>().text = "";
                //accedemos a su metodo
                term.CerrarExcavacionManual(buttonAnterior.gameObject);
               
                //volvemos a reiniciar la variable privada de clicks Instantaneos a 0 de el script PicarAnimacion
                clicksInstantaneos.ReiniciarClicksInstantaneos();
            }

            

            //vemos si ha encontrado agua para sonido, si está entre 0 y 10 y es igual a numeroPicadas
            if (numeroToquesAgua > 0 && numeroToquesAgua <= 10 && numeroToquesAgua == numeroPicadasHoyo)
            {
                //no se puede picar más
                picarMas = false;
                AudioManager.Instance.PlaySFX("Agua");
            }
        }
        
        
    }
    private void Start()
    {
        _myGameManager = GameManager.GetInstance();
    }
}
