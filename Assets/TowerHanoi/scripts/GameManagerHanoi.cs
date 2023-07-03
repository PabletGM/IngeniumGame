using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
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
    private GameObject[] listaDiscos;

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

    //ver si pòs disco coincide con alguna pos de algun hueco
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
    

    //metodo al que le de dos widths, y devuelve true si el disco que se quiere poner(el primer argumento) es mas pequeño que el disco que estaba
    public bool PosiblePonerDiscoEncimaWidth(float widthDiscoNuevo, float widthDiscoAntiguo)
    {
        //cumple la condicion para ponerse encima
        if (widthDiscoNuevo <= widthDiscoAntiguo) return true;
        else return false;
    }

    //para ver si colocamos el disco en el hueco de la posicion Libre por encima debemos ver si
    //el width del discoNuevo es menor que el width del discoMasAlto
    //para esto llamamos a metodo de GameManager que compare width del discoMasAlto y el discoNuevo
    public bool ComparacionWidthDiscos(GameObject discoNuevo, GameObject palo)
    {
        bool posiblePonerDiscoNuevoEncimaWidth = false;
        //esta fallando porque el discoNuevo y el discoMasAlto lo pilla como el mismo, y el discoMasAlto deberia ser el discoAnterior
        //tenemos ya el GO del discoNuevo, buscamos el GO del discoMasAlto
        string nombreDiscoMasArriba = MetodoDevuelveDiscoMasArribaPalo(palo);
        GameObject discoMasAlto = DevolverDiscoSegunNombre(nombreDiscoMasArriba);

        //sabiendo ahora estos 2 gameObjects podemos comparar sus widths
        //si hay discoMasAlto y no esta vacio el palo sin discos
        if(discoMasAlto!=null)
        {
            posiblePonerDiscoNuevoEncimaWidth = PosiblePonerDiscoEncimaWidth(discoNuevo.GetComponent<RectTransform>().sizeDelta.x, discoMasAlto.GetComponent<RectTransform>().sizeDelta.x);
        }
        else
        {
            posiblePonerDiscoNuevoEncimaWidth=false;
        }
        

        return posiblePonerDiscoNuevoEncimaWidth;
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

    //buscamos el palo y miramos si tiene algun disco o no
    public bool PaloVacioONo(GameObject palo)
    {
        //por defecto true
        bool paloVacio = true;
        //si tiene disco devolvemos false
        //sino tiene ninguno devolvemos true
        switch (palo.name)
        {
            //buscar hueco en primera lista
            case "palo1":
                //recorrer hijos del palo
                foreach (GameObject currentHuecoPalo1 in palo1Places)
                {
                    
                    bool libre = currentHuecoPalo1.GetComponent<Libre>().GetHuecoLibre();
                    //si está ocupada es que hay disco
                    if (!libre)
                    {
                        paloVacio = false;
                        return paloVacio;
                    }
                    //sino seguimos buscando

                }
                break;

            //buscar hueco en segunda lista
            case "palo2":
                foreach (GameObject currentHuecoPalo2 in palo2Places)
                {

                    bool libre = currentHuecoPalo2.GetComponent<Libre>().GetHuecoLibre();
                    //si está ocupada es que hay disco
                    if (!libre)
                    {
                        paloVacio = false;
                        return paloVacio;
                    }
                    //sino seguimos buscando

                }
                break;

            //buscar hueco en tercera lista
            case "palo3":
                foreach (GameObject currentHuecoPalo3 in palo3Places)
                {
                    bool libre = currentHuecoPalo3.GetComponent<Libre>().GetHuecoLibre();
                    //si está ocupada es que hay disco
                    if (!libre)
                    {
                        paloVacio = false;
                        return paloVacio;
                    }
                    //sino seguimos buscando
                }
                break;

            //excepciones
            default:
                Debug.Log("No hay hueco");
                break;

        }
        return paloVacio;
    }

    //devuelve gameObject disco segun el nombre
    public GameObject DevolverDiscoSegunNombre(string nameDisco)
    {
        GameObject discoDevuelto= null;
        switch (nameDisco)
        {
            case "disco1Imagen":
                discoDevuelto = listaDiscos[0];
                break;
            case "disco2Imagen":
                discoDevuelto = listaDiscos[1];
                break;
            case "disco3Imagen":
                discoDevuelto = listaDiscos[2];
                break;
            case "disco4Imagen":
                discoDevuelto = listaDiscos[3];
                break;
            //excepciones
            default:
                break;

        }
        return discoDevuelto;
    }

    //devuelve el nombre del disco de arriba del todo
    public string MetodoDevuelveDiscoMasArribaPalo(GameObject palo)
    {
        int posicionDiscoArriba = 0;
        //esta será la posicion del disco más alto
        GameObject PosiciondiscoMasAlto = null;
        //disco mas alto nombre
        string name = "";
        //segun que palo sea buscamos disco mas arriba
        switch (palo.name)
        {
            //buscar hueco en primera lista
            case "palo1":
                //recorrer hijos del palo
                foreach (GameObject currentHuecoPalo1 in palo1Places)
                {
                    //cada hijo del palo tendrá clase libre a true o false, lo comprobamos de la Pos1 a la Pos4
                    //si es true está libre el hueco, si está libre el hueco en la 4a pos devolvemos ese disco ya que estará encima de todo
                    bool libre = currentHuecoPalo1.GetComponent<Libre>().GetHuecoLibre();
                    //si está ocupada vemos que posicion es, su numero
                    if (!libre)
                    {
                        string namePos = currentHuecoPalo1.name;
                        int numero = DevolverPosicionSegunNombre(namePos);
                        //ahora comparamos su numero con el numero de posicionDiscoArriba, si es mayor, se cambia, ya que el disco está mas alto
                        if(numero> posicionDiscoArriba) 
                        { 
                            posicionDiscoArriba = numero;
                            PosiciondiscoMasAlto = currentHuecoPalo1;
                            //hacemos metodo que busque él disco mas alto buscandolo en su propio hueco en sus variables
                            if(PosiciondiscoMasAlto.GetComponent<Libre>().GetNombreDiscoActual()!="")
                            {
                                //comprobamos que no devuelva el nombre de un disco o posicion que ya no está
                                name = PosiciondiscoMasAlto.GetComponent<Libre>().GetNombreDiscoActual();
                            }
                            

                        }
                    }
                    //sino seguimos buscando

                }
                break;

            //buscar hueco en segunda lista
            case "palo2":
                foreach (GameObject currentHuecoPalo2 in palo2Places)
                {

                    //cada hijo del palo tendrá clase libre a true o false, lo comprobamos de la Pos1 a la Pos4
                    //si es true está libre el hueco, si está libre el hueco en la 4a pos devolvemos ese disco ya que estará encima de todo
                    bool libre = currentHuecoPalo2.GetComponent<Libre>().GetHuecoLibre();
                    //si está ocupada vemos que posicion es, su numero
                    if (!libre)
                    {
                        string namePos = currentHuecoPalo2.name;
                        int numero = DevolverPosicionSegunNombre(namePos);
                        //ahora comparamos su numero con el numero de posicionDiscoArriba, si es mayor, se cambia, ya que el disco está mas alto
                        if (numero > posicionDiscoArriba)
                        {
                            posicionDiscoArriba = numero;
                            PosiciondiscoMasAlto = currentHuecoPalo2;
                            //hacemos metodo que busque él disco mas alto buscandolo en su propio hueco en sus variables
                            name = PosiciondiscoMasAlto.GetComponent<Libre>().GetNombreDiscoActual();

                        }
                    }
                    //sino seguimos buscando

                }
                break;

            //buscar hueco en tercera lista
            case "palo3":
                foreach (GameObject currentHuecoPalo3 in palo3Places)
                {
                    //cada hijo del palo tendrá clase libre a true o false, lo comprobamos de la Pos1 a la Pos4
                    //si es true está libre el hueco, si está libre el hueco en la 4a pos devolvemos ese disco ya que estará encima de todo
                    bool libre = currentHuecoPalo3.GetComponent<Libre>().GetHuecoLibre();
                    //si está ocupada vemos que posicion es, su numero
                    if (!libre)
                    {
                        string namePos = currentHuecoPalo3.name;
                        int numero = DevolverPosicionSegunNombre(namePos);
                        //ahora comparamos su numero con el numero de posicionDiscoArriba, si es mayor, se cambia, ya que el disco está mas alto
                        if (numero > posicionDiscoArriba)
                        {
                            posicionDiscoArriba = numero;
                            PosiciondiscoMasAlto = currentHuecoPalo3;
                            //hacemos metodo que busque él disco mas alto buscandolo en su propio hueco en sus variables
                            name = PosiciondiscoMasAlto.GetComponent<Libre>().GetNombreDiscoActual();

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

        return name;
    }

    

    //metodo que busca en el palo que tu pases como argumento que posiciones libres hay para devolver así una posicion
    //solo devolverá una posicion sino se intenta poner un disco mas grande en uno mas pequeño
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

    //pero tenemos que ver que discos tiene en concreto cada palo
    public void QuitarPermisoDiscosExceptoArriba(string discoMasAltoName, GameObject paloActual)
    {
        //almacenamos en un array los discos que si estan en el palo y podemos modificar
        GameObject[] listaDiscosModificablesPalo = new GameObject[4];
        int contadorListadiscosModificables = 0;
        //miramos en que palo estamos
        switch (paloActual.gameObject.name)
        {
            //una vez lo sabemos miramos que huecos libres tenemos en cada palo
            case "palo1":
                foreach (GameObject currentHuecoPalo1 in palo1Places)
                {
                    //miramos en cada hueco y su script Libre para ver que disco tiene
                    string name = currentHuecoPalo1.GetComponent<Libre>().GetNombreDiscoActual();
                    //si el nombre es diferente de "", lo añadimos a los discos modificables
                    if(name!= "")
                    {
                        //cogemeos disco segun el nombre
                        GameObject discoValido = DevolverDiscoSegunNombre(name);
                        listaDiscosModificablesPalo[contadorListadiscosModificables] = discoValido;
                        contadorListadiscosModificables++;
                    }
                }
                break;

            case "palo2":
                foreach (GameObject currentHuecoPalo2 in palo2Places)
                {
                    //miramos en cada hueco y su script Libre para ver que disco tiene
                    string name = currentHuecoPalo2.GetComponent<Libre>().GetNombreDiscoActual();
                    //si el nombre es diferente de "", lo añadimos a los discos modificables
                    if (name != "")
                    {
                        //cogmeos disco segun el nombre
                        GameObject discoValido = DevolverDiscoSegunNombre(name);
                        listaDiscosModificablesPalo[contadorListadiscosModificables] = discoValido;
                        contadorListadiscosModificables++;
                    }
                }
                break;

            case "palo3":
                foreach (GameObject currentHuecoPalo3 in palo3Places)
                {
                    //miramos en cada hueco y su script Libre para ver que disco tiene
                    string name = currentHuecoPalo3.GetComponent<Libre>().GetNombreDiscoActual();
                    //si el nombre es diferente de "", lo añadimos a los discos modificables
                    if (name != "")
                    {
                        //cogmeos disco segun el nombre
                        GameObject discoValido = DevolverDiscoSegunNombre(name);
                        listaDiscosModificablesPalo[contadorListadiscosModificables] = discoValido;
                        contadorListadiscosModificables++;
                    }
                }
                break;

        }

        // se reinicia
        contadorListadiscosModificables = 0;

        //quitamos funcionalidad o la ponemos en los discos del palo actual
        foreach (GameObject currentDisco in listaDiscosModificablesPalo)
        {
            //si el currentDisco no es el discoMasAlto le quitamos su raycastTarget
            //puede ser null si solo hay 3 discos pues el disco 4
            if(currentDisco!= null)
            {
                if (currentDisco.name != discoMasAltoName)
                {
                    currentDisco.GetComponent<Image>().raycastTarget = false;
                }
                else
                {
                    currentDisco.GetComponent<Image>().raycastTarget = true;
                }
            }
            
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    //en todo momento sabemos cual es el disco que está mas arriba en cada palo
    void Update()
    {
        //llamamos a metodo de GameManager que devuelva el disco que está mas arriba una vez se ha colocado el ultimo
        string discoMasAltoPalo1Name = MetodoDevuelveDiscoMasArribaPalo(Palos[0].gameObject);
        //si hay disco en el palo
        if(discoMasAltoPalo1Name != "")
        {
            QuitarPermisoDiscosExceptoArriba(discoMasAltoPalo1Name, Palos[0].gameObject);
        }

        string discoMasAltoPalo2Name = MetodoDevuelveDiscoMasArribaPalo(Palos[1].gameObject);
        //si hay disco en el palo
        if (discoMasAltoPalo2Name != "")
        {
            QuitarPermisoDiscosExceptoArriba(discoMasAltoPalo2Name, Palos[1].gameObject);
        }

        string discoMasAltoPalo3Name = MetodoDevuelveDiscoMasArribaPalo(Palos[2].gameObject);
        //si hay disco en el palo
        if (discoMasAltoPalo3Name != "")
        {
            QuitarPermisoDiscosExceptoArriba(discoMasAltoPalo3Name, Palos[2].gameObject);
        }
        //en todo momento actualizar y poner que solo tenga raycastTarget activado para poder cogerse el disco más alto



    }
}
