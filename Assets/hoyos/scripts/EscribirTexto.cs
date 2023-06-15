using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscribirTexto : MonoBehaviour
{
    #region references

    //texto que se va a escribir
    [SerializeField]
    private protected TextMeshProUGUI dialogueText;

    [SerializeField]
    private GameObject continuar;

    #endregion

    #region parameters

    //diferentes lineas de texto que tengamos
    [SerializeField]
    private protected string[] lines;

    //velocidad a la que se escribe el texto
    [SerializeField]
    private float textSpeed = 0.02f;

    //para saber en que linea estamos
    private int index;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
        //dialogueText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        //click izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            //o siguiente linea
            if (dialogueText.text == lines[index])
            {
                NextLine();

            }
            //o se termina texto 
            else
            {
                continuar.SetActive(true);
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
        
    }

    public void StartDialogue()
    {
        //comenzar desde linea 0
        index = 0;
        //llamamos a corrutina que escriba las cosas
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        //para escribir cada letra cada cierto tiempo hasta completar la linea
        //como tenemos varias lineas especificamos que es linea[index] para que se sumen las lineas
        foreach (char letter in lines[index].ToCharArray())
        {
            //vamos mostrando nuestro texto letra por letra
            dialogueText.text += letter;
            //para que se muestre cada cierto tiempo textSpeed
            yield return new WaitForSeconds(textSpeed);
        }
    }

    //para pasar de lineas
    public void NextLine()
    {
        //si la linea en la que estamos no es la ultima puede pasar de linea
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        //sino cerramos dialogo porque es ultima linea
        else
        {
            dialogueText.text = string.Empty;
        }
    }

}
