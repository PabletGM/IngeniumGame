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

    //lista de los 4 lugares Palo1 para poner ah� cada disco
    [SerializeField]
    private GameObject[] palo1Places;
    //lista de los 4 lugares Palo1 para poner ah� cada disco
    [SerializeField]
    private GameObject[] palo2Places;
    //lista de los 4 lugares Palo1 para poner ah� cada disco
    [SerializeField]
    private GameObject[] palo3Places;

    //booleano que dice si ultimo disco colocado en un palo o no
    private bool ultimoDiscoColocado = true;

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

    //ver si p�s disco coincide con alguna pos de algun hueco
    public bool VerSiDiscoEstaEnPalo(Transform posDisco)
    {
                bool discoEnPalo = false;

                //recorrer hijos del palo 1, huecos
                foreach (GameObject currentHuecoPalo1 in palo1Places)
                {
                    //en cada hijo comparamos las posiciones, si son iguales true
                    if(currentHuecoPalo1.transform.position == posDisco.position)
                    {
                        discoEnPalo = true;
                    }
                }
                
                //recorrer hijos del palo 2, huecos
                foreach (GameObject currentHuecoPalo2 in palo2Places)
                {

                    //en cada hijo comparamos las posiciones, si son iguales true
                    if (currentHuecoPalo2.transform.position == posDisco.position)
                    {
                        discoEnPalo = true;
                    }
                }

                //recorrer hijos del palo 3, huecos
                foreach (GameObject currentHuecoPalo3 in palo3Places)
                {
                    //en cada hijo comparamos las posiciones, si son iguales true
                    if (currentHuecoPalo3.transform.position == posDisco.position)
                    {
                        discoEnPalo = true;
                    }
                }

        return discoEnPalo;
    }
    

    //metodo al que le de dos widths, y devuelve true si el disco que se quiere poner(el primer argumento) es mas peque�o que el disco que estaba
    public bool PosiblePonerDiscoEncimaWidth(float widthDiscoNuevo, float widthDiscoAntiguo)
    {
        //cumple la condicion para ponerse encima
        if (widthDiscoNuevo < widthDiscoAntiguo) return true;
        else return false;
    }

    //devuelve numero de posicion segun el nombre
    public int DevolverPosicionSegunNombre(string namePos)
    {
        int num =0;
        switch (namePos)
        {
            case "Pos1":
                num = 1;
                break;
            case "Pos2":
                num = 2;
                break;
            case "Pos3":
                num = 3;
                break;
            case "Pos4":
                num = 4;
                break;
            //excepciones
            default:  
                break;

        }
        return num;
    }

    //devuelve el disco de arriba del todo
    public GameObject MetodoDevuelveDiscoMasArribaPalo(GameObject palo)
    {
        int posicionDiscoArriba = 0;
        GameObject discoMasAlto = null;
        //segun que palo sea buscamos hueco libre en una lista u otra
        switch (palo.name)
        {
            //buscar hueco en primera lista
            case "palo1":
                //recorrer hijos del palo
                foreach (GameObject currentHuecoPalo1 in palo1Places)
                {
                    //cada hijo del palo tendr� clase libre a true o false, lo comprobamos de la Pos1 a la Pos4
                    //si es true est� libre el hueco, si est� libre el hueco en la 4a pos devolvemos ese disco ya que estar� encima de todo
                    bool libre = currentHuecoPalo1.GetComponent<Libre>().GetHuecoLibre();
                    //si est� ocupada vemos que posicion es, su numero
                    if (!libre)
                    {
                        string namePos = currentHuecoPalo1.name;
                        int numero = DevolverPosicionSegunNombre(namePos);
                        //ahora comparamos su numero con el numero de posicionDiscoArriba, si es mayor, se cambia, ya que el disco est� mas alto
                        if(numero> posicionDiscoArriba) 
                        { 
                            posicionDiscoArriba = numero;
                            discoMasAlto = currentHuecoPalo1;
                        }
                    }
                    //sino seguimos buscando

                }
                break;

            //buscar hueco en segunda lista
            case "palo2":
                foreach (GameObject currentHuecoPalo2 in palo2Places)
                {

                    //cada hijo del palo tendr� clase libre a true o false, lo comprobamos de la Pos1 a la Pos4
                    //si es true est� libre el hueco, si est� libre el hueco en la 4a pos devolvemos ese disco ya que estar� encima de todo
                    bool libre = currentHuecoPalo2.GetComponent<Libre>().GetHuecoLibre();
                    //si est� ocupada vemos que posicion es, su numero
                    if (!libre)
                    {
                        string namePos = currentHuecoPalo2.name;
                        int numero = DevolverPosicionSegunNombre(namePos);
                        //ahora comparamos su numero con el numero de posicionDiscoArriba, si es mayor, se cambia, ya que el disco est� mas alto
                        if (numero > posicionDiscoArriba)
                        {
                            posicionDiscoArriba = numero;
                            discoMasAlto = currentHuecoPalo2;
                        }
                    }
                    //sino seguimos buscando

                }
                break;

            //buscar hueco en tercera lista
            case "palo3":
                foreach (GameObject currentHuecoPalo3 in palo3Places)
                {
                    //cada hijo del palo tendr� clase libre a true o false, lo comprobamos de la Pos1 a la Pos4
                    //si es true est� libre el hueco, si est� libre el hueco en la 4a pos devolvemos ese disco ya que estar� encima de todo
                    bool libre = currentHuecoPalo3.GetComponent<Libre>().GetHuecoLibre();
                    //si est� ocupada vemos que posicion es, su numero
                    if (!libre)
                    {
                        string namePos = currentHuecoPalo3.name;
                        int numero = DevolverPosicionSegunNombre(namePos);
                        //ahora comparamos su numero con el numero de posicionDiscoArriba, si es mayor, se cambia, ya que el disco est� mas alto
                        if (numero > posicionDiscoArriba)
                        {
                            posicionDiscoArriba = numero;
                            discoMasAlto = currentHuecoPalo3;
                        }
                    }
                    //sino seguimos buscando
                }
                break;

            //excepciones
            default:
                Debug.Log("No hay hueco");
                break;

        }

        return discoMasAlto;
    }

    //metodo que busca en el palo que tu pases como argumento que posiciones libres hay para devolver as� una posicion
    //solo devolver� una posicion sino se intenta poner un disco mas grande en uno mas peque�o
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
                    //cada hijo del palo tendr� clase libre a true o false, lo comprobamos de la Pos1 a la Pos4
                    //si es true est� libre el hueco
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
                    
                    //cada hijo del palo tendr� clase libre a true o false, lo comprobamos de la Pos1 a la Pos4
                    //si es true est� libre el hueco
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
                    //cada hijo del palo tendr� clase libre a true o false, lo comprobamos de la Pos1 a la Pos4
                    //si es true est� libre el hueco
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

    //en todo momento sabemos cual es el disco que est� mas arriba en cada palo
    void Update()
    {
        //llamamos a metodo de GameManager que devuelva el disco que est� mas arriba una vez se ha colocado el ultimo
        GameObject discoMasAltoPalo1 = MetodoDevuelveDiscoMasArribaPalo(Palos[0].gameObject);
        GameObject discoMasAltoPalo2 = MetodoDevuelveDiscoMasArribaPalo(Palos[1].gameObject);
        GameObject discoMasAltoPalo3 = MetodoDevuelveDiscoMasArribaPalo(Palos[2].gameObject);
    }
}
