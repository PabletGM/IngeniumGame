using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicarAnimacion : MonoBehaviour
{
    //pondrá el booleano picar a true para que se haga animacion 1 vez de picar y luego no
    //queremos que las 2 primeras picadas sean instantaneos sin esperas y después espera de 1 segundo

    public Animator animatorPicar;

    GameManager _myGameManager;

    //contador para ver si se han hecho 2 veces el click instantaneo
    private int clickInst = 0;

    private int maxclicksInstantaneos = 2;

    public void Picar()
    {
        
        animatorPicar.SetBool("picar", true);
        //se suma 1 en el click instantaneo
        clickInst++;
        //comprobamos si hemos superado el limite de clicks instantaneos para ladesactivacion temporal del resto de cliks
        if(clickInst > maxclicksInstantaneos)
        {
            //mientras está activa debemos desactivar el isInteractable del boton para que no se pueda dar a picar todo el rato
            _myGameManager.FuncionalidadBotonPicoTemporalPonerQuitar(false);  
        }
        //sonido pala golpe al acabar animacion
        AudioManager.Instance.PlaySFX("Pala");
        //invocamos en 1 segundo y medio que es lo que dura la animacion el nopicar para que pare
        Invoke("NoPicar", 1.5f);

    }


    //cuando pasamos de hoyo volvemos a tener clickInst = 0
    public void ReiniciarClicksInstantaneos()
    {
        clickInst = 0;
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
