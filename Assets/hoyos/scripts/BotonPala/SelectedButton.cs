using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//script que hará el feedback visual del boton cuando está pulsado
public class SelectedButton : MonoBehaviour
{
    
    private bool isSelected = false;
    private float aumentoSize = 1.2f;
    GameManager _myGameManager;

    //devuelve estado del boton
    public bool Selected
    {
        get
        {
            return this.isSelected;
        }

        set
        {
            isSelected = value;
        }
    }

    //cambia estado del boton y tiene efecto, el resto de botones los pone a false
    public void ChangeSelectedButton()
    {
        //ponemos el valor contrario a este boton
        isSelected = !isSelected;
        //se pone ese valor
        Selected = isSelected;
        //se hace mas grande la pala correspondiente y el resto se quedan normales
        _myGameManager.ChangeBiggerPala(aumentoSize, this);
    }
    // Start is called before the first frame update
    void Start()
    {
        //asi es como asociamos el GameManager con el UI
        _myGameManager = GameManager.GetInstance();
    }

    //al hacer click al boton que se llame al metodo
    private void Awake()
    {
        //al hacer click metodo que quite el canvas
        this.gameObject.GetComponent<Button>().onClick.AddListener(ChangeSelectedButton);
    }
   


}
