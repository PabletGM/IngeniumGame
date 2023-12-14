using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComportamientoCrearPantalla : MonoBehaviour
{

    [SerializeField]
    private GameObject pantalla;
    public void Pantalla()
    {
        CrearPantalla();
        Invoke("NextScene", 6f);
    }

    private void CrearPantalla()
    {
        //activar pantalla
        pantalla.SetActive(true);
        //tamaño pantalla
        SetTweenSizePantalla();
        //desactivar pantalla
        this.gameObject.SetActive(false);
    }

    public void SetTweenSizePantalla()
    {
        pantalla.transform.DOScale(new Vector3(1,1,1), 1f);
    }

    private void NextScene()
    {
        SceneManager.LoadScene("escenaConversacionRobot2");
    }
}
