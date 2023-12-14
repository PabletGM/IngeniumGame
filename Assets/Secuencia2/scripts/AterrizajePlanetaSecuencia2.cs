using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AterrizajePlanetaSecuencia2 : MonoBehaviour
{
    [SerializeField]
    private GameObject nave;

    [Header("ParticleSystem")]
    [SerializeField]
    private ParticleSystem vfxAterrizaje1;
    [SerializeField]
    private ParticleSystem vfxAterrizaje2;

    [SerializeField]
    private GameObject humo;

    private int cantidadParticulasAQuitarPorIteracion = 7;

    // al activarse comenzamos animacion
    private void OnEnable()
    {
        nave.SetActive(true);
        Invoke("SonidoNaveAterrizando", 0f);
        //hacemos efecto desaparecer particulas
        StartCoroutine(BajarMaxParticlesCanon1());
        StartCoroutine(BajarMaxParticlesCanon2());
        //hacemos efecto humo
        
        Invoke("PonerHumo", 2.8f);
        Invoke("QuitarFuncionalidadEscena", 5f);
    }

    private void SonidoNaveAterrizando()
    {
        AudioManagerCirculos.instance.PlaySFXDuracion("Aterrizaje",2f);
    }

    private void QuitarFuncionalidadEscena()
    {
        //quitamos animacion nave
        nave.GetComponent<Animator>().enabled = false;
    }
    private void PonerHumo()
    {
        humo.SetActive(true);
    }

    // va bajando poco a poco max particles
    private IEnumerator BajarMaxParticlesCanon1()
    {
        while (vfxAterrizaje1.main.maxParticles > 0)
        {
            vfxAterrizaje1.maxParticles -= cantidadParticulasAQuitarPorIteracion;

            // Esperamos 0.1 segundos hasta repetirlo
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator BajarMaxParticlesCanon2()
    {
        while (vfxAterrizaje2.main.maxParticles > 0)
        {
            vfxAterrizaje2.maxParticles -= cantidadParticulasAQuitarPorIteracion;

            // Esperamos 0.1 segundos hasta repetirlo
            yield return new WaitForSeconds(0.1f);
        }
    }
}
