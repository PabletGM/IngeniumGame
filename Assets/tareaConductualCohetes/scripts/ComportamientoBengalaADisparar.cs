using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoBengalaADisparar : MonoBehaviour
{
    private float velocidad = 4f;
    private float aceleracion = 0.25f;
    [SerializeField]
    private GameObject vfxDespegue;
    public void DespegarCohete()
    {
        InvokeRepeating("CoheteVolar",0.1f, 0.1f);
    }

    private void CoheteVolar()
    { 
        //le aplicamos velocidad y lo movemos en una direccion
        transform.Translate(Vector2.up * velocidad * Time.deltaTime);
        velocidad += aceleracion;
    }

    public void DespegueVFX()
    {
        vfxDespegue.SetActive(true);
        vfxDespegue.GetComponent<ParticleSystem>().Play();
        //vfxDespegue.GetComponent<ParticleSystem>().Stop();
        //vfxDespegue.SetActive(false);
    }
}
