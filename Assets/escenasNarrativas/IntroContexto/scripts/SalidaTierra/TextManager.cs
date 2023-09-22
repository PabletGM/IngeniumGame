using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{

    //array de textos
    [SerializeField]
    private GameObject[] textos;

    private int numMaxTextos = 3;

    private int numTextoActual = 0; //puede ser el 0,1 o 2

    [SerializeField]
    private GameObject buttonContinue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
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

    
    public void  PasarSiguienteTexto()
    {
        //sumamos uno
        if(numTextoActual< textos.Length-1)
        {
            numTextoActual++;
        }
        //si ha llegado al final
        else
        {
            buttonContinue.SetActive(true);
        }
       
        //ponemos texto
        PonerTextoActivo(textos[numTextoActual]);
    }

    public void VolverAnteriorTexto()
    {
        //quitamos uno
        if(numTextoActual>0)
        {
            numTextoActual--;
        }
        
        //ponemos texto
        PonerTextoActivo(textos[numTextoActual]);
    }

    public void PasarSiguienteEscenaIntermedia()
    {
        //cargas escena intermedia
        SceneManager.LoadScene("ExplosionTierra");
    }




}
