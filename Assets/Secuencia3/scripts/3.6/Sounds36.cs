using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds36 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //invoca metodo para sonido andar
        Invoke("SonidoAndar",0f);

        //paramos sonidos en 11 segundos que es la duracion de la animacion
        Invoke("StopSound",9f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SonidoAndar()
    {
        AudioManagerBengalas.instance.PlaySFX("walking", 1f);
    }

    private void StopSound()
    {
        AudioManagerBengalas.instance.StopSFX();
    }
}
