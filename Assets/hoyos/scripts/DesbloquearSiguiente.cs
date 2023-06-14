using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesbloquearSiguiente : MonoBehaviour
{
    //en este script habrá un metodo que se llame al pulsar 1 vez en cada boton que hará 2 cosas
    //cambiar el sprite del siguiente boton a un pico
    //hacer el siguiente boton interactuable

    [SerializeField]
    private Sprite pico;

    [SerializeField]
    private Button botonSiguienteExcavacion;

    public void DesbloquearExcavacion()
    {
        //cambiamos a sprite Pico
        botonSiguienteExcavacion.GetComponent<Image>().sprite = pico;
        //hacemos boton interactuable
        botonSiguienteExcavacion.GetComponent<Button>().interactable = true;
    }
}
