using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamecontroller : MonoBehaviour
{


    static private gamecontroller _instanceGameController;

    public TMP_Text[] buttonList;

    private string playerSide = "X";
    private string enemySide = "0";
    //el turno que toque
    private string gameSide;

    public GameObject gameover;
    public TMP_Text gameoverText;

    public GameObject win;

    private int moveCount;

    //maximo numero de rondas en caso de empate es siempre 4
    private int maxRounds = 4;

    public GameObject restartButton;

    public GameObject playerX;

    public GameObject player0;

    [SerializeField]
    private GameObject panelEnemyWaiting;

    [SerializeField]
    private GameObject board;

    [SerializeField]
    private GameObject lineas;

    [SerializeField]
    private GameObject botones;

    [SerializeField]
    private GameObject nextLevel;


    //booleano que permite que durante el modo de dificultad medio se haga la comprobacion 1 vez para taponar jugada player de victoria
    private bool modoMedioTaponarPrimeraJugadaVictoria = true;

    //creamos array tamaño 8 para poner ahí posiciones donde el enemigo si mueve ficha puede ganar
    //se actualiza en cada turno
    int[] posicionesVictoriaEnemy;
    int[] posicionesVictoriaPlayer;

    //para saber que casillas se van ocupando con alguna ficha
    bool[] posicionesLibresSinFicha;

    int contadorPosicionesVictoriaEnemy = 0;
    int contadorPosicionesVictoriaPlayer = 0;

    private bool endPartida = false;

    private bool evitarDobleJugadaPlayer = false;



    static public gamecontroller GetInstanceGameController()
    {
        return _instanceGameController;
    }




    public void EnemyTurnInicial()
    {
        //turno enemy
        ChangeSides("0");
        //turno del enemigo, sabiendo lugares que poner ficha para ganar
        ElegirFichaEnemigoAutomatico();
        //eliminamos para en siguiente ronda volver a poner posicionesVictoriaEnemy
        LimpiarPosicionesVictoriaRondaAnterior();
        FichaEnemySound();
    }
    public void EnemyTurn()
    {
        //sino ha ganado el jugador se hace, esto es si endPartida = false && gameOver.text != "X has ganado"
        //si ha ganado el jugador el enemigo no pone jugada ya
        if (!endPartida && gameoverText.text != "X has ganado")
        {
            ChangeSides("0");
            //turno del enemigo, sabiendo lugares que poner ficha para ganar
            ElegirFichaEnemigoAutomatico();
            //eliminamos para en siguiente ronda volver a poner posicionesVictoriaEnemy
            LimpiarPosicionesVictoriaRondaAnterior();

            //ahora toca jugador al acabar enemigo
            EmpezarTurnoJugador();
            FichaEnemySound();
        }
        

    }

    //activa panel enemy waiting durante la duracion del turno del enemigo
    public void SetPanelEnemyWaiting()
    {
        panelEnemyWaiting.SetActive(true);
        //desactivar em un segundo al acabar el turno del enemigo
        Invoke("DesactivarPanelWaiting", 1.4f);
    }
    void DesactivarPanelWaiting()
    {
        panelEnemyWaiting.SetActive(false);
    }

    //cuando termina de mover el enemigo
    void EmpezarTurnoJugador()
    {
        ChangeSides("X");
    }

    //cuando termina de mover el JUGADOR
    public void EmpezarTurnoEnemigo()
    {
        ChangeSides("0");
    }

    private void Awake()
    {

        //si la instancia no existe se hace este script la instancia
        if (_instanceGameController == null)
        {
            _instanceGameController = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }

        //SetGameControllerReferenceOnButtons();

        InicializarJuego();

        //turno inicial del enemigo
        EnemyTurnInicial();
    }

    public void FichaEnemySound()
    {
        AudioManager3EnRaya.instance.PlaySFXDuracion("FichaEnemy", 1f);
    }

    public void ReturnTotalRounds()
    {
        Debug.Log(moveCount);
    }


    public void InicializarJuego()
    {
        gameover.SetActive(false);
        win.SetActive(false);
        gameSide = playerSide;
        moveCount = 0;
        //tamaño 8
        posicionesVictoriaEnemy = new int[8];
        posicionesVictoriaPlayer = new int[8];
        //tablero
        posicionesLibresSinFicha = new bool[9];
        //poner todas las posiciones a true por defecto
    }

    public void TodasPosicionesLibres()
    {
        for(int i=0; i<posicionesLibresSinFicha.Length;i++)
        {
            posicionesLibresSinFicha[i] = true;
        }
    }


    //pasas argumento de boton que se ha pulsado y lo marca en el array
    public void PosicionBotonPulsadoOcupada(GameObject botonpulsado)
    {
        //hacemos el botonPulsado no interactuable
        botonpulsado.GetComponent<Button>().interactable = false;
        //vemos que numero es el botonPulsado
        int numero = devolverNumeroConBoton(botonpulsado);
        if(numero!=-1)
        {
            switch (numero)
            {
                case 0 :
                    posicionesLibresSinFicha[0] = true;
                    break;
                case 1 :
                    posicionesLibresSinFicha[1] = true;
                    break;
                case 2:
                    posicionesLibresSinFicha[2] = true;
                    break;
                case 3:
                    posicionesLibresSinFicha[3] = true;
                    break;
                case 4:
                    posicionesLibresSinFicha[4] = true;
                    break; 
                case 5:
                    posicionesLibresSinFicha[5] = true;
                    break;
                case 6:
                    posicionesLibresSinFicha[6] = true;
                    break;
                case 7:
                    posicionesLibresSinFicha[7] = true;
                    break;
                case 8:
                    posicionesLibresSinFicha[8] = true;
                    break;
                default:
                    break;
            }        
        }
    }

    public void NewMovePlayer()
    {
        moveCount++;
    }

    void SetGameControllerReferenceOnButtons()
    {
        for(int i=0; i<buttonList.Length;i++)
        {
            buttonList[i].GetComponentInParent<GripSpace>().SetGameControllerReference(this);
        }
    }

    private void Update()
    {
        //comprobamos todo el rato si se ha ganado
        if(!endPartida)
        {
            Posibles3EnRaya();
        }
        //si la partida ha acabado ponemos next level
        else
        {
            ActivarNextLevel();
        }
       
    }

    public void ActivarNextLevel()
    {
        nextLevel.SetActive(true);
    }

    public string GetGameSide()
    {
        return gameSide;
    }

    public void EndTurn()
    {
        //turno enemigo
        EnemyTurn();
        //se suma un movimiento mas o ronda mas
        NewMovePlayer();
        //devuelve moveCount
        //ReturnTotalRounds();

    }

    public void ComprobacionJugadas3EnRayaPlayer()
    {
        //1 ---
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            Win();
            endPartida = true;
        }

        //2 ---
        if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            Win();
            endPartida = true;
        }

        //3 ---
        if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            Win();
            endPartida = true;
        }

        //4 ---
        if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            Win();
            endPartida = true;
        }

        //5 ---
        if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            Win();
            endPartida = true;
        }

        //6 ---
        if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            Win();
            endPartida = true;
        }

        //7 
        if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            Win();
            endPartida = true;
        }

        //8
        if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            Win();
            endPartida = true;
        }
    }

    public void ComprobacionJugadas3EnRayaEnemy()
    {
        //1 ---
        if (buttonList[0].text == enemySide && buttonList[1].text == enemySide && buttonList[2].text == enemySide)
        {
            GameOver();
            endPartida = true;
        }

        //2 ---
        if (buttonList[3].text == enemySide && buttonList[4].text == enemySide && buttonList[5].text ==enemySide)
        {
            GameOver();
            endPartida = true;
        }

        //3 ---
        if (buttonList[6].text == enemySide && buttonList[7].text == enemySide && buttonList[8].text == enemySide)
        {
            GameOver();
            endPartida = true;
        }

        //4 ---
        if (buttonList[0].text == enemySide && buttonList[3].text == enemySide && buttonList[6].text == enemySide)
        {
            GameOver();
            endPartida = true;
        }

        //5 ---
        if (buttonList[1].text == enemySide && buttonList[4].text == enemySide && buttonList[7].text == enemySide)
        {
            GameOver();
            endPartida = true;
        }

        //6 ---
        if (buttonList[2].text == enemySide && buttonList[5].text == enemySide && buttonList[8].text == enemySide)
        {
            GameOver();
            endPartida = true;
        }

        //7 
        if (buttonList[0].text == enemySide && buttonList[4].text == enemySide && buttonList[8].text == enemySide)
        {
            GameOver();
            endPartida = true;
        }

        //8
        if (buttonList[2].text == enemySide && buttonList[4].text == enemySide && buttonList[6].text == enemySide)
        {
            GameOver();
            endPartida = true;
        }
    }

    public void Posibles3EnRaya()
    {
        
        //diferentes maneras de acabar juego o lineas para hacer 3 en raya

        ComprobacionJugadas3EnRayaPlayer();

        ComprobacionJugadas3EnRayaEnemy();

        //sino ha ganado alguien antes
        if (moveCount >= maxRounds && !endPartida)
        {
            endPartida = true;
            gameover.SetActive(true);
            gameoverText.text = "Empate";
            DesactivarTableroAlGanarOPerder();
        }
    }

    public void Botonn()
    {
        AudioManager3EnRaya.instance.PlaySFXDuracion("IniciarPartida", 1f);
    }

    


    //ves las fichas restantes y como es dificultad alta 
    public void  ElegirFichaEnemigoAutomatico()
    {

        //en modo facil solo intent ganar el, no para al jugador de intentar ganar y en modo facil el enemigo no empieza en el medio
        //en modo medio el enemigo no empieza en el medio y tapona jugada player 1 sola vez
        //en modo dificil intent ganar el y tapona jugadas player, el enemigo empieza en el medio

        //miramos si ha habido algun posible posicionVictoria, esto es parejas de XX o 00 para ganar tu
        if (posicionesVictoriaEnemy[contadorPosicionesVictoriaEnemy] != 0)
        {
            //ves si hay algun duo de fichas, si es así colocas en la tercera posicion para ganar
            ColocarFichaEnemyCombinacion3();

        }
        //sino hay posibilidad de posicionVictoria vemos si el jugador puede ganar para tapar ese hueco y sino la pones al lado de la ficha haciendo una pareja
        else
        {

                //PLAYER, comprobacion que solo hace en modo dificil y 1 vez en medio, no modo facil
                //comprobamos si hay posibilidad de victoria por parte del player para tapar el hueco, si hay alguna
                if ((posicionesVictoriaPlayer[contadorPosicionesVictoriaPlayer] != 0 && SceneManager.GetActiveScene().name == "mecanicas3EnRayaModoDificil") || (posicionesVictoriaPlayer[contadorPosicionesVictoriaPlayer] != 0 && SceneManager.GetActiveScene().name == "mecanicas3EnRayaModoMedio") && modoMedioTaponarPrimeraJugadaVictoria)
                {
                    TaparHuecoEvitarVictoriaPlayer();
                }
                //si el jugador no tiene ninguna posibilidad de ganar
                else
                {
                    //PAREJA de 00
                    #region PAREJA 00
                        //buscamos una ficha enemy aleatoria en el tablero que sepamos que tiene vecinos vacios y le hacemos una pareja
                        string nameFichaEnemyTablero = DevolverFichaEnemyColocadaEnTableroConVecinosVacios();
                            //miramos si hay alguna ficha enemySide
                            if (nameFichaEnemyTablero != null)
                            {
                                CrearParejaFichasEnemyEnTablero(DevuelveBotonConNombre(nameFichaEnemyTablero));
                            }
                #endregion

                    //FICHA SOLA
                    #region FICHA SOLA
                        //si no hay fichas la pones en el centro
                        else
                        {
                                //si está en modo facil no pone la primera posicion en el medio
                                if (SceneManager.GetActiveScene().name == "mecanicas3EnRayaModoFacil")
                                {
                                    //si está en el modo dificil si pone la primera posicion en el medio
                                    buttonList[1].text = enemySide;
                                    //lo marcas como pulsado
                                    PosicionBotonPulsadoOcupada(buttonList[1].transform.parent.gameObject);
                                }
                                //si esta en modo dificil si pone la primera pos en el medio
                                else if (SceneManager.GetActiveScene().name == "mecanicas3EnRayaModoDificil" || SceneManager.GetActiveScene().name == "mecanicas3EnRayaModoMedio")
                                {
                                    //si está en el modo dificil si pone la primera posicion en el medio
                                    buttonList[4].text = enemySide;
                                    //lo marcas como pulsado
                                    PosicionBotonPulsadoOcupada(buttonList[4].transform.parent.gameObject);
                                }      
                        }
                    #endregion
                }    
        }
        
    }

    //se llama a este si se ha detectado alguna jugada que el player podría hacer o boton que rellenar para ganar
    public void TaparHuecoEvitarVictoriaPlayer()
    {
        //si estamos en modo medio impedimos que repita proceso
        if(SceneManager.GetActiveScene().name == "mecanicas3EnRayaModoMedio")
        {
            modoMedioTaponarPrimeraJugadaVictoria = false;
        }
        //vemos cual es el boton con posibleVictoriaPlayer
        //ya tenemos numero de boton 
        int botonElegido = posicionesVictoriaPlayer[contadorPosicionesVictoriaPlayer] - 1;
        //marcar como pulsado ese boton
        PosicionBotonPulsadoOcupada(devolverBotonConNumero(botonElegido));
        //metodo que al pasarle un numero te devuelva el GameObject boton donde cambias el texto del hijo con enemySide
        devolverBotonConNumero(botonElegido).GetComponentInChildren<TMP_Text>().text = enemySide;
    }


    //metodo que por nombre te busque el GameObject de el boton que tiene ficha enemy
    public GameObject DevuelveBotonConNombre(string name)
    {
        switch (name)
        {
            case "boton0":
                return buttonList[0].transform.parent.gameObject;
            case "boton1":
                return buttonList[1].transform.parent.gameObject;
            case "boton2":
                return buttonList[2].transform.parent.gameObject;
            case "boton3":
                return buttonList[3].transform.parent.gameObject;
            case "boton4":
                return buttonList[4].transform.parent.gameObject;
            case "boton5":
                return buttonList[5].transform.parent.gameObject;
            case "boton6":
                return buttonList[6].transform.parent.gameObject;
            case "boton7":
                return buttonList[7].transform.parent.gameObject;
            case "boton8":
                return buttonList[8].transform.parent.gameObject;
            default:
                break;
        }
        return null;
    }

    public void CrearParejaFichasEnemyEnTablero(GameObject botonElegido)
    {
        //buscas en tablero una ficha con enemySide, la primera que encuentre
        //miras que vecinos tiene como fichas y colocas en un vecino otra ficha enemySide
        Poner0EnFichaVecina(botonElegido);
        

    }

    //buscamos fichas vecinas, cogemos una al azar y le ponemos una ficha enemy
    public void Poner0EnFichaVecina(GameObject botonConFichaEnemy)
    {
        //orden de acabar y salir si es true
        //booleano para salir del bucle infinito while de Poner0EnFichaVecina(botonElegido)
        bool acabado = false;
        //si hay un hueco solo o jugada doble player

        if (CuantosHuecoslibres() == 1)
        {
            //se coge numero boton libre
            int numeroBoton = devolverNumeroConBoton(DevolverUnicoHuecoLibre());
            //se le pone la ficha enemy
            buttonList[numeroBoton].GetComponent<TMP_Text>().text = enemySide;
            
            PosicionBotonPulsadoOcupada(buttonList[numeroBoton].transform.parent.gameObject);
            acabado = true;

        }

        //si es doble jugada player o si solo queda una ficha libre en todo el tablero
        else if (evitarDobleJugadaPlayer)
        {
            buttonList[devolverNumeroConBoton(botonConFichaEnemy)].text = enemySide;
            //marcar como pulsado
            PosicionBotonPulsadoOcupada(buttonList[devolverNumeroConBoton(botonConFichaEnemy)].transform.parent.gameObject);
            evitarDobleJugadaPlayer = false;
            acabado = true;
        }



        System.Random random = new System.Random();

        
            //lista de fichas vecinas con cada boton
            List<int> fichasVecinas = new List<int>();
            //lista de posiciones 
            List<int> posicionesDisponibles = new List<int>();

            if (botonConFichaEnemy.name == "boton0")
            {
                fichasVecinas.AddRange(new int[] { 1, 4, 3 });
            }
            else if (botonConFichaEnemy.name == "boton1")
            {
                fichasVecinas.AddRange(new int[] { 0,4,2 });
            }
            else if (botonConFichaEnemy.name == "boton2")
            {
                fichasVecinas.AddRange(new int[] { 1, 4, 5 });
            }
            else if (botonConFichaEnemy.name == "boton3")
            {
                fichasVecinas.AddRange(new int[] { 0,4,6 });
            }
            else if (botonConFichaEnemy.name == "boton4")
            {
                fichasVecinas.AddRange(new int[] { 0,1,2,3,4,5,6,7,8 });
            }
            else if (botonConFichaEnemy.name == "boton5")
            {
                fichasVecinas.AddRange(new int[] { 2,4,8 });
            }
            else if (botonConFichaEnemy.name == "boton6")
            {
                fichasVecinas.AddRange(new int[] { 3,4,7 });
            }
            else if (botonConFichaEnemy.name == "boton7")
            {
                fichasVecinas.AddRange(new int[] {6,4,8 });
            }
            else if (botonConFichaEnemy.name == "boton8")
            {
                fichasVecinas.AddRange(new int[] { 4,5,7 });
            }

            //añadimos las fichas vecinas del boton a posicionesDisponibles
            foreach (int fichaVecina in fichasVecinas)
            {
                if (buttonList[fichaVecina].text == "")
                {
                    posicionesDisponibles.Add(fichaVecina);
                }
            }
            
                //numero maximo de intentos a ver si encontramos posicion para hacer pareja es el numero de posiciones Disponibles
                int maxAttempts = posicionesDisponibles.Count;

                
                int asignado = -1;
                //vamos mirando entre los vecinos elegimos uno aleatorio hasta que no queden
                while (!acabado && maxAttempts>0)
                {
                    if(maxAttempts > 0 && asignado == -1)
                    {
                        //se elige pos aleatoria
                        int randomValue = random.Next(0, posicionesDisponibles.Count);
                        //pones la posicion disponible que ha salido a la variable asignado
                        asignado = posicionesDisponibles[randomValue];

                        //ves si está vacia y posicionesLibresSinFicha[asignado] == false => se puede poner "0"
                        if (buttonList[asignado].text == "" && posicionesLibresSinFicha[asignado] == false)
                        {
                            buttonList[asignado].text = enemySide;
                            //marcar como pulsado
                            PosicionBotonPulsadoOcupada(buttonList[asignado].transform.parent.gameObject);
                            acabado = true;
                        }
                        //si ya hay text = "X" o no esta vacio
                        else
                        {
                            acabado = false;
                        }

                        //se quita de la lista
                        posicionesDisponibles.Remove(asignado);
                        maxAttempts--;
                        //si se han hecho todos los intentos y no se ha salido se fuerza salir
                        if(maxAttempts == 0) { acabado = true; }
                    }
                }

    }

    public void DesactivarTableroAlGanarOPerder()
    {

        Invoke("DesactivarJugabilidad", 1.5f);
    }

    private void DesactivarJugabilidad()
    {
        //desactivar board,lineas,botones,playerX y player0
        board.SetActive(false);
        lineas.SetActive(false);
        botones.SetActive(false);
        player0.SetActive(false);
        playerX.SetActive(false);
    }

    public int CuantosHuecoslibres()
    {
        int numeroVeces = 0;

        for (int i = 0; i < buttonList.Length; i++)
        {
            if(buttonList[i].text == "")
            {
                numeroVeces++;
            }
            
        }
        return numeroVeces;
    }

    public GameObject DevolverUnicoHuecoLibre()
    {
        

        for (int i = 0; i < buttonList.Length; i++)
        {
            if (buttonList[i].text == "")
            {
                return buttonList[i].transform.parent.gameObject;
            }

        }
        return null;
    }

    public string DevolverFichaEnemyColocadaEnTableroConVecinosVacios()
    {
        System.Random random = new System.Random();
        int intentos = 0;
        //posibles botones que puede devolver, todos
        int cantidadPosiciones = buttonList.Length;
        //posiciones usadas
        bool[] posicionesUsadas = new bool[cantidadPosiciones];

        int posicionActual = 0;

        //hasta que no se haya probado todos los botones
        while (intentos < cantidadPosiciones)
        {
            //antes de probar boton aleatorio vemos si alguna de las esquinas enemigas tiene como vecinos al lado a 2 XX para tapar jugada doble
            if(TaparJugadaDoblePlayer()!=null)
            {
                evitarDobleJugadaPlayer = true;
                return TaparJugadaDoblePlayer().name;
            }
            else
            {
                

                //si la posicion de ese boton no está pillada
                if (!posicionesUsadas[posicionActual])
                {
                    //la ponemos como vista o comprobada
                    posicionesUsadas[posicionActual] = true;

                    //si posee el enemySide "0" lo devolvemos, debemos ver tambien si tiene vecinos libres el boton
                    if (buttonList[posicionActual].text == enemySide && ComprobarSiBotonTieneVecinosLibres(buttonList[posicionActual].transform.parent.gameObject))
                    {
                        return buttonList[posicionActual].transform.parent.gameObject.name;
                    }

                    intentos++;
                    posicionActual++;
                }
            }
           
        }

        return null;
    }

    public GameObject TaparJugadaDoblePlayer()
    {
        //esquinas son 0,2,6,8

            //para evitar doble jugada de player en boton 0
                if (buttonList[1].text == "X" && buttonList[3].text == "X" && buttonList[0].text == "")
                {
                    return  buttonList[0].transform.parent.gameObject;
                }

                //para evitar doble jugada de player en boton 2
                if (buttonList[1].text == "X" && buttonList[5].text == "X" && buttonList[2].text == "")
                {
                    return buttonList[2].transform.parent.gameObject;
                }

                //para evitar doble jugada de player en boton 6
                if (buttonList[7].text == "X" && buttonList[3].text == "X" && buttonList[6].text == "")
                {
                    return buttonList[6].transform.parent.gameObject;
                }

                //para evitar doble jugada de player en boton 8
                if (buttonList[7].text == "X" && buttonList[5].text == "X" && buttonList[8].text == "")
                {
                     return buttonList[8].transform.parent.gameObject;
                }


        return null;
           
    }

    //te devuelve y te dice si el boton como argumento tiene algun vecino libre o no
    public bool ComprobarSiBotonTieneVecinosLibres(GameObject boton)
    {
        //miramos que boton es, segun cual sea comprobamos cada vecino
        switch (boton.name)
        {
            //vecinos 1, 4, 3
            case "boton0":
                if (buttonList[1].text == "") { return true; }
                else if (buttonList[4].text == "") { return true; }
                else if (buttonList[3].text == "") { return true; }
                else if (buttonList[3].text == "") { return true; }
                //para evitar doble jugada de player
                //else if (buttonList[1].text == "X" && buttonList[3].text == "X") { return true; }
                //si ningun vecino está libre
                else { return false; }

            //vecinos 0,4,2
            case "boton1":
                if (buttonList[0].text == "") { return true; }
                else if (buttonList[4].text == "") { return true; }
                else if (buttonList[2].text == "") { return true; }
               
                //si ningun vecino está libre
                else { return false; }

            //vecinos 1, 4, 5
            case "boton2":
                if (buttonList[1].text == "") { return true; }
                else if (buttonList[4].text == "") { return true; }
                else if (buttonList[5].text == "") { return true; }
                //para evitar doble jugada de player
                //else if (buttonList[1].text == "X" && buttonList[5].text == "X") { return true; }
                //si ningun vecino está libre
                else { return false; }

            //vecinos 0,4,6
            case "boton3":
                if (buttonList[0].text == "") { return true; }
                else if (buttonList[4].text == "") { return true; }
                else if (buttonList[6].text == "") { return true; }
                //si ningun vecino está libre
                else { return false; }

            //0,1,2,3,4,5,6,7,8
            case "boton4":
                if (buttonList[0].text == "") { return true; }
                else if (buttonList[1].text == "") { return true; }
                else if (buttonList[2].text == "") { return true; }
                else if (buttonList[3].text == "") { return true; }
                else if (buttonList[4].text == "") { return true; }
                else if (buttonList[5].text == "") { return true; }
                else if (buttonList[6].text == "") { return true; }
                else if (buttonList[7].text == "") { return true; }
                else if (buttonList[8].text == "") { return true; }
                //si ningun vecino está libre
                else { return false; }

            //vecinos 2,4,8
            case "boton5":
                if (buttonList[2].text == "") { return true; }
                else if (buttonList[4].text == "") { return true; }
                else if (buttonList[8].text == "") { return true; }
                //si ningun vecino está libre
                else { return false; }

            //vecinos 3,4,7
            case "boton6":
                if (buttonList[3].text == "") { return true; }
                else if (buttonList[4].text == "") { return true; }
                else if (buttonList[7].text == "") { return true; }
                //para evitar doble jugada de player
                //else if (buttonList[3].text == "X" && buttonList[7].text == "X") { return true; }
                //si ningun vecino está libre
                else { return false; }

            //vecinos 6,4,8
            case "boton7":
                if (buttonList[1].text == "") { return true; }
                else if (buttonList[4].text == "") { return true; }
                else if (buttonList[3].text == "") { return true; }
                //si ningun vecino está libre
                else { return false; }

            // vecinos 4,5,7
            case "boton8":
                if (buttonList[4].text == "") { return true; }
                else if (buttonList[5].text == "") { return true; }
                else if (buttonList[7].text == "") { return true; }
                //para evitar doble jugada de player
                //else if (buttonList[5].text == "X" && buttonList[7].text == "X") { return true; }
                //si ningun vecino está libre
                else { return false; }
            default:
                break;
        }

        return false;
        
    }

    //colocar ficha en la primera posicion del array posicionVictoriaEnemy
    public void ColocarFichaEnemyCombinacion3()
    {
        //colocamos ficha enemy en la primera posicion del array posicionVictoriaEnemy

            //ya tenemos numero de boton 
            int botonElegido = posicionesVictoriaEnemy[contadorPosicionesVictoriaEnemy] - 1;
            //marcar como pulsado
            PosicionBotonPulsadoOcupada(devolverBotonConNumero(botonElegido));
            //metodo que al pasarle un numero te devuelva el GameObject boton donde cambias el texto del hijo con enemySide
            devolverBotonConNumero(botonElegido).GetComponentInChildren<TMP_Text>().text = enemySide;

    }

    public GameObject devolverBotonConNumero(int numeroBoton)
    {
        switch (numeroBoton)
        {
            case 0:
                return buttonList[0].transform.parent.gameObject;  
            case 1:
                return buttonList[1].transform.parent.gameObject;
            case 2:
                return buttonList[2].transform.parent.gameObject;
            case 3:
                return buttonList[3].transform.parent.gameObject;
            case 4:
                return buttonList[4].transform.parent.gameObject;
            case 5:
                return buttonList[5].transform.parent.gameObject;
            case 6:
                return buttonList[6].transform.parent.gameObject;
            case 7:
                return buttonList[7].transform.parent.gameObject;
            case 8:
                return buttonList[8].transform.parent.gameObject;
            default:
                break;
        }
        return null;
    }


    public int devolverNumeroConBoton(GameObject boton)
    {
        switch (boton.name)
        {
            case "boton0":
                return 0;
            case "boton1":
                return 1;
            case "boton2":
                return 2;
            case "boton3":
                return 3;
            case "boton4":
                return 4;
            case "boton5":
                return 5;
            case "boton6":
                return 6;
            case "boton7":
                return 7;
            case "boton8":
                return 8;
            default:
                break;
        }
        return -1;
    }

    //buscar pareja enemies 2s 00
    public void Combinacion200XX()
    {
        //comprobamos el buttonList para ver cuales no estan vacios o sin jugar
        for (int i = 0; i < buttonList.Length; i++)
        {
            //por cada boton que no esté vacio y tenga 0 o X buscamos en sus laterales o diagonales
            if (buttonList[i].text != "")
            {
                BuscarParejaCombinacion(buttonList[i]);
            }
        }
    }

    public void ReiniciarContador()
    {
        contadorPosicionesVictoriaEnemy = 0;
        contadorPosicionesVictoriaPlayer = 0;
    }

    //mira en laterales,diagonales y todo
    public void BuscarParejaCombinacion(TMP_Text botonElegido)
    {
        //miramos si es buttonList[0] ya que sus casillas vecinas serán unas
        if(botonElegido == buttonList[0])
        {
            if (buttonList[0].text == "0")
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(1, 3, 4, 2, 6, 8, botonElegido);
                //solo comprueba las de al lado, no victorias de 0-0, vamos a ello,reutilizamos metodo pero diferentes vecinos, los de los extremos ahora
                MirarCasillasAdyacentesYColocarTerceraEnemy(2, 6, 8, 1, 3, 4, botonElegido);
            }
            else if (buttonList[0].text == "X")
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraPlayer(1, 3, 4, 2, 6, 8, botonElegido);
                //solo comprueba las de al lado, no victorias de X-X, vamos a ello,reutilizamos metodo pero diferentes vecinos, los de los extremos ahora
                MirarCasillasAdyacentesYColocarTerceraPlayer(2, 6, 8, 1, 3, 4, botonElegido);
            } 
        }
        //miramos si es buttonList[1] ya que sus casillas vecinas serán unas
        else if (botonElegido == buttonList[1])
        {
            if (buttonList[1].text == "0")
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(0, 2, 4, 2, 0, 7, botonElegido);
                //solo comprueba las de al lado, no victorias de 0-0, vamos a ello,reutilizamos metodo pero diferentes vecinos, los de los extremos ahora
                MirarCasillasAdyacentesYColocarTerceraEnemy( 2, 0, 7, 0, 2, 4, botonElegido);
            }
            else if (buttonList[1].text == "X")
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraPlayer(0, 2, 4, 2, 0, 7, botonElegido);
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraPlayer(2, 0, 7, 0, 2, 4, botonElegido);
            }
        }
        //miramos si es buttonList[2] ya que sus casillas vecinas serán unas
        else if (botonElegido == buttonList[2])
        {
            if (buttonList[2].text == "0")
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(1, 4, 5, 0, 6, 8, botonElegido);
                //solo comprueba las de al lado, no victorias de 0-0, vamos a ello,reutilizamos metodo pero diferentes vecinos, los de los extremos ahora
                MirarCasillasAdyacentesYColocarTerceraEnemy(0, 6, 8, 1, 4, 5, botonElegido);
            }
            else if (buttonList[2].text == "X")
            {

                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraPlayer(1, 4, 5, 0, 6, 8, botonElegido);
                //solo comprueba las de al lado, no victorias de 0-0, vamos a ello,reutilizamos metodo pero diferentes vecinos, los de los extremos ahora
                MirarCasillasAdyacentesYColocarTerceraPlayer(0, 6, 8, 1, 4, 5, botonElegido);
            }

        }
        //miramos si es buttonList[3] ya que sus casillas vecinas serán unas
        else if (botonElegido == buttonList[3])
        {
            if (buttonList[3].text == "0")
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(0, 4, 6, 6, 5, 0, botonElegido);
                //solo comprueba las de al lado, no victorias de 0-0, vamos a ello,reutilizamos metodo pero diferentes vecinos, los de los extremos ahora
                MirarCasillasAdyacentesYColocarTerceraEnemy(6, 5, 0, 0, 4, 6, botonElegido);
            }
            else if (buttonList[3].text == "X")
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraPlayer(0, 4, 6, 6, 5, 0, botonElegido);
                //solo comprueba las de al lado, no victorias de 0-0, vamos a ello,reutilizamos metodo pero diferentes vecinos, los de los extremos ahora
                MirarCasillasAdyacentesYColocarTerceraPlayer(6, 5, 0, 0, 4, 6, botonElegido);
            }

        }
        //miramos si es buttonList[4] ya que sus casillas vecinas serán unas
        else if (botonElegido == buttonList[4])
        {
            if (buttonList[4].text == "0")
            {
                MirarCasillasAdyacentes8YColocarTerceraEnemy(botonElegido);

            }
            else if (buttonList[4].text == "X")
            {

                MirarCasillasAdyacentes8YColocarTerceraPlayer(botonElegido);

            }

        }
        //miramos si es buttonList[5] ya que sus casillas vecinas serán unas
        else if (botonElegido == buttonList[5])
        {
            if (buttonList[5].text == "0")
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(2, 4, 8, 8, 3, 2, botonElegido);
                //solo comprueba las de al lado, no victorias de 0-0, vamos a ello,reutilizamos metodo pero diferentes vecinos, los de los extremos ahora
                MirarCasillasAdyacentesYColocarTerceraEnemy(8, 3, 2, 2, 4, 8, botonElegido);

            }
            else if (buttonList[5].text == "X")
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraPlayer(2, 4, 8, 8, 3, 2, botonElegido);
                //solo comprueba las de al lado, no victorias de 0-0, vamos a ello,reutilizamos metodo pero diferentes vecinos, los de los extremos ahora
                MirarCasillasAdyacentesYColocarTerceraPlayer(8, 3, 2, 2, 4, 8, botonElegido);
            }
        }
        //miramos si es buttonList[6] ya que sus casillas vecinas serán unas
        else if (botonElegido == buttonList[6])
        {
            if (buttonList[6].text == "0")
            {
                MirarCasillasAdyacentesYColocarTerceraEnemy(3, 4, 7, 0, 2, 8, botonElegido);
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(0, 2, 8, 3, 4, 7, botonElegido);
            }
            else if (buttonList[6].text == "X")
            {
                MirarCasillasAdyacentesYColocarTerceraPlayer(3, 4, 7, 0, 2, 8, botonElegido);
                //solo comprueba las de al lado, no victorias de 0-0, vamos a ello,reutilizamos metodo pero diferentes vecinos, los de los extremos ahora
                MirarCasillasAdyacentesYColocarTerceraPlayer(0, 2, 8, 3, 4, 7, botonElegido);
            }

        }
        //miramos si es buttonList[7] ya que sus casillas vecinas serán unas
        else if (botonElegido == buttonList[7])
        {
            if (buttonList[7].text == "0")
            {
                MirarCasillasAdyacentesYColocarTerceraEnemy(6, 4, 8, 8, 1, 6, botonElegido);
                MirarCasillasAdyacentesYColocarTerceraEnemy(8, 1, 6,6, 4, 8,  botonElegido);
            }
            else if (buttonList[7].text == "X")
            {
                MirarCasillasAdyacentesYColocarTerceraPlayer(6, 4, 8, 8, 1, 6, botonElegido);
                MirarCasillasAdyacentesYColocarTerceraPlayer( 8, 1, 6, 6, 4, 8, botonElegido);
            }
        }
        //miramos si es buttonList[8] ya que sus casillas vecinas serán unas
        else if (botonElegido == buttonList[8])
        {
            if (buttonList[8].text == "0")
            {
                MirarCasillasAdyacentesYColocarTerceraEnemy(7, 4, 5, 6, 0, 2, botonElegido);
                MirarCasillasAdyacentesYColocarTerceraEnemy(6, 0, 2, 7, 4, 5,  botonElegido);
            }
            else if (buttonList[8].text == "X")
            {
                MirarCasillasAdyacentesYColocarTerceraPlayer(7, 4, 5, 6, 0, 2, botonElegido);
                MirarCasillasAdyacentesYColocarTerceraPlayer(6, 0, 2, 7, 4, 5,  botonElegido);
            }
        }
       
    }


    #region AveriguarPosicionVictoriaEnemy

    //para resto de botones, sabiendo la combinacion de 00 y en qje botones estan devuelve el boton que tendría la ficha ganadora
    public void MirarCasillasAdyacentesYColocarTerceraEnemy(int vecino1, int vecino2, int vecino3, int ultimaCasillaTriovecino1, int ultimaCasillaTriovecino2, int ultimaCasillaTriovecino3, TMP_Text botonElegido)
    {
        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        if (botonElegido.text == "0" && buttonList[vecino1].text == "0" && botonElegido.text == buttonList[vecino1].text && buttonList[ultimaCasillaTriovecino1].text == "")
        {
            //Debug.Log("Si colocas la ficha " + enemySide + " en " + buttonList[ultimaCasillaTriovecino1].transform.parent.gameObject.name + " ganas, y haces trio");

            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[ultimaCasillaTriovecino1].transform.parent.gameObject.name);
        }

        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        else if (botonElegido.text == "0" && buttonList[vecino2].text == "0" && botonElegido.text == buttonList[vecino2].text && buttonList[ultimaCasillaTriovecino2].text == "")
        {
            //Debug.Log("Si colocas la ficha " + enemySide + " en " + buttonList[ultimaCasillaTriovecino2].transform.parent.gameObject.name + " ganas, y haces trio");

            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[ultimaCasillaTriovecino2].transform.parent.gameObject.name);
        }

        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        else if (botonElegido.text == "0" && buttonList[vecino3].text == "0" && botonElegido.text == buttonList[vecino3].text && buttonList[ultimaCasillaTriovecino3].text == "")
        {
            //Debug.Log("Si colocas la ficha " + enemySide + " en " + buttonList[ultimaCasillaTriovecino3].transform.parent.gameObject.name + " ganas, y haces trio");

            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[ultimaCasillaTriovecino3].transform.parent.gameObject.name);
        }
    }




    

    //para el boton 4 con todas las casillas vecinas y colocar la ficha vencedora
    public void MirarCasillasAdyacentes8YColocarTerceraEnemy(TMP_Text botonElegido4)
    {

        //vemos si hay un duo entre el boton 4 y el boton 0 con O y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 0 y 4 seria 8
        if (botonElegido4.text == "0" && buttonList[0].text == "0" && botonElegido4.text == buttonList[0].text && buttonList[8].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[8].transform.parent.gameObject.name + " ganas, y haces trio");
            
            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[8].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 0 y el boton 8 con O y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 0 y 8 seria 4
        if (botonElegido4.text == "" && buttonList[0].text == "0" && botonElegido4.text == buttonList[0].text && buttonList[8].text == "0")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[8].transform.parent.gameObject.name + " ganas, y haces trio");

            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[4].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 1 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 1 y 4 seria 7
        else if(botonElegido4.text == "0" && buttonList[1].text == "0" && botonElegido4.text == buttonList[1].text && buttonList[7].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[7].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 1 y el boton 7 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 1 y 7 seria 4
        else if (botonElegido4.text == "" && buttonList[1].text == "0" && botonElegido4.text == buttonList[1].text && buttonList[7].text == "0")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[4].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 2 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 2 y 4 seria 6
        else if (botonElegido4.text == "0" && buttonList[2].text == "0" && botonElegido4.text == buttonList[2].text && buttonList[6].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[6].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[6].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 2 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 2 y 4 seria 6
        else if (botonElegido4.text == "" && buttonList[2].text == "0" && botonElegido4.text == buttonList[2].text && buttonList[6].text == "0")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[6].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[4].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 3 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 3 y 4 seria 5
        else if (botonElegido4.text == "0" && buttonList[3].text == "0" && botonElegido4.text == buttonList[3].text && buttonList[5].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[5].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[5].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 3 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 3 y 4 seria 5
        else if (botonElegido4.text == "" && buttonList[3].text == "0" && botonElegido4.text == buttonList[3].text && buttonList[5].text == "0")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[5].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[4].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 5 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 5 y 4 seria 3
        else if (botonElegido4.text == "0" && buttonList[5].text == "0" && botonElegido4.text == buttonList[5].text && buttonList[3].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[3].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 5 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 5 y 4 seria 3
        else if (botonElegido4.text == "" && buttonList[5].text == "0" && botonElegido4.text == buttonList[5].text && buttonList[3].text == "0")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[4].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 6 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 6 y 4 seria 2
        else if (botonElegido4.text == "0" && buttonList[6].text == "0" && botonElegido4.text == buttonList[6].text && buttonList[2].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[2].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[2].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 6 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 6 y 4 seria 2
        else if (botonElegido4.text == "" && buttonList[6].text == "0" && botonElegido4.text == buttonList[6].text && buttonList[2].text == "0")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[2].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[4].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 7 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 7 y 4 seria 1
        else if (botonElegido4.text == "0" && buttonList[7].text == "0" && botonElegido4.text == buttonList[7].text && buttonList[1].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[1].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[1].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 7 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 7 y 4 seria 1
        else if (botonElegido4.text == "" && buttonList[7].text == "0" && botonElegido4.text == buttonList[7].text && buttonList[1].text == "0")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[1].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[4].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 8 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 8 y 4 seria 0
        else if (botonElegido4.text == "0" && buttonList[8].text == "0" && botonElegido4.text == buttonList[8].text && buttonList[0].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[0].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[0].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 8 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 8 y 4 seria 0
        else if (botonElegido4.text == "" && buttonList[8].text == "0" && botonElegido4.text == buttonList[8].text && buttonList[0].text == "0")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[0].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[4].transform.parent.gameObject.name);
        }

    }

    #endregion

    #region PosicionVictoriaEnemy

    //añadir nueva Posicion Victoria Enemy del 1 al 9, ya que 0 significará elemento vacio
    public void NuevaPosicionVictoriaEnemy( string nombreBoton)
    {
        int posicionVictoria = 0;
        //segun el gameObject que pasemos será una posicion u otra
        switch (nombreBoton)
        {
            case "boton0":
                posicionVictoria = 1;
                break;
            case "boton1":
                posicionVictoria = 2;
                break;
            case "boton2":
                posicionVictoria = 3;
                break;
            case "boton3":
                posicionVictoria = 4;
                break;
            case "boton4":
                posicionVictoria = 5;
                break;
            case "boton5":
                posicionVictoria = 6;
                break;
            case "boton6":
                posicionVictoria = 7;
                break;
            case "boton7":
                posicionVictoria = 8;
                break;
            case "boton8":
                posicionVictoria = 9;
                break;
            default:
                break;
        }
        //añadimos la posicion al array que seria el numero el boton +1
        posicionesVictoriaEnemy[contadorPosicionesVictoriaEnemy] = posicionVictoria;
        contadorPosicionesVictoriaEnemy++;

    }

    //limpiar todas las posiciones, ya que 0 significará elemento vacio
    public void LimpiarPosicionesVictoriaRondaAnterior()
    {
        for(int i=0; i<posicionesVictoriaEnemy.Length; i++)
        {
            posicionesVictoriaEnemy[contadorPosicionesVictoriaEnemy] = 0;
            posicionesVictoriaPlayer[contadorPosicionesVictoriaEnemy] = 0;
        }
        contadorPosicionesVictoriaEnemy = 0;
    }

    #endregion

    #region AveriguarPosicionVictoriaPlayer
    //para resto de botones, sabiendo la combinacion de XX y en qje botones estan devuelve el boton que tendría la ficha ganadora
    public void MirarCasillasAdyacentesYColocarTerceraPlayer(int vecino1, int vecino2, int vecino3, int ultimaCasillaTriovecino1, int ultimaCasillaTriovecino2, int ultimaCasillaTriovecino3, TMP_Text botonElegido)
    {
        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        if (botonElegido.text == "X" && buttonList[vecino1].text == "X" && botonElegido.text == buttonList[vecino1].text && buttonList[ultimaCasillaTriovecino1].text == "")
        {
            
            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[ultimaCasillaTriovecino1].transform.parent.gameObject.name);
        }

        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        else if (botonElegido.text == "X" && buttonList[vecino2].text == "X" && botonElegido.text == buttonList[vecino2].text && buttonList[ultimaCasillaTriovecino2].text == "")
        {
            

            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[ultimaCasillaTriovecino2].transform.parent.gameObject.name);
        }

        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        else if (botonElegido.text == "X" && buttonList[vecino3].text == "X" && botonElegido.text == buttonList[vecino3].text && buttonList[ultimaCasillaTriovecino3].text == "")
        {
            //Debug.Log("Si colocas la ficha " + enemySide + " en " + buttonList[ultimaCasillaTriovecino3].transform.parent.gameObject.name + " ganas, y haces trio");

            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[ultimaCasillaTriovecino3].transform.parent.gameObject.name);
        }
    }



    //para el boton 4 con todas las casillas vecinas y colocar la ficha vencedora
    public void MirarCasillasAdyacentes8YColocarTerceraPlayer(TMP_Text botonElegido4)
    {

        //vemos si hay un duo entre el boton 4 y el boton 0 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 0 y 4 seria 8
        if (botonElegido4.text == "X" && buttonList[0].text == "X" && botonElegido4.text == buttonList[0].text && buttonList[8].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[8].transform.parent.gameObject.name + " ganas, y haces trio");

            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[8].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 0 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 0 y 4 seria 8
        else if (botonElegido4.text == "" && buttonList[0].text == "X" && botonElegido4.text == buttonList[0].text && buttonList[8].text == "X")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[8].transform.parent.gameObject.name + " ganas, y haces trio");

            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[4].transform.parent.gameObject.name);
        }


        //vemos si hay un duo entre el boton 4 y el boton 1 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 1 y 4 seria 7
        else if (botonElegido4.text == "X" && buttonList[1].text == "X" && botonElegido4.text == buttonList[1].text && buttonList[7].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[4].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 1 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 1 y 4 seria 7
        else if (botonElegido4.text == "" && buttonList[1].text == "X" && botonElegido4.text == buttonList[1].text && buttonList[7].text == "X")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[4].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 2 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 2 y 4 seria 6
        else if (botonElegido4.text == "X" && buttonList[2].text == "X" && botonElegido4.text == buttonList[2].text && buttonList[6].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[6].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[6].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 2 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 2 y 4 seria 6
        else if (botonElegido4.text == "" && buttonList[2].text == "X" && botonElegido4.text == buttonList[2].text && buttonList[6].text == "X")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[6].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[4].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 3 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 3 y 4 seria 5
        else if (botonElegido4.text == "X" && buttonList[3].text == "X" && botonElegido4.text == buttonList[3].text && buttonList[5].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[5].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[5].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 3 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 3 y 4 seria 5
        else if (botonElegido4.text == "" && buttonList[3].text == "X" && botonElegido4.text == buttonList[3].text && buttonList[5].text == "X")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[5].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[4].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 5 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 5 y 4 seria 3
        else if (botonElegido4.text == "X" && buttonList[5].text == "X" && botonElegido4.text == buttonList[5].text && buttonList[3].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[3].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 5 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 5 y 4 seria 3
        else if (botonElegido4.text == "" && buttonList[5].text == "X" && botonElegido4.text == buttonList[5].text && buttonList[3].text == "X")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[4].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 6 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 6 y 4 seria 2
        else if (botonElegido4.text == "X" && buttonList[6].text == "X" && botonElegido4.text == buttonList[6].text && buttonList[2].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[2].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[2].transform.parent.gameObject.name);
        }
        //vemos si hay un duo entre el boton 4 y el boton 6 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 6 y 4 seria 2
        else if (botonElegido4.text == "" && buttonList[6].text == "X" && botonElegido4.text == buttonList[6].text && buttonList[2].text == "X")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[2].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[4].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 7 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 7 y 4 seria 1
        else if (botonElegido4.text == "X" && buttonList[7].text == "X" && botonElegido4.text == buttonList[7].text && buttonList[1].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[1].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[1].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 7 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 7 y 4 seria 1
        else if (botonElegido4.text == "" && buttonList[7].text == "X" && botonElegido4.text == buttonList[7].text && buttonList[1].text == "X")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[1].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[4].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 8 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 8 y 4 seria 0
        else if (botonElegido4.text == "X" && buttonList[8].text == "X" && botonElegido4.text == buttonList[8].text && buttonList[0].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[0].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[0].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 8 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 8 y 4 seria 0
        else if (botonElegido4.text == "" && buttonList[8].text == "X" && botonElegido4.text == buttonList[8].text && buttonList[0].text == "X")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[0].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaPlayer(buttonList[4].transform.parent.gameObject.name);
        }

    }
    #endregion

    #region PosicionVictoriaPlayer
    //añadir nueva Posicion Victoria Player del 1 al 9, ya que 0 significará elemento vacio
    public void NuevaPosicionVictoriaPlayer(string nombreBoton)
    {
        int posicionVictoria = 0;
        //segun el gameObject que pasemos será una posicion u otra
        switch (nombreBoton)
        {
            case "boton0":
                posicionVictoria = 1;
                break;
            case "boton1":
                posicionVictoria = 2;
                break;
            case "boton2":
                posicionVictoria = 3;
                break;
            case "boton3":
                posicionVictoria = 4;
                break;
            case "boton4":
                posicionVictoria = 5;
                break;
            case "boton5":
                posicionVictoria = 6;
                break;
            case "boton6":
                posicionVictoria = 7;
                break;
            case "boton7":
                posicionVictoria = 8;
                break;
            case "boton8":
                posicionVictoria = 9;
                break;
            default:
                break;
        }
        //añadimos la posicion al array que seria el numero el boton +1
        posicionesVictoriaPlayer[contadorPosicionesVictoriaPlayer] = posicionVictoria;
        contadorPosicionesVictoriaPlayer++;

    }
    #endregion

    //para decir si ha ganado enemy
    void GameOver()
    {
        AudioManager3EnRaya.instance.PlaySFXDuracion("Perder", 1f);
        SetBoardInteractable(false);
        DesactivarTableroAlGanarOPerder();

        gameover.SetActive(true);
        gameoverText.text = "O has ganado";
    }

    //para decir si ha ganado enemy
    void Win()
    {
        AudioManager3EnRaya.instance.PlaySFXDuracion("Ganar", 1f);
        SetBoardInteractable(false);
        DesactivarTableroAlGanarOPerder();

        win.SetActive(true);
        gameoverText.text ="X has ganado";
    }

    //segun en que nivel estés pasas a uno u otro
    public void NextLevel()
    {
        //si estas en nivel facil pasas a nivel dificil
        if (SceneManager.GetActiveScene().name == "mecanicas3EnRayaModoFacil")
        {
            SceneManager.LoadScene("mecanicas3EnRayaModoMedio");
        }
        //si esta en modo dificil pasas a modo facil
        else if (SceneManager.GetActiveScene().name == "mecanicas3EnRayaModoDificil")
        {
            SceneManager.LoadScene("mecanicas3EnRayaModoFacil");
        }

        //si esta en modo dificil pasas a modo facil
        else if (SceneManager.GetActiveScene().name == "mecanicas3EnRayaModoMedio")
        {
            //SceneManager.LoadScene("mecanicas3EnRayaModoDificil");
            SceneManager.LoadScene("tutorial");
        }
    }

    public void ChangeSides(string gameSide)
    {
        //paneles canvas
        TurnoJugadorCanvas(gameSide);
    }

    void TurnoJugadorCanvas(string gameSides)
    {
        gameSide = gameSides;
        //si playerSide es "X" activamos playerX en pantalla
        if (gameSide == "X")
        {

            playerX.SetActive(true);
            player0.SetActive(false);

        }
        else if (gameSide == "0")
        {
            playerX.SetActive(false);
            player0.SetActive(true);

            
        }
        //si playerSide es "Y" activamos playerY en pantalla
    }

    public void RestartGame()
    {
        //reinicia escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }


    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }
}
