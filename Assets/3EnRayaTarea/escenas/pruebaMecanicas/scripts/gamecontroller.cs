using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class gamecontroller : MonoBehaviour
{
    public TMP_Text[] buttonList;

    private string playerSide = "X";
    private string enemySide = "0";
    //el turno que toque
    private string gameSide;

    public GameObject gameover;
    public TMP_Text gameoverText;

    private int moveCount;

    public GameObject restartButton;

    public GameObject playerX;

    public GameObject player0;

    //creamos array tamaño 4 para poner ahí posiciones donde el enemigo si mueve ficha puede ganar
    //se actualiza en cada turno
    int[] posicionesVictoriaEnemy;

    int contadorPosicionesVictoriaEnemy = 0;


    private void Start()
    {
        //tamaño 4
        posicionesVictoriaEnemy = new int[8];

        
    }

    public void EnemyTurn()
    {
        //turno del enemigo, sabiendo lugares que poner ficha para ganar
        ElegirFichaEnemigoAutomatico();
        //eliminamos para en siguiente ronda volver a poner posicionesVictoriaEnemy
        LimpiarPosicionesVictoriaRondaAnterior();
        //cambiar turno
        ChangeSides();
    }
    private void Awake()
    {
        gameover.SetActive(false);
        SetGameControllerReferenceOnButtons();
        //siempre empieza el jugador
        gameSide = enemySide;
        moveCount = 0;
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
        TurnoJugadorCanvas();
    }

    public string GetGameSide()
    {
        return gameSide;
    }

    public void EndTurn()
    {

        moveCount++;
        //diferentes maneras de acabar juego o lineas para hacer 3 en raya

        //1 ---
        if (buttonList[0].text == gameSide && buttonList[1].text == gameSide && buttonList[2].text == gameSide)
        {
            GameOver();
        }

        //2 ---
        if (buttonList[3].text == gameSide && buttonList[4].text == gameSide && buttonList[5].text == gameSide)
        {
            GameOver();
        }

        //3 ---
        if (buttonList[6].text == gameSide && buttonList[7].text == gameSide && buttonList[8].text == gameSide)
        {
            GameOver();
        }

        //4 ---
        if (buttonList[0].text == gameSide && buttonList[3].text == gameSide && buttonList[6].text == gameSide)
        {
            GameOver();
        }

        //5 ---
        if (buttonList[1].text == gameSide && buttonList[4].text == gameSide && buttonList[7].text == gameSide)
        {
            GameOver();
        }

        //6 ---
        if (buttonList[2].text == gameSide && buttonList[5].text == gameSide && buttonList[8].text == gameSide)
        {
            GameOver();
        }

        //7 
        if (buttonList[0].text == gameSide && buttonList[4].text == gameSide && buttonList[8].text == gameSide)
        {
            GameOver();
        }

        //8
        if (buttonList[2].text == gameSide && buttonList[4].text == gameSide && buttonList[6].text == gameSide)
        {
            GameOver();
        }

        if(moveCount >= 9)
        {
            gameover.SetActive(true);
            gameoverText.text = "Empate";
        }

        ChangeSides();



        
    }


    //ves las fichas restantes y como es dificultad alta 
    public void  ElegirFichaEnemigoAutomatico()
    {

        //miramos si ha habido algun posible posicionVictoria, esto es parejas de XX o 00
        if (posicionesVictoriaEnemy[contadorPosicionesVictoriaEnemy] != 0)
        {
            //ves si hay algun duo de fichas, si es así colocas en la tercera posicion para ganar
            ColocarFichaEnemyCombinacion3();
            //ganas
            EndTurn();
        }
        //sino hay posibilidad de posicionVictoria la pones al lado de la ficha haciendo una pareja
        else
        {
           
            //miramos si hay alguna ficha enemySide
            if (DevolverFichaEnemyColocadaEnTablero() != null)
            {
                CrearParejaFichasEnemyEnTablero();
            }
            //si no hay fichas la pones en el centro
            else
            {
                buttonList[4].text = enemySide;
            }
        }
        
        

       
    }

    public void CrearParejaFichasEnemyEnTablero()
    {
        //buscas en tablero una ficha con enemySide
        //miras que vecinos tiene como fichas y colocas en un vecino otra ficha enemySide
        Poner0EnFichaVecina(DevolverFichaEnemyColocadaEnTablero());
        

    }

    //buscamos fichas vecinas, cogemos una al azar y le ponemos una ficha enemy
    public void Poner0EnFichaVecina(GameObject botonConFichaEnemy)
    {
        int randomValue =0;

        //segun que boton sea devuelve unas fichas vecinas y dentro de esas fichas vecinas pone ficha enemy
        if (botonConFichaEnemy.name == "boton0")
        {
            System.Random random = new System.Random();

            int maxAttempts = 3; // Número máximo de intentos

            //vecinas fichas
            int fichaVecina1 = 1;
            int fichaVecina4 = 4;
            int fichaVecina3 = 3;

            bool asignado = false;

            do
            {
                // Genera un valor aleatorio entre 0 y 2
                randomValue = random.Next(0, 3);

                switch (randomValue)
                {
                    case 0:
                        if (buttonList[fichaVecina1].text == "")
                        {
                            buttonList[fichaVecina1].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 1:
                        if (buttonList[fichaVecina4].text == "")
                        {
                            buttonList[fichaVecina4].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 2:
                        if (buttonList[fichaVecina3].text == "")
                        {
                            buttonList[fichaVecina3].text = enemySide;
                            asignado = true;
                        }
                        break;
                }

                maxAttempts--;

            } while (!asignado && maxAttempts > 0);
        }
        else if (botonConFichaEnemy.name == "boton1")
        {

            System.Random random = new System.Random();

            int maxAttempts = 3; // Número máximo de intentos

            //vecinas fichas
            int fichaVecina0 = 0;
            int fichaVecina4 = 4;
            int fichaVecina2 = 2;

            bool asignado = false;

            do
            {
                // Genera un valor aleatorio entre 0 y 2
                randomValue = random.Next(0, 3);

                switch (randomValue)
                {
                    case 0:
                        if (buttonList[fichaVecina0].text == "")
                        {
                            buttonList[fichaVecina0].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 1:
                        if (buttonList[fichaVecina4].text == "")
                        {
                            buttonList[fichaVecina4].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 2:
                        if (buttonList[fichaVecina2].text == "")
                        {
                            buttonList[fichaVecina2].text = enemySide;
                            asignado = true;
                        }
                        break;
                }

                maxAttempts--;

            } while (!asignado && maxAttempts > 0);
        }
        else if (botonConFichaEnemy.name == "boton2")
        {

            System.Random random = new System.Random();

            int maxAttempts = 3; // Número máximo de intentos

            //vecinas fichas
            int fichaVecina1 = 1;
            int fichaVecina4 = 4;
            int fichaVecina5 = 5;

            bool asignado = false;

            do
            {
                // Genera un valor aleatorio entre 0 y 2
                randomValue = random.Next(0, 3);

                switch (randomValue)
                {
                    case 0:
                        if (buttonList[fichaVecina1].text == "")
                        {
                            buttonList[fichaVecina1].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 1:
                        if (buttonList[fichaVecina4].text == "")
                        {
                            buttonList[fichaVecina4].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 2:
                        if (buttonList[fichaVecina5].text == "")
                        {
                            buttonList[fichaVecina5].text = enemySide;
                            asignado = true;
                        }
                        break;
                }

                maxAttempts--;

            } while (!asignado && maxAttempts > 0);
        }
        else if (botonConFichaEnemy.name == "boton3")
        {

            System.Random random = new System.Random();

            int maxAttempts = 3; // Número máximo de intentos

            //vecinas fichas
            int fichaVecina0 = 0;
            int fichaVecina4 = 4;
            int fichaVecina6 = 6;

            bool asignado = false;

            do
            {
                // Genera un valor aleatorio entre 0 y 2
                randomValue = random.Next(0, 3);

                switch (randomValue)
                {
                    case 0:
                        if (buttonList[fichaVecina0].text == "")
                        {
                            buttonList[fichaVecina0].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 1:
                        if (buttonList[fichaVecina4].text == "")
                        {
                            buttonList[fichaVecina4].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 2:
                        if (buttonList[fichaVecina6].text == "")
                        {
                            buttonList[fichaVecina6].text = enemySide;
                            asignado = true;
                        }
                        break;
                }

                maxAttempts--;

            } while (!asignado && maxAttempts > 0);
        }
        else if (botonConFichaEnemy.name == "boton4")
        {
            System.Random random = new System.Random();

            int maxAttempts = 3; // Número máximo de intentos

            //fichas vecinas 0,1,2,3,5,6,7,8
            int fichaVecina0 = 0;
            int fichaVecina1 = 1;
            int fichaVecina2 = 2;
            int fichaVecina3 = 3;
            int fichaVecina5 = 5;
            int fichaVecina6 = 6;
            int fichaVecina7 = 7;
            int fichaVecina8 = 8;


            bool asignado = false;
            //como son 8 posibilidades
            randomValue = random.Next(0, 9);

            //el valor que haya salido es el boton al que haremos ficha enemySide
            switch (randomValue)
            {
                //fichaVecina0
                case 0:
                    if (buttonList[fichaVecina0].text == "")
                    {
                        buttonList[fichaVecina0].text = enemySide;
                        asignado = true;
                    }
                    break;

                //fichaVecina1
                case 1:
                    if (buttonList[fichaVecina1].text == "")
                    {
                        buttonList[fichaVecina1].text = enemySide;
                        asignado = true;
                    }
                    break;

                //fichaVecina2
                case 2:
                    if (buttonList[fichaVecina2].text == "")
                    {
                        buttonList[fichaVecina2].text = enemySide;
                        asignado = true;
                    }
                    break;

                //fichaVecina3
                case 3:
                    if (buttonList[fichaVecina3].text == "")
                    {
                        buttonList[fichaVecina3].text = enemySide;
                        asignado = true;
                    }
                    break;

                //fichaVecina5
                case 5:
                    if (buttonList[fichaVecina5].text == "")
                    {
                        buttonList[fichaVecina5].text = enemySide;
                        asignado = true;
                    }
                    break;

                //fichaVecina6
                case 6:
                    if (buttonList[fichaVecina6].text == "")
                    {
                        buttonList[fichaVecina6].text = enemySide;
                        asignado = true;
                    }
                    break;

                //fichaVecina7
                case 7:
                    if (buttonList[fichaVecina7].text == "")
                    {
                        buttonList[fichaVecina7].text = enemySide;
                        asignado = true;
                    }
                    break;

                //fichaVecina8
                case 8:
                    if (buttonList[fichaVecina8].text == "")
                    {
                        buttonList[fichaVecina8].text = enemySide;
                        asignado = true;
                    }
                    break;

            }
        }
        else if (botonConFichaEnemy.name == "boton5")
        {
            System.Random random = new System.Random();

            int maxAttempts = 3; // Número máximo de intentos

            //vecinas fichas
            int fichaVecina2 = 2;
            int fichaVecina4 = 4;
            int fichaVecina8 = 8;

            bool asignado = false;

            do
            {
                // Genera un valor aleatorio entre 0 y 2
                randomValue = random.Next(0, 3);

                switch (randomValue)
                {
                    case 0:
                        if (buttonList[fichaVecina2].text == "")
                        {
                            buttonList[fichaVecina2].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 1:
                        if (buttonList[fichaVecina4].text == "")
                        {
                            buttonList[fichaVecina4].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 2:
                        if (buttonList[fichaVecina8].text == "")
                        {
                            buttonList[fichaVecina8].text = enemySide;
                            asignado = true;
                        }
                        break;
                }

                maxAttempts--;

            } while (!asignado && maxAttempts > 0);

        }
        else if (botonConFichaEnemy.name == "boton6")
        {
            System.Random random = new System.Random();

            int maxAttempts = 3; // Número máximo de intentos

            //vecinas fichas
            int fichaVecina3 = 3;
            int fichaVecina4 = 4;
            int fichaVecina7 = 7;

            bool asignado = false;

            do
            {
                // Genera un valor aleatorio entre 0 y 2
                randomValue = random.Next(0, 3);

                switch (randomValue)
                {
                    case 0:
                        if (buttonList[fichaVecina3].text == "")
                        {
                            buttonList[fichaVecina3].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 1:
                        if (buttonList[fichaVecina4].text == "")
                        {
                            buttonList[fichaVecina4].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 2:
                        if (buttonList[fichaVecina7].text == "")
                        {
                            buttonList[fichaVecina7].text = enemySide;
                            asignado = true;
                        }
                        break;
                }

                maxAttempts--;

            } while (!asignado && maxAttempts > 0);
        }
        else if (botonConFichaEnemy.name == "boton7")
        {
            System.Random random = new System.Random();

            int maxAttempts = 3; // Número máximo de intentos

            //vecinas fichas
            int fichaVecina6 = 6;
            int fichaVecina4 = 4;
            int fichaVecina8 = 8;

            bool asignado = false;

            do
            {
                // Genera un valor aleatorio entre 0 y 2
                randomValue = random.Next(0, 3);

                switch (randomValue)
                {
                    case 0:
                        if (buttonList[fichaVecina6].text == "")
                        {
                            buttonList[fichaVecina6].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 1:
                        if (buttonList[fichaVecina4].text == "")
                        {
                            buttonList[fichaVecina4].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 2:
                        if (buttonList[fichaVecina8].text == "")
                        {
                            buttonList[fichaVecina8].text = enemySide;
                            asignado = true;
                        }
                        break;
                }

                maxAttempts--;

            } while (!asignado && maxAttempts > 0);
        }
        else if (botonConFichaEnemy.name == "boton8")
        {
            System.Random random = new System.Random();

            int maxAttempts = 3; // Número máximo de intentos

            //vecinas fichas
            int fichaVecina4 = 4;
            int fichaVecina5 = 5;
            int fichaVecina7 = 7;

            bool asignado = false;

            do
            {
                // Genera un valor aleatorio entre 0 y 2
                randomValue = random.Next(0, 3);

                switch (randomValue)
                {
                    case 0:
                        if (buttonList[fichaVecina4].text == "")
                        {
                            buttonList[fichaVecina4].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 1:
                        if (buttonList[fichaVecina5].text == "")
                        {
                            buttonList[fichaVecina5].text = enemySide;
                            asignado = true;
                        }
                        break;
                    case 2:
                        if (buttonList[fichaVecina7].text == "")
                        {
                            buttonList[fichaVecina7].text = enemySide;
                            asignado = true;
                        }
                        break;
                }

                maxAttempts--;

            } while (!asignado && maxAttempts > 0);
        }

    }

    public GameObject DevolverFichaEnemyColocadaEnTablero()
    {
        System.Random random = new System.Random();
        int intentos = 0;
        int cantidadPosiciones = buttonList.Length;
        bool[] posicionesUsadas = new bool[cantidadPosiciones];

        while (intentos < cantidadPosiciones)
        {
            int posicionAleatoria = random.Next(0, cantidadPosiciones);

            if (!posicionesUsadas[posicionAleatoria])
            {
                posicionesUsadas[posicionAleatoria] = true;

                if (buttonList[posicionAleatoria].text == enemySide)
                {
                    return buttonList[posicionAleatoria].transform.parent.gameObject;
                }

                intentos++;
            }
        }

        return null;
    }

    //colocar ficha en la primera posicion del array posicionVictoriaEnemy
    public void ColocarFichaEnemyCombinacion3()
    {
        //colocamos ficha enemy en la primera posicion del array posicionVictoriaEnemy

       
            //ya tenemos numero de boton 
            int botonElegido = posicionesVictoriaEnemy[contadorPosicionesVictoriaEnemy] - 1;
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

    //buscar pareja enemies 2s 00
    public void Combinacion200()
    {
        //comprobamos el buttonList para ver cuales no estan vacios o sin jugar
        for (int i = 0; i < buttonList.Length; i++)
        {
            //por cada boton que no esté vacio y tenga 0 buscamos en sus laterales o diagonales
            if (buttonList[i].text != "" && buttonList[i].text == "0")
            {
                BuscarParejaCombinacion(buttonList[i]);
            }
        }
    }

    public void ReiniciarContador()
    {
        contadorPosicionesVictoriaEnemy = 0;
    }

    //mira en laterales,diagonales y todo
    public void BuscarParejaCombinacion(TMP_Text botonElegido)
    {
        //miramos si es buttonList[0] ya que sus casillas vecinas serán unas
        if(botonElegido == buttonList[0])
        {
            ////puede hacer pareja con el 1,3 y 4 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(1, 3, 4, botonElegido);

            ////puede hacer pareja con el 1,3 y 4 y pasas referencia con enemySide
            //MirarCasillasAdyacentes3Enemy(1, 3, 4, botonElegido);
            //if(gameSide ==playerSide)
            //{
            //    //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
            //    //MirarCasillasAdyacentesYColocarTerceraPlayer(1, 3, 4, 2, 6, 8, botonElegido);
            //}
            //else if(gameSide == enemySide)
            //{
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(1, 3, 4, 2, 6, 8, botonElegido);
            //}
            
        }
        //miramos si es buttonList[1] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[1])
        {
            ////puede hacer pareja con el 0,2,4 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(0, 2, 4, botonElegido);

            ////puede hacer pareja con el 1,3 y 4 y pasas referencia con enemySide
            //MirarCasillasAdyacentes3Enemy(0, 2, 4, botonElegido);

            //if (gameSide == playerSide)
            //{
            //    //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
            //    //MirarCasillasAdyacentesYColocarTerceraPlayer(0, 2, 4, 2, 0, 7, botonElegido);
            //}
            //else if(gameSide == enemySide)
            //{
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(0, 2, 4, 2, 0, 7, botonElegido);
            //}
            
        }
        //miramos si es buttonList[2] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[2])
        {
            ////puede hacer pareja con el 1,4,5 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(1, 4, 5, botonElegido);

            ////puede hacer pareja con el 1,3 y 4 y pasas referencia con enemySide
            //MirarCasillasAdyacentes3Enemy(1, 4, 5, botonElegido);

            //if (gameSide == playerSide)
            //{
            //    //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
            //    //MirarCasillasAdyacentesYColocarTerceraPlayer(1, 4, 5, 0, 6, 8, botonElegido);
            //}
            //else if (gameSide == enemySide)
            //{
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(1, 4, 5, 0, 6, 8, botonElegido);
            //}

        }
        //miramos si es buttonList[3] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[3])
        {
            ////puede hacer pareja con el 0,4,6 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(0, 4 , 6, botonElegido);

            ////puede hacer pareja con el 1,3 y 4 y pasas referencia con enemySide
            //MirarCasillasAdyacentes3Enemy(0, 4, 6, botonElegido);

            //if (gameSide == playerSide)
            //{
            //    //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
            //    //MirarCasillasAdyacentesYColocarTerceraPlayer(0, 4, 6, 6, 5, 0, botonElegido);
            //}
            //else if (gameSide == enemySide)
            //{
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(0, 4, 6, 6, 5, 0, botonElegido);
            //}

        }
        //miramos si es buttonList[4] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[4])
        {
            ////como está en el centro el 4 puede hacer pareja con todos así que comprobamos
            //MirarCasillasAdyacentes8Player(botonElegido);

            ////como está en el centro el 4 puede hacer pareja con todos así que comprobamos
            //MirarCasillasAdyacentes8Enemy(botonElegido);

            //if (gameSide == playerSide)
            //{
            //    //MirarCasillasAdyacentes8YColocarTerceraPlayer(botonElegido);
            //}
            //else if (gameSide == enemySide)
            //{
                MirarCasillasAdyacentes8YColocarTerceraEnemy(botonElegido);
            //}

        }
        //miramos si es buttonList[5] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[5])
        {
            ////puede hacer pareja con el 2,4,8 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(2,4,8, botonElegido);

            ////puede hacer pareja con el 2,4,8 y pasas referencia con enemySide
            //MirarCasillasAdyacentes3Enemy(2,4,8, botonElegido);

            //if (gameSide == playerSide)
            //{
            //    //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
            //    //MirarCasillasAdyacentesYColocarTerceraPlayer(2, 4, 8, 8, 3, 2, botonElegido);
            //}
            //else if (gameSide == enemySide)
            //{
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(2, 4, 8, 8, 3, 2, botonElegido);
            //}

        }
        //miramos si es buttonList[6] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[6])
        {
            ////puede hacer pareja con el 3,4,7 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(3,4,7, botonElegido);

            //MirarCasillasAdyacentes3Enemy(3, 4, 7, botonElegido);

            //if (gameSide == playerSide)
            //{
            //    //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
            //    //MirarCasillasAdyacentesYColocarTerceraPlayer(3, 4, 7, 0, 2, 8, botonElegido);
            //}
            //else if (gameSide == enemySide)
            //{
                MirarCasillasAdyacentesYColocarTerceraEnemy(3, 4, 7, 0, 2, 8, botonElegido);
            //}

            
        }
        //miramos si es buttonList[7] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[7])
        {
            ////puede hacer pareja con el 6,4,8 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(6,4,8, botonElegido);

            //MirarCasillasAdyacentes3Enemy(6,4,8, botonElegido);

            //if (gameSide == playerSide)
            //{
            //    //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
            //    //MirarCasillasAdyacentesYColocarTerceraPlayer(6, 4, 8, 8, 1, 6, botonElegido);
            //}
            //else if (gameSide == enemySide)
            //{
                MirarCasillasAdyacentesYColocarTerceraEnemy(6, 4, 8, 8, 1, 6, botonElegido);
            //}
            
        }
        //miramos si es buttonList[8] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[8])
        {
            ////puede hacer pareja con el 7,4,5 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(7,4,5, botonElegido);

            //MirarCasillasAdyacentes3Enemy(7,4,5, botonElegido);

            //if (gameSide == playerSide)
            //{
            //    //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
            //    //MirarCasillasAdyacentesYColocarTerceraPlayer(7, 4, 5, 6, 0, 2, botonElegido);
            //}
            //else if (gameSide == enemySide)
            //{
                MirarCasillasAdyacentesYColocarTerceraEnemy(7, 4, 5, 6, 0, 2, botonElegido);
            //}
           
        }
       
    }


    # region mirarCasillasAdyacentes3
    //aquellos botones que tengan 3 casillas vecinas y mirar combinaciones X
    public void MirarCasillasAdyacentes3Player(int posibilidadCombinacion1, int posibilidadCombinacion2, int posibilidadCombinacion3, TMP_Text botonElegido)
    {
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion1
        if(botonElegido.text =="X" && buttonList[posibilidadCombinacion1].text == "X" && botonElegido.text == buttonList[posibilidadCombinacion1].text)
        {
            Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido.transform.parent.gameObject.name + "y" + buttonList[posibilidadCombinacion1].transform.parent.gameObject.name);
        }

        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion2
        if (botonElegido.text == "X" && buttonList[posibilidadCombinacion1].text == "X" && botonElegido.text == buttonList[posibilidadCombinacion2].text)
        {
            Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido.transform.parent.gameObject.name + "y" + buttonList[posibilidadCombinacion2].transform.parent.gameObject.name);
        }

        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion3
        if (botonElegido.text == "X" && buttonList[posibilidadCombinacion1].text == "X" && botonElegido.text == buttonList[posibilidadCombinacion3].text)
        {
            Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido.transform.parent.gameObject.name + "y" + buttonList[posibilidadCombinacion3].transform.parent.gameObject.name);
        }
    }

    //aquellos botones que tengan 3 casillas vecinas y mirar combinaciones 0
    public void MirarCasillasAdyacentes3Enemy(int posibilidadCombinacion1, int posibilidadCombinacion2, int posibilidadCombinacion3, TMP_Text botonElegido)
    {
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion1
        if (botonElegido.text == "0" && buttonList[posibilidadCombinacion1].text == "0" && botonElegido.text == buttonList[posibilidadCombinacion1].text)
        {
            Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido.transform.parent.gameObject.name + "y" + buttonList[posibilidadCombinacion1].transform.parent.gameObject.name);
        }

        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion2
        if (botonElegido.text == "0" && buttonList[posibilidadCombinacion1].text == "0" && botonElegido.text == buttonList[posibilidadCombinacion2].text)
        {
            Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido.transform.parent.gameObject.name + "y" + buttonList[posibilidadCombinacion2].transform.parent.gameObject.name);
        }

        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion3
        if (botonElegido.text == "0" && buttonList[posibilidadCombinacion1].text == "0" && botonElegido.text == buttonList[posibilidadCombinacion3].text)
        {
            Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido.transform.parent.gameObject.name + "y" + buttonList[posibilidadCombinacion3].transform.parent.gameObject.name);
        }
    }

    #endregion

    #region mirarCasillasAdyacentes8

    //aquellos botones que tengan todas las casillas vecinas como es el 4 que esta en el centro
    public void MirarCasillasAdyacentes8Player(TMP_Text botonElegido4)
    {

        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[0].text =="X" && botonElegido4.text == buttonList[0].text)
        {
            //Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[0].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[1].text == "X" && botonElegido4.text == buttonList[1].text)
        {
            //Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[1].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[2].text == "X" && botonElegido4.text == buttonList[2].text)
        {
            //Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[2].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[3].text == "X" && botonElegido4.text == buttonList[3].text)
        {
            //Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[3].transform.parent.gameObject.name);
        }
        
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[5].text == "X" && botonElegido4.text == buttonList[5].text)
        {
            //Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[5].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[6].text == "X" && botonElegido4.text == buttonList[6].text)
        {
            //Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[6].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[7].text == "X" && botonElegido4.text == buttonList[7].text)
        {
            //Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[7].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[8].text == "X" && botonElegido4.text == buttonList[8].text)
        {
            //Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[8].transform.parent.gameObject.name);
        }

    }

    public void MirarCasillasAdyacentes8Enemy(TMP_Text botonElegido4)
    {

        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[0].text == "0" && botonElegido4.text == buttonList[0].text)
        {
            //Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[0].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[1].text == "0" && botonElegido4.text == buttonList[1].text)
        {
            //Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[1].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[2].text == "0" && botonElegido4.text == buttonList[2].text)
        {
            //Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[2].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[3].text == "0" && botonElegido4.text == buttonList[3].text)
        {
            //Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[3].transform.parent.gameObject.name);
        }

        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[5].text == "0" && botonElegido4.text == buttonList[5].text)
        {
            //Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[5].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[6].text == "0" && botonElegido4.text == buttonList[6].text)
        {
            //Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[6].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[7].text == "0" && botonElegido4.text == buttonList[7].text)
        {
            //Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[7].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[8].text == "0" && botonElegido4.text == buttonList[8].text)
        {
            //Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[8].transform.parent.gameObject.name);
        }

    }

    #endregion

    #region AveriguarPosicionVictoria

    public void MirarCasillasAdyacentesYColocarTerceraPlayer(int vecino1, int vecino2, int vecino3, int ultimaCasillaTriovecino1, int ultimaCasillaTriovecino2, int ultimaCasillaTriovecino3, TMP_Text botonElegido)
    {
        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        if (botonElegido.text == "X" && buttonList[vecino1].text == "X" && botonElegido.text == buttonList[vecino1].text && buttonList[ultimaCasillaTriovecino1].text == "")
        {
            //Debug.Log("Si colocas la ficha " + playerSide + " en " + buttonList[ultimaCasillaTriovecino1].transform.parent.gameObject.name + " ganas, y haces trio");
            
        }

        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        if (botonElegido.text == "X" && buttonList[vecino2].text == "X" && botonElegido.text == buttonList[vecino2].text && buttonList[ultimaCasillaTriovecino2].text == "")
        {
            //Debug.Log("Si colocas la ficha " + playerSide + " en " + buttonList[ultimaCasillaTriovecino2].transform.parent.gameObject.name + " ganas, y haces trio");
           
        }

        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        if (botonElegido.text == "X" && buttonList[vecino3].text == "X" && botonElegido.text == buttonList[vecino3].text && buttonList[ultimaCasillaTriovecino3].text == "")
        {
            //Debug.Log("Si colocas la ficha " + playerSide + " en " + buttonList[ultimaCasillaTriovecino3].transform.parent.gameObject.name + " ganas, y haces trio");
            
        }
    }

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
        if (botonElegido.text == "0" && buttonList[vecino2].text == "0" && botonElegido.text == buttonList[vecino2].text && buttonList[ultimaCasillaTriovecino2].text == "")
        {
            //Debug.Log("Si colocas la ficha " + enemySide + " en " + buttonList[ultimaCasillaTriovecino2].transform.parent.gameObject.name + " ganas, y haces trio");

            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[ultimaCasillaTriovecino2].transform.parent.gameObject.name);
        }

        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        if (botonElegido.text == "0" && buttonList[vecino3].text == "0" && botonElegido.text == buttonList[vecino3].text && buttonList[ultimaCasillaTriovecino3].text == "")
        {
            //Debug.Log("Si colocas la ficha " + enemySide + " en " + buttonList[ultimaCasillaTriovecino3].transform.parent.gameObject.name + " ganas, y haces trio");

            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[ultimaCasillaTriovecino3].transform.parent.gameObject.name);
        }
    }

    //para el boton 4 con todas las casillas vecinas y colocar la ficha vencedora
    public void MirarCasillasAdyacentes8YColocarTerceraPlayer(TMP_Text botonElegido4)
    {

        //vemos si hay un duo entre el boton 4 y el boton 0 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 0 y 4 seria 8
        if (botonElegido4.text == "X" && buttonList[0].text == "X" && botonElegido4.text == buttonList[0].text && buttonList[8].text == "")
        {
            //Debug.Log("Si colocas la ficha en "+ buttonList[8].transform.parent.gameObject.name + " ganas, y haces trio");

        }

        //vemos si hay un duo entre el boton 4 y el boton 1 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 1 y 4 seria 7
        if (botonElegido4.text == "X" && buttonList[1].text == "X" && botonElegido4.text == buttonList[1].text && buttonList[7].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 2 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 2 y 4 seria 6
        if (botonElegido4.text == "X" && buttonList[2].text == "X" && botonElegido4.text == buttonList[2].text && buttonList[6].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[6].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 3 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 3 y 4 seria 5
        if (botonElegido4.text == "X" && buttonList[3].text == "X" && botonElegido4.text == buttonList[3].text && buttonList[5].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[5].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 5 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 5 y 4 seria 3
        if (botonElegido4.text == "X" && buttonList[5].text == "X" && botonElegido4.text == buttonList[5].text && buttonList[3].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 6 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 6 y 4 seria 2
        if (botonElegido4.text == "X" && buttonList[6].text == "X" && botonElegido4.text == buttonList[6].text && buttonList[2].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[2].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 7 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 7 y 4 seria 1
        if (botonElegido4.text == "X" && buttonList[7].text == "X" && botonElegido4.text == buttonList[7].text && buttonList[1].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[1].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 8 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 8 y 4 seria 0
        if (botonElegido4.text == "X" && buttonList[8].text == "X" && botonElegido4.text == buttonList[8].text && buttonList[0].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[0].transform.parent.gameObject.name + " ganas, y haces trio");
        }

    }

    //para el boton 4 con todas las casillas vecinas y colocar la ficha vencedora
    public void MirarCasillasAdyacentes8YColocarTerceraEnemy(TMP_Text botonElegido4)
    {

        //vemos si hay un duo entre el boton 4 y el boton 0 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 0 y 4 seria 8
        if (botonElegido4.text == "0" && buttonList[0].text == "0" && botonElegido4.text == buttonList[0].text && buttonList[8].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[8].transform.parent.gameObject.name + " ganas, y haces trio");
            
            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[8].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 1 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 1 y 4 seria 7
        if (botonElegido4.text == "0" && buttonList[1].text == "0" && botonElegido4.text == buttonList[1].text && buttonList[7].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[7].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 2 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 2 y 4 seria 6
        if (botonElegido4.text == "0" && buttonList[2].text == "0" && botonElegido4.text == buttonList[2].text && buttonList[6].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[6].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[6].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 3 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 3 y 4 seria 5
        if (botonElegido4.text == "0" && buttonList[3].text == "0" && botonElegido4.text == buttonList[3].text && buttonList[5].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[5].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[5].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 5 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 5 y 4 seria 3
        if (botonElegido4.text == "0" && buttonList[5].text == "0" && botonElegido4.text == buttonList[5].text && buttonList[3].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[3].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 6 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 6 y 4 seria 2
        if (botonElegido4.text == "0" && buttonList[6].text == "0" && botonElegido4.text == buttonList[6].text && buttonList[2].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[2].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[2].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 7 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 7 y 4 seria 1
        if (botonElegido4.text == "0" && buttonList[7].text == "0" && botonElegido4.text == buttonList[7].text && buttonList[1].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[1].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[1].transform.parent.gameObject.name);
        }

        //vemos si hay un duo entre el boton 4 y el boton 8 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 8 y 4 seria 0
        if (botonElegido4.text == "0" && buttonList[8].text == "0" && botonElegido4.text == buttonList[8].text && buttonList[0].text == "")
        {
            //Debug.Log("Si colocas la ficha en " + buttonList[0].transform.parent.gameObject.name + " ganas, y haces trio");


            //añadimos posicion victoria enemy
            NuevaPosicionVictoriaEnemy(buttonList[0].transform.parent.gameObject.name);
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
        }
        contadorPosicionesVictoriaEnemy = 0;
    }

    #endregion

    //para decir si ha ganado
    void GameOver()
    {
        SetBoardInteractable(false);

        gameover.SetActive(true);
        gameoverText.text = gameSide + " has ganado";
    }

    void ChangeSides()
    {
        gameSide = (gameSide == playerSide) ? "0" : "X";

       
    }

    void TurnoJugadorCanvas()
    {
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
            //si es el turno del enemigo, hace jugada
            EnemyTurn();

        }
        //si playerSide es "Y" activamos playerY en pantalla
    }

    public void RestartGame()
    {
        gameSide = enemySide;
        moveCount = 0;
        gameover.SetActive(false);

        SetBoardInteractable(true);

        for (int i = 0; i < buttonList.Length; i++)
        {
            
            buttonList[i].text = "";
        }
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }
}
