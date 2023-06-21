using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

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
    //numero de excavaciones totales en la partida
    private int numeroExcavacionesTotalesPartida = 0;

    //booleano que nos diga si quedan picadas por hacer o no
    private bool quedanPicadasHoyo = true;

    //numero de segundos por partida
    private int numSecsPartida;



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

    //segun el booleano que se pase se activará en el boton selected el isInteractable o no para así solo se pueda presionar en el pico mientras no hace animacion
    public void FuncionalidadBotonPicoTemporalPonerQuitar(bool estado)
    {
        //vemos en que boton estamos actualmente
        SelectedButton buttonNow = buttonPressed();
        //accedemos a su GO para cambiar su propiedad
        buttonNow.gameObject.GetComponent<Button>().interactable = estado;
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
                //ponemos el actual a false y asi se pondrá tamaño normal
                current.Selected = false;
                Vector3 size = new Vector3(1, 1, 1);
                //pasamos el tamaño normal y el boton que debe cambiar
                NormalPala(size, current);
                //hacemos tween de movimiento
                current.transform.DOMoveY(600.5f, 0.8f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
                
                //para modificar a el hijp del hijo de button, esto es a el texto del boton click
                GameObject grandChild = current.gameObject.transform.GetChild(0).GetChild(0).gameObject;
                grandChild.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
            //si coinciden y es nuestro boton
            else
            {
                //lo ponemos al boton como pulsado si quedan picadas por hacer en ese hoyo, sino quedan picadas no
                if(QuedanPicadasHoyoReturn())
                {
                    NewbuttonPressed(current);
                    current.Selected = true;
                    //cambiamos tamaño
                    button.gameObject.transform.localScale = new Vector3(aumento, aumento, aumento);
                    //cambiamos color de letra a verde
                    button.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
                    //para modificar a el hijp del hijo de button, esto es a el texto del boton click
                    GameObject grandChild = button.gameObject.transform.GetChild(0).GetChild(0).gameObject;
                    grandChild.GetComponentInChildren<TextMeshProUGUI>().text = "Excavar";
                    current.transform.DOPause();
                    //hacemos tween sobre texto escalable
                    grandChild.transform.DOScale(new Vector3(1.35f, 1.35f, 1.35f), 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
                }
                
            }
        }
    }

    //cambia estado booleano y se le llama cuando ya no queden picadas
    public void QuedanPicadasHoyo(bool estado)
    {
        quedanPicadasHoyo = estado;
    }

    //deuelve estado de bool
    public bool QuedanPicadasHoyoReturn()
    {
        return quedanPicadasHoyo;
    }

    //lo mismo pweo reduciendo tamaño
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

    //metodo que devuelve las veces excavadas por partida
    public int NumExcavacionesTotales()
    {
        return numeroExcavacionesTotalesPartida;
    }

    //metodo que suma 1 al total de veces excavadas
    public void ExcavacionExtra()
    {
         numeroExcavacionesTotalesPartida += 1;
    }

    //metodo que guardará el numero de segundos totales de partida que lleva
    public void NumSecsPartida(int secsPartida)
    {
        numSecsPartida = secsPartida;
    }

    //metodo que devuelve el numero de segundos de partida totales que se llevan
    public int NumSecsPartidaReturn()
    {
        return numSecsPartida;
    }


}
