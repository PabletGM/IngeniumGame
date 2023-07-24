using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscribirTextoHanoi : MonoBehaviour
{
    #region references

    //texto que se va a escribir
    [SerializeField]
    private protected TextMeshProUGUI dialogueText;

    [SerializeField]
    private GameObject continuar;

    [SerializeField]
    private GameObject mision;

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

    private int clickCount = 0;

    private bool permisoEscritura = true;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //click izquierdo y permiso para escribir
        if (Input.GetMouseButtonDown(0) && permisoEscritura)
        {
            //sumamos 1 al clickcount
            clickCount++;
            //si el numero de clicks es mayor que numero de lineas aparece boton entendido
            if(clickCount > lines.Length)
            {
                //mision.SetActive(false);
                continuar.SetActive(true);
                
                //cuando ya no hay mas lineas ya no hay mas permiso de escritura
                permisoEscritura  = false;
            }
            //o siguiente linea
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            //o se termina texto 
            else
            {
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
        //sonido escribir texto
        AudioManagerHanoi.Instance.PlaySFX("Escribir");
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
