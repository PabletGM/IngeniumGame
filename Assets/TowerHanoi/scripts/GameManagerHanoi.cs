using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GameManagerHanoi : MonoBehaviour
{
    //lista de objetos Palo metalico que soporta a los discos
    [HideInInspector]
    public ItemSlot[] Palos;

    //singleton
    static private GameManagerHanoi _instanceHanoi;

    UIManagerHanoi _myUIManagerHanoi;

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

    [HideInInspector]
    public string combinacionGanadora = "";

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

    public bool DiscoEnAlgunHueco(GameObject disco)
    {
        
            bool verSidiscoPalo = VerSiDiscoEstaEnPalo(disco.transform);
            return verSidiscoPalo;
    }

    //pasamos una posicion y nos tiene que devolver el hueco que la posee
    public GameObject BuscandoPaloConPosicion(Vector3 position)
    {
        //recorrer hijos del palo 1, huecos
        foreach (GameObject currentHuecoPalo1 in palo1Places)
        {
            //en cada hijo comparamos las posiciones, si son iguales true
            if (currentHuecoPalo1.transform.position == position)
            {
                return Palos[0].gameObject;
            }
        }

        //recorrer hijos del palo 2, huecos
        foreach (GameObject currentHuecoPalo2 in palo2Places)
        {

            //en cada hijo comparamos las posiciones, si son iguales true
            if (currentHuecoPalo2.transform.position == position)
            {
                return Palos[1].gameObject;
            }
        }

        //recorrer hijos del palo 3, huecos
        foreach (GameObject currentHuecoPalo3 in palo3Places)
        {
            //en cada hijo comparamos las posiciones, si son iguales true
            if (currentHuecoPalo3.transform.position == position)
            {
                return Palos[2].gameObject;
            }
        }

        return null;
    }

    public GameObject BuscandoHuecoConPosicion(Vector3 position)
    {
        //recorrer hijos del palo 1, huecos
        foreach (GameObject currentHuecoPalo1 in palo1Places)
        {
            //en cada hijo comparamos las posiciones, si son iguales true
            if (currentHuecoPalo1.transform.position == position)
            {
                return currentHuecoPalo1;
            }
        }

        //recorrer hijos del palo 2, huecos
        foreach (GameObject currentHuecoPalo2 in palo2Places)
        {

            //en cada hijo comparamos las posiciones, si son iguales true
            if (currentHuecoPalo2.transform.position == position)
            {
                return currentHuecoPalo2;
            }
        }

        //recorrer hijos del palo 3, huecos
        foreach (GameObject currentHuecoPalo3 in palo3Places)
        {
            //en cada hijo comparamos las posiciones, si son iguales true
            if (currentHuecoPalo3.transform.position == position)
            {
                return currentHuecoPalo3;
            }
        }

        return null;
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


    //metodo que comprueba si en la pos del hueco hay algun disco
    public bool ComprobarSihayDiscoEnPos(Transform posicionHueco)
    {
        foreach (GameObject disco in listaDiscos)
        {
            if(disco.transform.position == posicionHueco.transform.position)
            {
                return true;
            }

        }
        return false;
    }

    //por cada hueco que ponga que tiene disco y no lo tiene lo cambio en ese palo
    public void CambiarFalsoHuecoEnPalo(GameObject palo)
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
                    //buscamos huecoLibre con caracteristicas especiales = (huecoLibre = false y nombreDiscoActual != "")
                    //esto es un hueco ocupado para ver si realmente está ocupado
                    bool libre = currentHuecoPalo1.GetComponent<Libre>().GetHuecoLibre();
                    string name = currentHuecoPalo1.GetComponent<Libre>().GetNombreDiscoActual();

                    if (!libre && name!="")
                    {
                        //comprobamos si en la posicion del hueco hay algun disco
                        bool discoEnPos = ComprobarSihayDiscoEnPos(currentHuecoPalo1.transform);
                        //si hay disco en esa posicion todo correcto, sino lo hay, hay que cambiarlo
                        if(!discoEnPos)
                        {
                            currentHuecoPalo1.GetComponent<Libre>().SetHuecoLibre(true);
                            currentHuecoPalo1.GetComponent<Libre>().SetNombreDiscoActual("");
                        }
                    }
                    //sino seguimos buscando

                }
                break;
            //buscar hueco en segunda lista
            case "palo2":
                foreach (GameObject currentHuecoPalo2 in palo2Places)
                {

                    //buscamos huecoLibre con caracteristicas especiales = (huecoLibre = false y nombreDiscoActual != "")
                    //esto es un hueco ocupado para ver si realmente está ocupado
                    bool libre = currentHuecoPalo2.GetComponent<Libre>().GetHuecoLibre();
                    string name = currentHuecoPalo2.GetComponent<Libre>().GetNombreDiscoActual();

                    if (!libre && name != "")
                    {
                        //comprobamos si en la posicion del hueco hay algun disco
                        bool discoEnPos = ComprobarSihayDiscoEnPos(currentHuecoPalo2.transform);
                        //si hay disco en esa posicion todo correcto, sino lo hay, hay que cambiarlo
                        if (!discoEnPos)
                        {
                            currentHuecoPalo2.GetComponent<Libre>().SetHuecoLibre(true);
                            currentHuecoPalo2.GetComponent<Libre>().SetNombreDiscoActual("");
                        }
                    }
                    //sino seguimos buscando
                }
                break;
            //buscar hueco en tercera lista
            case "palo3":
                foreach (GameObject currentHuecoPalo3 in palo3Places)
                {
                    //buscamos huecoLibre con caracteristicas especiales = (huecoLibre = false y nombreDiscoActual != "")
                    //esto es un hueco ocupado para ver si realmente está ocupado
                    bool libre = currentHuecoPalo3.GetComponent<Libre>().GetHuecoLibre();
                    string name = currentHuecoPalo3.GetComponent<Libre>().GetNombreDiscoActual();

                    if (!libre && name != "")
                    {
                        //comprobamos si en la posicion del hueco hay algun disco
                        bool discoEnPos = ComprobarSihayDiscoEnPos(currentHuecoPalo3.transform);
                        //si hay disco en esa posicion todo correcto, sino lo hay, hay que cambiarlo
                        if (!discoEnPos)
                        {
                            currentHuecoPalo3.GetComponent<Libre>().SetHuecoLibre(true);
                            currentHuecoPalo3.GetComponent<Libre>().SetNombreDiscoActual("");
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

      

    }

    public GameObject BuscarHuecoEnAlgunPalo(GameObject discoNuevo)
    {
        GameObject huecoPalo1 = BuscarHuecoEnPalo(Palos[0].gameObject);
        GameObject huecoPalo2 = BuscarHuecoEnPalo(Palos[1].gameObject);
        GameObject huecoPalo3 = BuscarHuecoEnPalo(Palos[2].gameObject);

        //miramos y cogemos el primero que no sea nulo y sea posible por width reglas
        if(huecoPalo1!=null && ComparacionWidthDiscos(discoNuevo, Palos[0].gameObject))
        {
            return huecoPalo1;
        }
        //miramos y cogemos el primero que no sea nulo y sea posible por width reglas
        else if (huecoPalo2 != null && ComparacionWidthDiscos(discoNuevo, Palos[1].gameObject))
        {
            return huecoPalo2;
        }
        //miramos y cogemos el primero que no sea nulo y sea posible por width reglas
        else if (huecoPalo3 != null && ComparacionWidthDiscos(discoNuevo, Palos[2].gameObject))
        {
            return huecoPalo3;
        }
        else
        {
            return null;
        }
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
        _myUIManagerHanoi = UIManagerHanoi.GetInstanceUI();

    }

    //en todo momento sabemos cual es el disco que está mas arriba en cada palo
    void Update()
    {
        


        //llamamos a metodo de GameManager que devuelva el disco que está mas arriba una vez se ha colocado el ultimo
        string discoMasAltoPalo1Name = MetodoDevuelveDiscoMasArribaPalo(Palos[0].gameObject);
        //con el nombre necesitamos el GameObject
        GameObject discoMasAlto = BuscarDiscoSegunNombre(discoMasAltoPalo1Name);
        //si hay disco en el palo
        if (discoMasAltoPalo1Name != "")
        {
            PonerDraggableDiscoElegido(discoMasAlto);
            QuitarPermisoDiscosExceptoArriba(discoMasAltoPalo1Name, Palos[0].gameObject);
        }

        string discoMasAltoPalo2Name = MetodoDevuelveDiscoMasArribaPalo(Palos[1].gameObject);
        //con el nombre necesitamos el GameObject
        GameObject discoMasAlto2 = BuscarDiscoSegunNombre(discoMasAltoPalo2Name);
        //si hay disco en el palo
        if (discoMasAltoPalo2Name != "")
        {
            PonerDraggableDiscoElegido(discoMasAlto2);
            QuitarPermisoDiscosExceptoArriba(discoMasAltoPalo2Name, Palos[1].gameObject);
        }

        string discoMasAltoPalo3Name = MetodoDevuelveDiscoMasArribaPalo(Palos[2].gameObject);
        //con el nombre necesitamos el GameObject
        GameObject discoMasAlto3 = BuscarDiscoSegunNombre(discoMasAltoPalo3Name);
        //si hay disco en el palo
        if (discoMasAltoPalo3Name != "")
        {
            PonerDraggableDiscoElegido(discoMasAlto3);
            QuitarPermisoDiscosExceptoArriba(discoMasAltoPalo3Name, Palos[2].gameObject);
        }
        //en todo momento actualizar y poner que solo tenga raycastTarget activado para poder cogerse el disco más alto

        
        //si se quedan 2 discos en 1 posicion lo resuelve y sube uno a la siguiente pos
        //MismaPosicionDiscoMismoPalo();
    }

    public GameObject BuscarDiscoSegunNombre(string discoName)
    {
        switch(discoName)
        {
            case "disco1Imagen":
                return listaDiscos[0];

            case "disco2Imagen":
                return listaDiscos[1];


            case "disco3Imagen":
                return listaDiscos[2];

            case "disco4Imagen":
                return listaDiscos[3];
                
        }
        return null;
    }


    //quitamos todos los Draggable 
    public void QuitarDraggable()
    {
        foreach(GameObject  currentDisco in listaDiscos)
        {  
                currentDisco.GetComponent<Draggable>().SetDraggable(false); 
        }
    }

    public void PonerDraggableDiscoElegido(GameObject discoAlto)
    {
        foreach (GameObject currentDisco in listaDiscos)
        {
            if (currentDisco == discoAlto)
            {
                currentDisco.GetComponent<Draggable>().SetDraggable(true);
            }

        }
    }


    //con los 3 palos
    public void CorreccionHuecosFalsoRelleno()
    {
        CambiarFalsoHuecoEnPalo(Palos[0].gameObject);
        CambiarFalsoHuecoEnPalo(Palos[1].gameObject);
        CambiarFalsoHuecoEnPalo(Palos[2].gameObject);
    }

    


    public void Incorrect()
    {
        AudioManagerHanoi.Instance.PlaySFX("error");
        _myUIManagerHanoi.Incorrect();
    }

    public void FueraLimites()
    {
        AudioManagerHanoi.Instance.PlaySFX("error");
        _myUIManagerHanoi.FueraLimites();
    }

    private void Victoria()
    {
        //ganaste
        Debug.Log("ganaste!!");
        //bloqueamos palos para no poder coger mas discos
        HabilitarPalos();
        AudioManagerHanoi.Instance.PlaySFX("firework");
        //efectos especiales fuegos artificiales ACTIVARLOS UIMANAGER
        _myUIManagerHanoi.SetFireworksWin(true);
    }

    //creamos metodo que compruebe si en los huecos de un palo hay 2 discos con misma posicion, si son así, cogemos ambos discos
    //vemos si se puede poner uno encima del otro y se hace, poniendolo en siguiente posicion del palo, sino pos anterior del disco
    public void MismaPosicionDiscoMismoPalo()
    {
        //metodo que 
        //vea si 2 discos poseen la misma posición
        foreach(GameObject objetoDisco in listaDiscos )
        {
            //comparamos posicion disco con la del resto
            for(int i = 0; i < listaDiscos.Length; i++)
            {
                //si son diferentes objetos y misma posicion
                if( objetoDisco.name != listaDiscos[i].name && objetoDisco.transform.position == listaDiscos[i].transform.position)
                {
                    //metodo que comprueba si los discos que estan libres tienen disco o no para cambiar su variable discoActual
                    CorreccionHuecosFalsoRelleno();


                    //comprobar si se puede poner 1 encima de otro de alguna manera
                    bool combinacion1 = PosiblePonerDiscoEncimaWidth(objetoDisco.GetComponent<RectTransform>().sizeDelta.x, listaDiscos[i].GetComponent<RectTransform>().sizeDelta.x);

                    bool combinacion2 = PosiblePonerDiscoEncimaWidth( listaDiscos[i].GetComponent<RectTransform>().sizeDelta.x, objetoDisco.GetComponent<RectTransform>().sizeDelta.x);

                    //si alguna combinacion es true, esto es el discoNuevo es mas pequeño que discoAntiguo, lo hacemos
                    //buscamos el palo donde estan estos discos y llamamos a metodo de ItemSlot para colocar el pequeño encima
                    GameObject huecoQuePosee2Discos = BuscandoHuecoConPosicion(objetoDisco.transform.position);
                    //ver objeto padre
                    GameObject palo = huecoQuePosee2Discos.GetComponentInParent<Transform>().parent.gameObject;

                    //si la combinacion 1 es true es que el disco pequeño es objetoDisco
                    if (combinacion1)
                    {
                        //buscar siguiente hueco libre 
                        GameObject huecoLibre =BuscarHuecoEnPalo(palo);
                        //objetoDisco.GetComponent<DragDrop>().OnBeginDragManual();
                        //objetoDisco.GetComponent<DragDrop>().OnDragManual(palo,huecoLibre);
                        palo.GetComponent<ItemSlot>().SetDiscoEnPosicionManual(objetoDisco, huecoLibre, huecoQuePosee2Discos, listaDiscos[i]);
                        //objetoDisco.GetComponent<DragDrop>().OnEndDragManual();
                    }
                    //si la combinacion 2 es true es que el disco pequeño es listaDiscos[i]
                    else if (combinacion2)
                    {
                        //buscar siguiente hueco libre 
                        GameObject huecoLibre = BuscarHuecoEnPalo(palo);
                        //listaDiscos[i].GetComponent<DragDrop>().OnBeginDragManual();
                        //listaDiscos[i].GetComponent<DragDrop>().OnDragManual(palo,huecoLibre);
                        palo.GetComponent<ItemSlot>().SetDiscoEnPosicionManual(listaDiscos[i], huecoLibre, huecoQuePosee2Discos, objetoDisco);
                        //listaDiscos[i].GetComponent<DragDrop>().OnEndDragManual();
                    }

                }
            }
        }
        
        //colocarlo encima
    }

    //comprobacion si hay combinacion ganadora palo3
    //llamamos a metodo en el cual cada vez que se pone un disco en palo3 segun la posición añadimos una letra, esto es
    //disco1Azul = W , disco2Verde = I, disco3Amarillo = N, disco4Rojo = !, y sin disco "", asi se deben juntar para formar
    // WIN!, asi se revisan las letras cada vez que se añade disco a palo3
    //comprobamos en lista Palo3Places el nombre del disco actual, segun este vamos cambiando la combinacion ganadora
    public void ActualizarCombinacionGanadora()
    {
        
        
        string sumaTotalletrasCombinacion = "";
        //quitamos funcionalidad o la ponemos en los discos del palo actual
        foreach (GameObject huecoPalo3 in palo3Places)
        {
            //accedemos a componente Libre de la Pos de la lista y a su discoActual
            string nombreDiscoActualHueco =huecoPalo3.GetComponent<Libre>().GetNombreDiscoActual();
            //hacemos metodo que según el nombre devuelva una letra de la combinacion ganadora
            string letraCombinacion = SegunNombreLetra(nombreDiscoActualHueco);
            sumaTotalletrasCombinacion += letraCombinacion;
        }
        combinacionGanadora = sumaTotalletrasCombinacion;

        //comprobamos si hemos ganado
        if (combinacionGanadora == "WIN!")
        {
            Victoria();
        }
    }

    private string SegunNombreLetra(string nombreDiscoActualHueco)
    {
        string letraCombinacion = "";
        switch (nombreDiscoActualHueco)
        {
            //5 casos, 4 de discos y si no hay disco
            case "disco1Imagen":
                letraCombinacion = "W";
                break;
            case "disco2Imagen":
                letraCombinacion = "I";
                break;
            case "disco3Imagen":
                letraCombinacion = "N";
                break;
            case "disco4Imagen":
                letraCombinacion = "!";
                break;
            case "":
                letraCombinacion = "";
                break;
        }
        return letraCombinacion;
    }

    public string DevolverCombinacionGanadora()
    {
        return combinacionGanadora;
    }
}
