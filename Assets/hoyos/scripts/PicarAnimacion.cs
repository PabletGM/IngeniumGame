using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicarAnimacion : MonoBehaviour
{
    //pondrá el booleano picar a true para que se haga animacion 1 vez de picar y luego no

    public Animator animatorPicar;

    public void Picar()
    {
        animatorPicar.SetBool("picar", true);
        //invocamos en 1 segundo y medio que es lo que dura la animacion el nopicar para que pare
        Invoke("NoPicar", 1.5f);
    }
    public void NoPicar()
    {
        animatorPicar.SetBool("picar", false);
    }
}
