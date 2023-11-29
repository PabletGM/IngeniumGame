using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private GameObject[] asteroides;

    [SerializeField]
    private GameObject explosionTierra;

    [SerializeField]
    private GameObject cenizasFuego;

    [SerializeField]
    private GameObject explosionTocha;

    private int numMaxAsteroides = 3;

    public Sprite planetaQuemado;

    public SpriteRenderer planeta;



    // Start is called before the first frame update
    void Start()
    {
       
    }

    //destruir todos los asteroides
    public void DestroyAsteroides()
    {
        for (int i = 0; i < numMaxAsteroides; i++)
        {
            asteroides[i].SetActive(false);
        }
    }

    //aparecen todos los asteroides
    public void AppearAsteroides()
    {
        for (int i = 0; i < numMaxAsteroides; i++)
        {
            asteroides[i].SetActive(true);
        }
    }

    public void ExplosionAsteroides()
    {
        //DestroyAsteroides();
        //vfx
        explosionTierra.SetActive(true);
        explosionTierra.GetComponent<ParticleSystem>().Play();

        Invoke("ExplosionTocha", 1f);
    }

    public void ExplosionTocha()
    {
        explosionTocha.SetActive(true);
        explosionTocha.GetComponent<ParticleSystem>().Play();
        CambiarImagenPlaneta();
        Invoke("CenizasFuego", 0.5f);
    }

    public void CenizasFuego()
    {
        cenizasFuego.SetActive(true);
        cenizasFuego.GetComponent<ParticleSystem>().Play();

    }

    public void CambiarImagenPlaneta()
    {
        //cambiamos imagen de planeta
        planeta.sprite = planetaQuemado;
        Invoke("NextScene", 1f);
    }



    public void Play()
    {
        //AppearAsteroides();
        //en 2.5 segundos que es lo que tarda la animacion de los asteroides los explotamos
        Invoke("ExplosionAsteroides", 1f);
    }

    public void NextScene()
    {
        SceneManager.LoadScene("viajeGalaxia");
    }
}
