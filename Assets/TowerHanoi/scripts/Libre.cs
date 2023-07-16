using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libre : MonoBehaviour
{
    //clase que dice si está libre o no el hueco, por defecto true
    public bool huecoLibre = true;

    //dice nombre disco actual que está en el hueco, ponemos el inicial que será el rojo disco en el caso de Pos4
    [SerializeField]
    private string nombreDiscoActual;

    GameManagerHanoi _myGameManagerHanoi;


    private void Start()
    {
        _myGameManagerHanoi = GetComponent<GameManagerHanoi>();
    }


    public void SetNombreDiscoActual(string nombre)
    {
        nombreDiscoActual = nombre;
    }

    //metodo para cambiar estado de huecoLibre
    public void SetHuecoLibre(bool set)
    {
        huecoLibre = set;
    }

    public string GetNombreDiscoActual()
    {
        return nombreDiscoActual;
    }

    

    //metodo que devuelve estado de huecoLibre
    public bool GetHuecoLibre()
    {
        return huecoLibre;
    }

}
