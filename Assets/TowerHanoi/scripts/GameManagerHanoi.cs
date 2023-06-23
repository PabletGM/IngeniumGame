using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerHanoi : MonoBehaviour
{
    //lista de objetos Palo metalico que soporta a los discos
    [SerializeField]
    private ItemSlot[] Palos;

    //singleton
    static private GameManagerHanoi _instanceHanoi;

    //lista de los discos que hay en juego
    [SerializeField]
    private DragDrop[] listaDiscos;

    //ultimo disco seleccionado
    private GameObject ultimoDiscoSeleccionado;
    //info palo ultimo disco seleccionado
    private GameObject ultimoPaloSeleccionado;
    //info posicion del palo ultimo disco seleccionado
    private GameObject ultimaPosicionSeleccionada;

    //lista de los 4 lugares Palo1 para poner ahí cada disco
    [SerializeField]
    private GameObject[] palo1Places;
    //lista de los 4 lugares Palo1 para poner ahí cada disco
    [SerializeField]
    private GameObject[] palo2Places;
    //lista de los 4 lugares Palo1 para poner ahí cada disco
    [SerializeField]
    private GameObject[] palo3Places;

    private void Awake()
    {
        //si la instancia no existe se hace este script la instancia
        if (_instanceHanoi == null)
        {
            _instanceHanoi = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }

    static public GameManagerHanoi GetInstance()
    {
        return _instanceHanoi;
    }

    //metodo que compruebe los 3 palos y pone todos como rayCastTarget = true;
    public void HabilitarPalos()
    {

        //ponemos todos con raycastTarget = true
        foreach (ItemSlot currentPalo in Palos)
        {
            Image imagePalo = currentPalo.gameObject.GetComponent<Image>();
            imagePalo.raycastTarget = true;
        }
    }

    //metodo que deshabilita los 3 palos y pone todos como rayCastTarget = false; para que se puedan coger discos
    public void DesHabilitarPalos()
    {

        //ponemos todos con raycastTarget = true
        foreach (ItemSlot currentPalo in Palos)
        {
            Image imagePalo = currentPalo.gameObject.GetComponent<Image>();
            imagePalo.raycastTarget = false;
        }
    }

    //metodo que busca en el palo que tu pases como argumento que posiciones libres hay para devolver así una posicion
    public GameObject BuscarHuecoEnPalo(GameObject palo)
    {
        //miramos que palo es el pasado en argumentos
        string namePalo = palo.name;
        //segun que palo sea buscamos hueco libre en una lista u otra
        switch (namePalo)
        {
            //buscar hueco en primera lista
            case "palo1":
                //recorrer hijos del palo
                foreach (GameObject currentHuecoPalo1 in palo1Places)
                {
                    //cada hijo del palo tendrá clase libre a true o false, lo comprobamos de la Pos1 a la Pos4
                    //si es true está libre el hueco
                    bool libre =currentHuecoPalo1.GetComponent<Libre>().GetHuecoLibre();
                    //si esta libre el hueco enviamos este como GameObject
                    if(libre) return currentHuecoPalo1;
                    //sino seguimos buscando

                }
                break;
            //buscar hueco en segunda lista
            case "palo2":
                foreach (GameObject currentHuecoPalo2 in palo2Places)
                {
                    
                    //cada hijo del palo tendrá clase libre a true o false, lo comprobamos de la Pos1 a la Pos4
                    //si es true está libre el hueco
                    bool libre = currentHuecoPalo2.GetComponent<Libre>().GetHuecoLibre();
                    //si esta libre el hueco enviamos este como GameObject
                    if (libre) return currentHuecoPalo2;
                    //sino seguimos buscando

                }
                break;
            //buscar hueco en tercera lista
            case "palo3":
                foreach (GameObject currentHuecoPalo3 in palo3Places)
                {
                    //cada hijo del palo tendrá clase libre a true o false, lo comprobamos de la Pos1 a la Pos4
                    //si es true está libre el hueco
                    bool libre = currentHuecoPalo3.GetComponent<Libre>().GetHuecoLibre();
                    //si esta libre el hueco enviamos este como GameObject
                    if (libre) return currentHuecoPalo3;
                    //sino seguimos buscando
                }
                break;
            //excepciones
            default:
                Debug.Log("No hay hueco");
                break;
            
        }

        return null;

    }


    //ponemos cual ha sido el ultimo disco seleccionado
    public void SetUltimoDiscoSeleccionado(GameObject ultimoDisco)
    {
        ultimoDiscoSeleccionado = ultimoDisco;
    }

    //vemos cual ha sido el ultimo disco seleccionado
    public GameObject GetUltimoDiscoSeleccionado()
    {
        return ultimoDiscoSeleccionado;
    }

    //para poner ultima posicion del palo y posicion del disco o ultimo movimiento hecho
    public void SetPaloYPosicionUltimoDiscoSeleccionado(GameObject paloUltimoDisco, GameObject posicionPaloUltimoDisco)
    {
        ultimoPaloSeleccionado = paloUltimoDisco;
        ultimaPosicionSeleccionada = posicionPaloUltimoDisco;
    }

    //para saber ultima  palo del ultimo disco ultimo movimiento hecho
    public GameObject GetPaloUltimoDiscoSeleccionado()
    {
        return ultimoPaloSeleccionado;
    }

    //para saber ultima posicion del hueco  del ultimo disco ultimo movimiento hecho
    public GameObject GetPosicionUltimoDiscoSeleccionado()
    {
        return ultimaPosicionSeleccionada;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
