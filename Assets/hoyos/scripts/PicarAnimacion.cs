using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicarAnimacion : MonoBehaviour
{
    //pondrá el booleano picar a true para que se haga animacion 1 vez de picar y luego no

    public Animator animatorPicar;

    GameManager _myGameManager;

    public void Picar()
    {
        animatorPicar.SetBool("picar", true);
        //mientras está activa debemos desactivar el isInteractable del boton para que no se pueda dar a picar todo el rato
        _myGameManager.FuncionalidadBotonPicoTemporalPonerQuitar(false);
        //sonido pala golpe al acabar animacion
        AudioManager.Instance.PlaySFX("Pala");
        //invocamos en 1 segundo y medio que es lo que dura la animacion el nopicar para que pare
        Invoke("NoPicar", 1.5f);
    }
    public void NoPicar()
    {

        animatorPicar.SetBool("picar", false);
        //mientras está desactiva debemos activar el isInteractable del boton para que no se  picar de normal
        _myGameManager.FuncionalidadBotonPicoTemporalPonerQuitar(true);
    }

    private void Start()
    {
        _myGameManager = GameManager.GetInstance();
    }
}
