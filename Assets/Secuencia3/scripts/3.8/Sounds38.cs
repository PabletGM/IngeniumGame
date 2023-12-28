using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds38 : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        //musica salvados
        MusicaSalvados();

        //invoca metodo para sonido andar
        Invoke("SonidoSaltoPersonaje", 2f);

    }

    

    private void SonidoSaltoPersonaje()
    {
        AudioManagerBengalas.instance.PlaySFX("saltoPersonaje", 1f);
    }

    private void MusicaSalvados()
    {
        AudioManagerBengalas.instance.PlayMusic("salvados", 1f);
    }

    private void SonidoEntrarNave()
    {
        //AudioManagerBengalas.instance.PlaySFX("", 1f);
    }

    public void SonidoFadeOut()
    {
        AudioManagerBengalas.instance.PlayMusic("transitionFadeOut", 1f);
    }

    public void StopSound()
    {
        Debug.Log("h");
        AudioManagerBengalas.instance.StopMusic();
    }
}
