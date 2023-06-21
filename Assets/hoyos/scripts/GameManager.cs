using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    //lista de objetos botones generales que pueden ser seleccionados con clase SelectedButton
    [SerializeField]
    private SelectedButton[] buttons;
    //saber en que posicion de boton estamos
    private int posBotonHoyo;
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

    //array de valores que guardará el numero de toques que se ha hecho en cada hoyo y que se pasará luego
    //a la base de datos
    public int[] valoresPicadasHoyoIndividual;
    //numero de posicion del hoyo a devolver
    int valorArray = 0;



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

        //inicializamos tamaño fijo array picadas
        valoresPicadasHoyoIndividual = new int[6];
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

    //metodo que 1.Averigua en que hoyo estamos y su numero 2.Añade al array y su posición el numerodePicadas del hoyo
    public void ModificarArrayPicadasTotalesCadaHoyo(int numeroPicadasTotalesHoyo)
    {
        int posicionArray = AveriguarHoyoDevolverNumero();
        valoresPicadasHoyoIndividual[posicionArray] = numeroPicadasTotalesHoyo;
    }

    public int AveriguarHoyoDevolverNumero()
    {
        
        //vemos que hoyo-boton esta pulsado
        SelectedButton button = buttonPressed();
        string nameButton = button.gameObject.name;
        //segun que nombre tenga asignamos un valor a el numero del array
        switch (nameButton)
        {
            case "Hoyo0":
                valorArray = 0;
                break;
            case "Hoyo1":
                valorArray = 1;
                break;
            case "Hoyo2":
                valorArray = 2;
                break;
            case "Hoyo3":
                valorArray = 3;
                break;
            case "Hoyo4":
                valorArray = 4;
                break;
            case "Hoyo5":
                valorArray = 5;
                break;
            default:
                break;
        }

        return valorArray;
    }

    //metodo que devuelva el array de picadas que tiene cada hoyo
    public int[] DevolverPicadasHoyo()
    {
        return valoresPicadasHoyoIndividual;
    }

    //averiguamos posicion hoyo actual, pasamos al siguiente y lo ponemos como boton pressed
    public void PasarAlSiguienteHoyo()
    {
        //buscamos pos hoyo actual
        posBotonHoyo = AveriguarHoyoDevolverNumero();
        //cambiamos hoyo actual al siguiente, si hay siguiente
        if(posBotonHoyo+1 < buttons.Length)
        {
            NewbuttonPressed(buttons[posBotonHoyo + 1]);
        }
       
    }

    //pasas un numero y te devuelve ese boton
    public SelectedButton botonDevueltoIndice(int i)
    {
        switch (i)
        {
            case 0:
                return buttons[0];
            case 1:
                return buttons[1];
            case 2:
                return buttons[2];
            case 3:
                return buttons[3];
            case 4:
                return buttons[4];
            case 5:
                return buttons[5];
            default:
                return buttons[0];
        }
    }



}
