using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //lista de objetos botones generales que pueden ser seleccionados con clase SelectedButton
    [SerializeField]
    private SelectedButton[] buttons;
    //lista de objetos Button texto de botones especificos 
    [SerializeField]
    private GameObject [] buttonTextClick;
    //guardamos boton pulsado
    private SelectedButton selectionButton;

    

    /// <summary>
    /// Unique GameManager instance (Singleton Pattern).
    /// </summary>
    static private GameManager _instance;

    private void Awake()
    {
        //si la instancia no existe se hace este script la instancia
        if (_instance == null)
        {
            _instance = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }

    static public GameManager GetInstance()
    {
        return _instance;
    }

    //se pasa el aumento que quieres hacer y la instancia a la que haces el aumento
    public void ChangeBiggerPala(float aumento,SelectedButton button)
    {
        //buscamos el objeto selected de la lista placedObjects
        foreach (SelectedButton current in buttons)
        {
            //sino coinciden el boton que he pasado y que estoy buscando con el actual de la lista
            if (button != current)
            {
                //ponemos el actual a false y asi se pondr� tama�o normal
                current.Selected = false;
                Vector3 size = new Vector3(1, 1, 1);
                //pasamos el tama�o normal y el boton que debe cambiar
                NormalPala(size, current);
            }
            //si coinciden y es nuestro boton
            else
            {
                //lo ponemos al boton como pulsado
                NewbuttonPressed(current);
                current.Selected = true;
                //cambiamos tama�o
                button.gameObject.transform.localScale = new Vector3(aumento, aumento, aumento);
                //cambiamos color de letra a verde
                button.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
                //
                
            }
        }
    }

    //lo mismo pweo reduciendo tama�o
    public void NormalPala(Vector3 size, SelectedButton button)
    {
        button.gameObject.transform.localScale = size;
        button.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    }

    //devuelve el boton que ha sido pulsado
    public SelectedButton buttonPressed()
    {
        return selectionButton;
    }

    //actualiza el boton que ha sido pulsado
    public void NewbuttonPressed(SelectedButton buttonSelect)
    {
        selectionButton = buttonSelect;
    }



}
