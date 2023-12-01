using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{

    //array de textos
    [SerializeField]
    private GameObject[] textos;

    [SerializeField]
    private GameObject DialoguePanel;

    private int numMaxTextos = 2;

    private int numTextoActual = 0; //puede ser el 0,1 o 2

    private float tiempoEsperaEntreTextos = 5f;



    private void Awake()
    {
        numMaxTextos = textos.Length;
        Debug.Log(numMaxTextos);
    }
    //metodo que quita todos los textos menos el que elijas
    public void PonerTextoActivo(GameObject textoElegido)
    {
        //quita todos los textos 
        for(int i=0; i<numMaxTextos; i++)
        {
            if (textos[i] == textoElegido)
            {
                textos[i].SetActive(true);
            }
            else
            {
                textos[i].SetActive(false);
            }
            
        }
    }

    private void OnEnable()
    {
        if (numTextoActual < textos.Length && DialoguePanel.activeInHierarchy)
        {
            InvokeRepeating("PasarSiguienteTexto", 3f, tiempoEsperaEntreTextos);
        }
    }


    public void  PasarSiguienteTexto()
    {
        //sumamos uno
        if(numTextoActual< textos.Length-1 && DialoguePanel.activeInHierarchy)
        {
            numTextoActual++;
        }
        //si ha llegado al final
        else
        {
            //quitamos texto
            this.gameObject.transform.parent.gameObject.SetActive(false);
            
            PasarSiguienteEscenaIntermedia();           
        }
       
        //ponemos texto
        if(DialoguePanel.activeInHierarchy)
        {
            PonerTextoActivo(textos[numTextoActual]);
        }
       
    }

    public void VolverAnteriorTexto()
    {
        //quitamos uno si esta active el gameObject
        if(numTextoActual>0&& DialoguePanel.activeInHierarchy)
        {
            numTextoActual--;
        }
        
        //ponemos texto
        if(DialoguePanel.activeInHierarchy)
        {
            PonerTextoActivo(textos[numTextoActual]);
        }
        
    }

    public void PasarSiguienteEscenaIntermedia()
    {
        if(SceneManager.GetActiveScene().name == "SalidaTierra")
        {
            //cargas escena intermedia
            SceneManager.LoadScene("ExplosionTierra");
        }

        else if(SceneManager.GetActiveScene().name == "LlegadaPlaneta")
        {
            SceneManager.LoadScene("LogoTitulo");
        }
        else if (SceneManager.GetActiveScene().name == "viajeGalaxia")
        {
            SceneManager.LoadScene("LlegadaPlaneta");
        }
    }




}
