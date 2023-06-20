using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminarAnterior : MonoBehaviour
{
    //en este script habrá un metodo que se llame al pulsar 1 vez en cada boton que hará 2 cosas
    //cambiar el sprite del anterior boton a un doble tick
    //hacer el anterior boton no interactuable una vez se pulsa el nuevo

    [SerializeField]
    private Sprite dobleTick;

    [SerializeField]
    private Button botonAnteriorExcavacion;

    public void CerrarExcavacion()
    {
        //cambiamos a sprite Pico
        botonAnteriorExcavacion.GetComponent<Image>().sprite = dobleTick;
        //hacemos boton interactuable
        botonAnteriorExcavacion.GetComponent<Button>().interactable = false;

        
    }

    //asi se puede cerrar la excavacion de un hoyo concreto y no del anterior todo el rato
    public void CerrarExcavacionManual(GameObject go)
    {
        //sonido CerrarMina
        AudioManager.Instance.PlaySFX("CerrarMina");
        //cambiamos a sprite Pico
        go.GetComponent<Image>().sprite = dobleTick;
        //hacemos boton interactuable
        go.GetComponent<Button>().interactable = false;

        
    }
}
