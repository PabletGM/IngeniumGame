using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntuacionTest3EnRaya : MonoBehaviour
{

    static private PuntuacionTest3EnRaya _instancePuntuacionTest3EnRaya;

    private float[] puntuacionTest3EnRaya;

    private float numeroPregunta = 0;

    private int numSecsPartida3EnRaya = 0;


    private void Awake()
    {
        //si la instancia no existe se hace este script la instancia
        if (_instancePuntuacionTest3EnRaya == null)
        {
            _instancePuntuacionTest3EnRaya = this;
            DontDestroyOnLoad(gameObject);
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        puntuacionTest3EnRaya = new float[3];
    }

    public void AñadirNumeroPregunta()
    {
        numeroPregunta++;
    }

    static public PuntuacionTest3EnRaya GetInstanceGM()
    {
        return _instancePuntuacionTest3EnRaya;
    }

    public float[] DevolverArrayRespuestas3EnRaya()
    {
        return puntuacionTest3EnRaya;
    }

    public void RellenarPrimerValorArrayPrimerTest(float valor)
    {
        puntuacionTest3EnRaya[0] = valor;
        Debug.Log(puntuacionTest3EnRaya[0]);
    }

    public void RellenarSegundorValorArraySegundoTest(float valor)
    {
        puntuacionTest3EnRaya[1] = valor;
        Debug.Log(puntuacionTest3EnRaya[1]);
    }

    public void RellenarTercerValorArrayTercerTest(float valor)
    {
        puntuacionTest3EnRaya[2] = valor;
        Debug.Log(puntuacionTest3EnRaya[2]);
    }






    public void NumSecsPartida3EnRaya(int secsPartida)
    {
        numSecsPartida3EnRaya = secsPartida;
    }

    public int TiempoPartida3EnRaya()
    {
        return numSecsPartida3EnRaya;
    }


}
