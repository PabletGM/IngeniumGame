using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libre : MonoBehaviour
{
    //clase que dice si está libre o no el hueco, por defecto true
    public bool huecoLibre = true;



    //metodo para cambiar estado de huecoLibre
    public void SetHuecoLibre(bool set)
    {
        huecoLibre = set;
    }

    //metodo que devuelve estado de huecoLibre
    public bool GetHuecoLibre()
    {
        return huecoLibre;
    }
}
