using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Excavando : MonoBehaviour
{
    GameManager _myGameManager;
    //segun el hoyo ser� diferente
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

   
   
  

    //metodo que quita profundidad cada vez que se hace click al boton click
    public void QuitarProfundidad()
    {
        //aumenta numero de picadas nntes de comprobar
        numeroPicadasHoyo++;
        //avisa al GameManager que se ha picado 1 vez m�s
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
            //ya no quedan picadas por hacer y avisamos para que no se pongan letras excavar
            _myGameManager.QuedanPicadasHoyo(false);
            //encontramos boton actual
            SelectedButton button = _myGameManager.buttonPressed();
            GameObject go = button.gameObject;
            go.GetComponentInChildren<TextMeshProUGUI>().text = "";
            //accedemos a su metodo
            term.CerrarExcavacionManual(button.gameObject);
        }
        
    }
    private void Start()
    {
        _myGameManager = GameManager.GetInstance();
    }
}
