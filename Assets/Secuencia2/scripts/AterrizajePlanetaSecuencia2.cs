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
    private ParticleSystem vfxAterrizaje1Fuego;
    [SerializeField]
    private ParticleSystem vfxAterrizaje2Fuego;

    [SerializeField]
    private GameObject humo;

    private int cantidadParticulasAQuitarPorIteracion = 7;

    [SerializeField]
    private string nameSecuencia2;
    [SerializeField]
    private string nameSecuencia5;

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
        if(SceneManager.GetActiveScene().name == nameSecuencia2)
        {
            AudioManagerCirculos.instance.PlaySFXDuracion("Aterrizaje", 2f);
        }
        else if(SceneManager.GetActiveScene().name == nameSecuencia5)
        {

        }
        
    }

    private void QuitarFuncionalidadEscena()
    {
        //quitamos animacion nave
        nave.GetComponent<Animator>().enabled = false;
    }
    private void PonerHumo()
    {
        humo.SetActive(true);
        if (SceneManager.GetActiveScene().name == nameSecuencia5)
        {
            vfxAterrizaje1Fuego.gameObject.SetActive(false);
            vfxAterrizaje2Fuego.gameObject.SetActive(false);
        }
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
