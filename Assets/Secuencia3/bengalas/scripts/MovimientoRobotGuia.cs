using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoRobotGuia : MonoBehaviour
{
    public Animator animator;
    
    //metodo que permite movimiento robot guia con animacion para que desaparezca
    public void AnimacionRobotGuia()
    {
        animator.SetBool("desaparecer", true);
    }


}
