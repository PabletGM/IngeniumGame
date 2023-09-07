using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class GameManagerHanoi : MonoBehaviour
{

    #region parametersDataBase

    [HideInInspector]
    public int tiempoTotalHanoiRegistrado=0;

    [HideInInspector]
    public int numJugadasTotal=0;

    [HideInInspector]
    public int numMovimientosIncorrectos=0;

    [HideInInspector]
    public int numMovimientosOutOfLimits=0;

    #endregion



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


    //palos reales sprites y su animacion
    [SerializeField]
    private Animator[] animacionPalosOpacidad;

    //booleano que dice si ultimo disco colocado en un palo o no
    #pragma warning disable CS0414
    private bool ultimoDiscoColocado = true;
    #pragma warning restore CS0414

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


    #region DataBaseDatos

        #region Get
                        public int GetTiempoTotalHanoiRegistrado()
                        {
                            return tiempoTotalHanoiRegistrado;
                        }

   
                        public int GetnumJugadasTotalHanoiRegistrado()
                        {

                            return numJugadasTotal;
                        }

                        public int GetnumMovsIncorrectosHanoiRegistrado()
                        {

                            return numMovimientosIncorrectos;
                        }

                        public int GetnumMovsOutOfLimitsHanoiRegistrado()
                        {

                            return numMovimientosOutOfLimits;
                        }
    #endregion

        #region Set

                public void SetTiempoTotalHanoiRegistrado(int tiempoPartida)
                {
                    tiempoTotalHanoiRegistrado = tiempoPartida;
                }


                public void SetnumJugadasTotalHanoiRegistrado(int numJugadas)
                {

                    numJugadasTotal = numJugadas;
                }

                public void SetnumMovsIncorrectosHanoiRegistrado(int numMovsIncorrect)
                {

                    numMovimientosIncorrectos = numMovsIncorrect;
                }

                public void SetnumMovsOutOfLimitsHanoiRegistrado(int numMovsOutOfLimits)
                {

                    numMovimientosOutOfLimits = numMovsOutOfLimits;
                }

    #endregion


        public void AumentarNumJugadasTotalHanoiRegistrado()
        {

            numJugadasTotal++;
        }
        public void AumentarNumMovimientosIncorrectosHanoiRegistrado()
        {

            numMovimientosIncorrectos++;
        }
        public void AumentarNumMovimientosOutOfLimitsHanoiRegistrado()
        {

            numMovimientosOutOfLimits++;
        }

    #endregion






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


    //activa/desactiva todas las animaciones Palo Opacidad
    public void SetAnimacionesOpacidad(bool set)
    {
        foreach (Animator animacionPaloOpacidad in animacionPalosOpacidad)
        {
            animacionPaloOpacidad.SetBool("DiscoDrag", set);
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

    public void PonerDraggableDiscosMasAltos()
    {
        //quita todos los permisos de discos de drgaggable
        QuitarDraggable();


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
            //a la hora de comparar pasamos a float ambos para un decimal
            double positionX = Math.Round(position.x, 1) ;
            double positionY = Math.Round(position.y, 1);
            double positionZ = Math.Round(position.z, 1);

            double currentHuecoPalo1X = Math.Round(currentHuecoPalo1.transform.position.x, 1);
            double currentHuecoPalo1Y = Math.Round(currentHuecoPalo1.transform.position.y, 1);
            double currentHuecoPalo1Z = Math.Round(currentHuecoPalo1.transform.position.z, 1);

            Vector3 positionFloat = new Vector3((float)positionX, (float)positionY, (float)positionZ);
            Vector3 currentHuecoPalo1Float = new Vector3((float)currentHuecoPalo1X, (float)currentHuecoPalo1Y,(float)currentHuecoPalo1Z);

            //en cada hijo comparamos las posiciones, si son iguales true
            if (positionFloat == currentHuecoPalo1Float)
            {
                return currentHuecoPalo1;
            }
        }

        //recorrer hijos del palo 2, huecos
        foreach (GameObject currentHuecoPalo2 in palo2Places)
        {
            //a la hora de comparar pasamos a float ambos para un decimal
            double positionX2 = Math.Round(position.x, 1);
            double positionY2 = Math.Round(position.y, 1);
            double positionZ2 = Math.Round(position.z, 1);

            double currentHuecoPalo2X = Math.Round(currentHuecoPalo2.transform.position.x, 1);
            double currentHuecoPalo2Y = Math.Round(currentHuecoPalo2.transform.position.y, 1);
            double currentHuecoPalo2Z = Math.Round(currentHuecoPalo2.transform.position.z, 1);

            Vector3 positionFloat2 = new Vector3((float)positionX2, (float)positionY2, (float)positionZ2);
            Vector3 currentHuecoPalo2Float = new Vector3((float)currentHuecoPalo2X, (float)currentHuecoPalo2Y, (float)currentHuecoPalo2Z);

            //en cada hijo comparamos las posiciones, si son iguales true
            if (positionFloat2 == currentHuecoPalo2Float)
            {
                return currentHuecoPalo2;
            }
        }

        //recorrer hijos del palo 3, huecos
        foreach (GameObject currentHuecoPalo3 in palo3Places)
        {
            //a la hora de comparar pasamos a float ambos para un decimal
            double positionX3 = Math.Round(position.x, 1);
            double positionY3 = Math.Round(position.y, 1);
            double positionZ3 = Math.Round(position.z, 1);

            double currentHuecoPalo3X = Math.Round(currentHuecoPalo3.transform.position.x, 1);
            double currentHuecoPalo3Y = Math.Round(currentHuecoPalo3.transform.position.y, 1);
            double currentHuecoPalo3Z = Math.Round(currentHuecoPalo3.transform.position.z, 1);

            Vector3 positionFloat2 = new Vector3((float)positionX3, (float)positionY3, (float)positionZ3);
            Vector3 currentHuecoPalo2Float = new Vector3((float)currentHuecoPalo3X, (float)currentHuecoPalo3Y, (float)currentHuecoPalo3Z);

            //en cada hijo comparamos las posiciones, si son iguales true
            if (positionFloat2 == currentHuecoPalo2Float)
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


    //metodo al que tu le pasas un numero y el palo y te devuelve el hueco
    public GameObject DevolverHuecoConNumeroYPalo(GameObject palo, int numero)
    {
        //buscamos palo
        switch (palo.name)
        {
            case "palo1":
                //dentro del palo buscamos hueco con el numero
                switch (numero)
                {
                    //buscamos hueco segun numero y lo devolvemos
                    case 1:
                        return palo1Places[0].gameObject;
                    case 2:
                        return palo1Places[1].gameObject;
                    case 3:
                        return palo1Places[2].gameObject;
                    case 4:
                        return palo1Places[3].gameObject;
                    //excepciones
                    default:
                        break;

                }
                break;

            case "palo2":
                //dentro del palo buscamos hueco con el numero
                switch (numero)
                {
                    //buscamos hueco segun numero y lo devolvemos
                    case 1:
                        return palo2Places[0].gameObject;
                    case 2:
                        return palo2Places[1].gameObject;
                    case 3:
                        return palo2Places[2].gameObject;
                    case 4:
                        return palo2Places[3].gameObject;
                    //excepciones
                    default:
                        break;

                }
                break;
            case "palo3":
                //dentro del palo buscamos hueco con el numero
                switch (numero)
                {
                    //buscamos hueco segun numero y lo devolvemos
                    case 1:
                        return palo3Places[0].gameObject;
                    case 2:
                        return palo3Places[1].gameObject;
                    case 3:
                        return palo3Places[2].gameObject;
                    case 4:
                        return palo3Places[3].gameObject;
                    //excepciones
                    default:
                        break;

                }
                break;
          
            //excepciones
            default:
                break;

        }
        return null;


    }

    public GameObject PosDiscoFalsaEncontrarla(GameObject paloDondeBuscar)
    {
        switch (paloDondeBuscar.name)
        {
            //buscar hueco en primera lista
            case "palo1":
                //recorrer hijos del palo
                foreach (GameObject currentHuecoPalo1 in palo1Places)
                {
                    //buscamos una posicion que huecoLibre = false
                    bool libre = currentHuecoPalo1.GetComponent<Libre>().GetHuecoLibre();
                    //buscamos una posicion que nombre =! ""
                    string name = currentHuecoPalo1.GetComponent<Libre>().GetNombreDiscoActual();
                    //si cumple ambas
                    if (!libre && name !="")
                    {
                        //comprobamos si algun disco está en su posicion para ver si es falsa posicion o no
                        //si tiene misma posicion con algun disco es que coincide y es disco real
                        //si ningun disco coincide en pos es que es un hueco falso
                        int num = 0;
                        foreach (GameObject currentDisco in listaDiscos)
                        {

                            //a la hora de comparar pasamos a float ambos para un decimal
                            double currentHuecopositionX1 = Math.Round(currentHuecoPalo1.transform.position.x, 1);
                            double currentHuecopositionY1 = Math.Round(currentHuecoPalo1.transform.position.y, 1);
                            double currentHuecopositionZ1 = Math.Round(currentHuecoPalo1.transform.position.z, 1);

                            double currentDiscoPalo1X = Math.Round(currentDisco.transform.position.x, 1);
                            double currentDiscoPalo1Y = Math.Round(currentDisco.transform.position.y, 1);
                            double currentDiscoPalo1Z = Math.Round(currentDisco.transform.position.z, 1);

                            Vector3 currentHuecoPaloPositionFloat1 = new Vector3((float)currentHuecopositionX1, (float)currentHuecopositionY1, (float)currentHuecopositionZ1);
                            Vector3 currentDiscoPositionFloat1 = new Vector3((float)currentDiscoPalo1X, (float)currentDiscoPalo1Y, (float)currentDiscoPalo1Z);
                            //sino coincide sumamos un intento
                            if (currentDiscoPositionFloat1 != currentHuecoPaloPositionFloat1)
                            {
                                num++;
                                //cuando ya se hayan recorrido todos los discos sino coincide ninguno num = 4
                                if(num>=4)
                                {
                                    //disco falso encontrado
                                    return currentHuecoPalo1;
                                }
                            }
                        }
                    }
                }
                break;

            //buscar hueco en segunda lista
            case "palo2":
                foreach (GameObject currentHuecoPalo2 in palo2Places)
                {

                    //buscamos una posicion que huecoLibre = false
                    bool libre = currentHuecoPalo2.GetComponent<Libre>().GetHuecoLibre();
                    //buscamos una posicion que nombre =! ""
                    string name = currentHuecoPalo2.GetComponent<Libre>().GetNombreDiscoActual();
                    //si cumple ambas
                    if (!libre && name != "")
                    {
                        //comprobamos si algun disco está en su posicion para ver si es falsa posicion o no
                        //si tiene misma posicion con algun disco es que coincide y es disco real
                        //si ningun disco coincide en pos es que es un hueco falso
                        int num = 0;
                        foreach (GameObject currentDisco in listaDiscos)
                        {
                            //a la hora de comparar pasamos a float ambos para un decimal
                            double currentHuecopositionX2 = Math.Round(currentHuecoPalo2.transform.position.x, 1);
                            double currentHuecopositionY2 = Math.Round(currentHuecoPalo2.transform.position.y, 1);
                            double currentHuecopositionZ2 = Math.Round(currentHuecoPalo2.transform.position.z, 1);

                            double currentDiscoPalo2X = Math.Round(currentDisco.transform.position.x, 1);
                            double currentDiscoPalo2Y = Math.Round(currentDisco.transform.position.y, 1);
                            double currentDiscoPalo2Z = Math.Round(currentDisco.transform.position.z, 1);

                            Vector3 currentHuecoPaloPositionFloat2 = new Vector3((float)currentHuecopositionX2, (float)currentHuecopositionY2, (float)currentHuecopositionZ2);
                            Vector3 currentDiscoPositionFloat2 = new Vector3((float)currentDiscoPalo2X, (float)currentDiscoPalo2Y, (float)currentDiscoPalo2Z);
                            //sino coincide sumamos un intento
                            if (currentDiscoPositionFloat2 != currentHuecoPaloPositionFloat2)
                            {
                                num++;
                                //cuando ya se hayan recorrido todos los discos sino coincide ninguno num = 4
                                if (num >= 4)
                                {
                                    //disco falso encontrado
                                    return currentHuecoPalo2;
                                }
                            }
                        }
                    }

                }
                break;

            //buscar hueco en tercera lista
            case "palo3":
                foreach (GameObject currentHuecoPalo3 in palo3Places)
                {
                    //buscamos una posicion que huecoLibre = false
                    bool libre = currentHuecoPalo3.GetComponent<Libre>().GetHuecoLibre();
                    //buscamos una posicion que nombre =! ""
                    string name = currentHuecoPalo3.GetComponent<Libre>().GetNombreDiscoActual();
                    //si cumple ambas
                    if (!libre && name != "")
                    {
                        //comprobamos si algun disco está en su posicion para ver si es falsa posicion o no
                        //si tiene misma posicion con algun disco es que coincide y es disco real
                        //si ningun disco coincide en pos es que es un hueco falso
                        int num = 0;
                        foreach (GameObject currentDisco in listaDiscos)
                        {
                            //a la hora de comparar pasamos a float ambos para un decimal
                            double currentHuecopositionX3 = Math.Round(currentHuecoPalo3.transform.position.x, 1);
                            double currentHuecopositionY3 = Math.Round(currentHuecoPalo3.transform.position.y, 1);
                            double currentHuecopositionZ3 = Math.Round(currentHuecoPalo3.transform.position.z, 1);

                            double currentDiscoPalo3X = Math.Round(currentDisco.transform.position.x, 1);
                            double currentDiscoPalo3Y = Math.Round(currentDisco.transform.position.y, 1);
                            double currentDiscoPalo3Z = Math.Round(currentDisco.transform.position.z, 1);

                            Vector3 currentHuecoPaloPositionFloat3 = new Vector3((float)currentHuecopositionX3, (float)currentHuecopositionY3, (float)currentHuecopositionZ3);
                            Vector3 currentDiscoPositionFloat3 = new Vector3((float)currentDiscoPalo3X, (float)currentDiscoPalo3Y, (float)currentDiscoPalo3Z);
                            //sino coincide sumamos un intento
                            if (currentDiscoPositionFloat3 != currentHuecoPaloPositionFloat3)
                            {
                                num++;
                                //cuando ya se hayan recorrido todos los discos sino coincide ninguno num = 4
                                if (num >= 4)
                                {
                                    //disco falso encontrado
                                    return currentHuecoPalo3;
                                }
                            }
                        }
                    }
                }
                break;

            //excepciones
            default:
                Debug.Log("No hay hueco");
                break;

        }
        return null;
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

            //a la hora de comparar pasamos a float ambos para un decimal
            double currentHuecopositionX3 = Math.Round(posicionHueco.transform.position.x, 1);
            double currentHuecopositionY3 = Math.Round(posicionHueco.transform.position.y, 1);
            double currentHuecopositionZ3 = Math.Round(posicionHueco.transform.position.z, 1);

            double currentDiscoPalo3X = Math.Round(disco.transform.position.x, 1);
            double currentDiscoPalo3Y = Math.Round(disco.transform.position.y, 1);
            double currentDiscoPalo3Z = Math.Round(disco.transform.position.z, 1);

            Vector3 currentHuecoPaloPositionFloat = new Vector3((float)currentHuecopositionX3, (float)currentHuecopositionY3, (float)currentHuecopositionZ3);
            Vector3 currentDiscoPositionFloat = new Vector3((float)currentDiscoPalo3X, (float)currentDiscoPalo3Y, (float)currentDiscoPalo3Z);
            if (currentDiscoPositionFloat == currentHuecoPaloPositionFloat)
            {
                return true;
            }

        }
        return false;
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

    }

    //en todo momento sabemos cual es el disco que está mas arriba en cada palo
    void Update()
    {

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
