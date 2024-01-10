using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vfxManager45 : MonoBehaviour
{

    [Header("Chispas Grandes")]
    [SerializeField]
    private ParticleSystem chispasGrandes1;
    [SerializeField]
    private ParticleSystem chispasGrandes2;
    [SerializeField]
    private ParticleSystem chispasGrandes3;


    [Header("MiniChispas")]
    [SerializeField]
    private ParticleSystem miniChispas1;
    [SerializeField]
    private ParticleSystem miniChispas2;
    [SerializeField]
    private ParticleSystem miniChispas3;
    [SerializeField]
    private ParticleSystem miniChispas4;

    [Header("RoturaCristales")]
    [SerializeField]
    private ParticleSystem roturaCristales1;
    


    [Header("Tiempo entre vfx")]
    [SerializeField]
    private float chispasGrandesTime;
    [SerializeField]
    private float miniChispasTime;
    [SerializeField]
    private float roturaCristalesTime;
    [SerializeField]
    private float chispazosTime;


    [Header("Carpetas vfx")]
    [SerializeField]
    private GameObject chispasGrandesGO;
    [SerializeField]
    private GameObject miniChispasGO;
    [SerializeField]
    private GameObject explosionElectricaGO;
    [SerializeField]
    private GameObject explosionChispazoGO;
    [SerializeField]
    private GameObject roturaCristalesGO;

    [Header("Cuerpo Roto")]
    [SerializeField]
    private GameObject cuerpoRoto;
    [SerializeField]
    private GameObject cuerpoSano;


    // Start is called before the first frame update
    void Start()
    {
        //comenzamos vfx
        SetChispasGrandes(true);
        Invoke("ChispasGrandes1", chispasGrandesTime);
    }

    #region ChispasGrandes
    private void ChispasGrandes1()
    {
        chispasGrandes1.Play();
        Invoke("ChispasGrandes2", chispasGrandesTime);
    }

    private void ChispasGrandes2()
    {
        chispasGrandes2.Play();
        chispasGrandes3.Play();
        Invoke("ChispasGrandes3", chispasGrandesTime);
    }

    private void ChispasGrandes3()
    {
        SetChispasGrandes(false);
    }

    private void SetChispasGrandes(bool set)
    {
        chispasGrandesGO.SetActive(set);

        //mini Chispas
        MiniChispas1();
    }

    #endregion



    #region miniChispas
    private void MiniChispas1()
    {
        SetMiniChispas(true);
        miniChispas1.Play();
        Invoke("MiniChispas2", miniChispasTime);
    }

    private void MiniChispas2()
    {
        miniChispas2.Play();
        Invoke("MiniChispas3", miniChispasTime);
    }

    private void MiniChispas3()
    {
        miniChispas3.Play();
        miniChispas4.Play();
        Invoke("MiniChispas4", miniChispasTime);
    }

    private void MiniChispas4()
    {
        SetMiniChispas(false);

        Invoke("RoturaCristales1", roturaCristalesTime);
    }

    private void SetMiniChispas(bool set)
    {
        miniChispasGO.SetActive(set);
    }
    #endregion



    #region roturaCristales
    private void RoturaCristales1()
    {
        SetRoturaCristales(true);
        roturaCristales1.Play();
        Invoke("RoturaCristales2", roturaCristalesTime);
    }

    private void RoturaCristales2()
    {
        Chispazo();
        SetRoturaCristales(false);
    }


    private void SetRoturaCristales(bool set)
    {
        roturaCristalesGO.SetActive(set);
    }
    #endregion


    #region Chispazo
    private void Chispazo()
    {
        explosionElectricaGO.SetActive(true);
        explosionChispazoGO.SetActive(true);
        Invoke("QuitarChispazo", chispazosTime);
    }

    private void QuitarChispazo()
    {
        explosionElectricaGO.SetActive(false);
        explosionChispazoGO.SetActive(false);
        CuerpoRoto();
    }

    private void CuerpoRoto()
    {
        cuerpoSano.SetActive(false);
        cuerpoRoto.SetActive(true);
        Invoke("NextScene", 1f);
    }
    #endregion

    private void NextScene()
    {
        SceneManager.LoadScene("4.6Item");
    }


}
