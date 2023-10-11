using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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


    private void Awake()
    {
        gameover.SetActive(false);
        SetGameControllerReferenceOnButtons();
        //siempre empieza el jugador
        gameSide = playerSide;
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

    public void Combinacion200XX()
    {
        //comprobamos el buttonList para ver cuales no estan vacios o sin jugar
        for (int i = 0; i < buttonList.Length; i++)
        {
            //por cada boton que no esté vacio buscamos en sus laterales o diagonales
            if (buttonList[i].text != "")
            {
                BuscarParejaCombinacion(buttonList[i]);
            }
        }
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
            if(gameSide ==playerSide)
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraPlayer(1, 3, 4, 2, 6, 8, botonElegido);
            }
            else if(gameSide == enemySide)
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(1, 3, 4, 2, 6, 8, botonElegido);
            }
            
        }
        //miramos si es buttonList[1] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[1])
        {
            ////puede hacer pareja con el 0,2,4 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(0, 2, 4, botonElegido);

            ////puede hacer pareja con el 1,3 y 4 y pasas referencia con enemySide
            //MirarCasillasAdyacentes3Enemy(0, 2, 4, botonElegido);

            if (gameSide == playerSide)
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraPlayer(0, 2, 4, 2, 0, 7, botonElegido);
            }
            else if(gameSide == enemySide)
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(0, 2, 4, 2, 0, 7, botonElegido);
            }
            
        }
        //miramos si es buttonList[2] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[2])
        {
            ////puede hacer pareja con el 1,4,5 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(1, 4, 5, botonElegido);

            ////puede hacer pareja con el 1,3 y 4 y pasas referencia con enemySide
            //MirarCasillasAdyacentes3Enemy(1, 4, 5, botonElegido);

            if (gameSide == playerSide)
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraPlayer(1, 4, 5, 0, 6, 8, botonElegido);
            }
            else if (gameSide == enemySide)
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(1, 4, 5, 0, 6, 8, botonElegido);
            }

        }
        //miramos si es buttonList[3] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[3])
        {
            ////puede hacer pareja con el 0,4,6 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(0, 4 , 6, botonElegido);

            ////puede hacer pareja con el 1,3 y 4 y pasas referencia con enemySide
            //MirarCasillasAdyacentes3Enemy(0, 4, 6, botonElegido);

            if (gameSide == playerSide)
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraPlayer(0, 4, 6, 6, 5, 0, botonElegido);
            }
            else if (gameSide == enemySide)
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(0, 4, 6, 6, 5, 0, botonElegido);
            }

        }
        //miramos si es buttonList[4] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[4])
        {
            ////como está en el centro el 4 puede hacer pareja con todos así que comprobamos
            //MirarCasillasAdyacentes8Player(botonElegido);

            ////como está en el centro el 4 puede hacer pareja con todos así que comprobamos
            //MirarCasillasAdyacentes8Enemy(botonElegido);

            if (gameSide == playerSide)
            {
                MirarCasillasAdyacentes8YColocarTerceraPlayer(botonElegido);
            }
            else if (gameSide == enemySide)
            {
                MirarCasillasAdyacentes8YColocarTerceraEnemy(botonElegido);
            }

        }
        //miramos si es buttonList[5] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[5])
        {
            ////puede hacer pareja con el 2,4,8 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(2,4,8, botonElegido);

            ////puede hacer pareja con el 2,4,8 y pasas referencia con enemySide
            //MirarCasillasAdyacentes3Enemy(2,4,8, botonElegido);

            if (gameSide == playerSide)
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraPlayer(2, 4, 8, 8, 3, 2, botonElegido);
            }
            else if (gameSide == enemySide)
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraEnemy(2, 4, 8, 8, 3, 2, botonElegido);
            }

        }
        //miramos si es buttonList[6] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[6])
        {
            ////puede hacer pareja con el 3,4,7 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(3,4,7, botonElegido);

            //MirarCasillasAdyacentes3Enemy(3, 4, 7, botonElegido);

            if (gameSide == playerSide)
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraPlayer(3, 4, 7, 0, 2, 8, botonElegido);
            }
            else if (gameSide == enemySide)
            {
                MirarCasillasAdyacentesYColocarTerceraEnemy(3, 4, 7, 0, 2, 8, botonElegido);
            }

            
        }
        //miramos si es buttonList[7] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[7])
        {
            ////puede hacer pareja con el 6,4,8 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(6,4,8, botonElegido);

            //MirarCasillasAdyacentes3Enemy(6,4,8, botonElegido);

            if (gameSide == playerSide)
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraPlayer(6, 4, 8, 8, 1, 6, botonElegido);
            }
            else if (gameSide == enemySide)
            {
                MirarCasillasAdyacentesYColocarTerceraEnemy(6, 4, 8, 8, 1, 6, botonElegido);
            }
            
        }
        //miramos si es buttonList[8] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[8])
        {
            ////puede hacer pareja con el 7,4,5 y pasas referencia con playerSide
            //MirarCasillasAdyacentes3Player(7,4,5, botonElegido);

            //MirarCasillasAdyacentes3Enemy(7,4,5, botonElegido);

            if (gameSide == playerSide)
            {
                //metodo que mira si en las casillas vecinas hay 2 fichas iguales y te dice donde deberías colocar la tercera
                MirarCasillasAdyacentesYColocarTerceraPlayer(7, 4, 5, 6, 0, 2, botonElegido);
            }
            else if (gameSide == enemySide)
            {
                MirarCasillasAdyacentesYColocarTerceraEnemy(7, 4, 5, 6, 0, 2, botonElegido);
            }
           
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
            Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[0].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[1].text == "X" && botonElegido4.text == buttonList[1].text)
        {
            Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[1].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[2].text == "X" && botonElegido4.text == buttonList[2].text)
        {
            Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[2].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[3].text == "X" && botonElegido4.text == buttonList[3].text)
        {
            Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[3].transform.parent.gameObject.name);
        }
        
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[5].text == "X" && botonElegido4.text == buttonList[5].text)
        {
            Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[5].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[6].text == "X" && botonElegido4.text == buttonList[6].text)
        {
            Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[6].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[7].text == "X" && botonElegido4.text == buttonList[7].text)
        {
            Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[7].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "X" && buttonList[8].text == "X" && botonElegido4.text == buttonList[8].text)
        {
            Debug.Log("Tienes una pareja de: " + playerSide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[8].transform.parent.gameObject.name);
        }

    }

    public void MirarCasillasAdyacentes8Enemy(TMP_Text botonElegido4)
    {

        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[0].text == "0" && botonElegido4.text == buttonList[0].text)
        {
            Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[0].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[1].text == "0" && botonElegido4.text == buttonList[1].text)
        {
            Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[1].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[2].text == "0" && botonElegido4.text == buttonList[2].text)
        {
            Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[2].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[3].text == "0" && botonElegido4.text == buttonList[3].text)
        {
            Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[3].transform.parent.gameObject.name);
        }

        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[5].text == "0" && botonElegido4.text == buttonList[5].text)
        {
            Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[5].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[6].text == "0" && botonElegido4.text == buttonList[6].text)
        {
            Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[6].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[7].text == "0" && botonElegido4.text == buttonList[7].text)
        {
            Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[7].transform.parent.gameObject.name);
        }
        //comprobamos las combinaciones y si coinciden las parejas de 2 con posibilidadCombinacion4
        if (botonElegido4.text == "0" && buttonList[8].text == "0" && botonElegido4.text == buttonList[8].text)
        {
            Debug.Log("Tienes una pareja de: " + enemySide + " en " + botonElegido4.transform.parent.gameObject.name + "y" + buttonList[8].transform.parent.gameObject.name);
        }

    }

    #endregion

    #region AveriguarPosicionVictoria

    public void MirarCasillasAdyacentesYColocarTerceraPlayer(int vecino1, int vecino2, int vecino3, int ultimaCasillaTriovecino1, int ultimaCasillaTriovecino2, int ultimaCasillaTriovecino3, TMP_Text botonElegido)
    {
        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        if (botonElegido.text == "X" && buttonList[vecino1].text == "X" && botonElegido.text == buttonList[vecino1].text && buttonList[ultimaCasillaTriovecino1].text == "")
        {
            Debug.Log("Si colocas la ficha " + playerSide + " en " + buttonList[ultimaCasillaTriovecino1].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        if (botonElegido.text == "X" && buttonList[vecino2].text == "X" && botonElegido.text == buttonList[vecino2].text && buttonList[ultimaCasillaTriovecino2].text == "")
        {
            Debug.Log("Si colocas la ficha " + playerSide + " en " + buttonList[ultimaCasillaTriovecino2].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        if (botonElegido.text == "X" && buttonList[vecino3].text == "X" && botonElegido.text == buttonList[vecino3].text && buttonList[ultimaCasillaTriovecino3].text == "")
        {
            Debug.Log("Si colocas la ficha " + playerSide + " en " + buttonList[ultimaCasillaTriovecino3].transform.parent.gameObject.name + " ganas, y haces trio");
        }
    }

    public void MirarCasillasAdyacentesYColocarTerceraEnemy(int vecino1, int vecino2, int vecino3, int ultimaCasillaTriovecino1, int ultimaCasillaTriovecino2, int ultimaCasillaTriovecino3, TMP_Text botonElegido)
    {
        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        if (botonElegido.text == "0" && buttonList[vecino1].text == "0" && botonElegido.text == buttonList[vecino1].text && buttonList[ultimaCasillaTriovecino1].text == "")
        {
            Debug.Log("Si colocas la ficha " + enemySide + " en " + buttonList[ultimaCasillaTriovecino1].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        if (botonElegido.text == "0" && buttonList[vecino2].text == "0" && botonElegido.text == buttonList[vecino2].text && buttonList[ultimaCasillaTriovecino2].text == "")
        {
            Debug.Log("Si colocas la ficha " + enemySide + " en " + buttonList[ultimaCasillaTriovecino2].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //comprobamos si las fichas vecina 1 tiene misma X y si la ultimacasillaTriovecino1 esto es (donde hay que colocar para ganar está vacía)
        if (botonElegido.text == "0" && buttonList[vecino3].text == "0" && botonElegido.text == buttonList[vecino3].text && buttonList[ultimaCasillaTriovecino3].text == "")
        {
            Debug.Log("Si colocas la ficha " + enemySide + " en " + buttonList[ultimaCasillaTriovecino3].transform.parent.gameObject.name + " ganas, y haces trio");
        }
    }

    //para el boton 4 con todas las casillas vecinas y colocar la ficha vencedora
    public void MirarCasillasAdyacentes8YColocarTerceraPlayer(TMP_Text botonElegido4)
    {

        //vemos si hay un duo entre el boton 4 y el boton 0 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 0 y 4 seria 8
        if (botonElegido4.text == "X" && buttonList[0].text == "X" && botonElegido4.text == buttonList[0].text && buttonList[8].text == "")
        {
            Debug.Log("Si colocas la ficha en "+ buttonList[8].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 1 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 1 y 4 seria 7
        if (botonElegido4.text == "X" && buttonList[1].text == "X" && botonElegido4.text == buttonList[1].text && buttonList[7].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 2 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 2 y 4 seria 6
        if (botonElegido4.text == "X" && buttonList[2].text == "X" && botonElegido4.text == buttonList[2].text && buttonList[6].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[6].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 3 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 3 y 4 seria 5
        if (botonElegido4.text == "X" && buttonList[3].text == "X" && botonElegido4.text == buttonList[3].text && buttonList[5].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[5].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 5 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 5 y 4 seria 3
        if (botonElegido4.text == "X" && buttonList[5].text == "X" && botonElegido4.text == buttonList[5].text && buttonList[3].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 6 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 6 y 4 seria 2
        if (botonElegido4.text == "X" && buttonList[6].text == "X" && botonElegido4.text == buttonList[6].text && buttonList[2].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[2].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 7 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 7 y 4 seria 1
        if (botonElegido4.text == "X" && buttonList[7].text == "X" && botonElegido4.text == buttonList[7].text && buttonList[1].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[1].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 8 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 8 y 4 seria 0
        if (botonElegido4.text == "X" && buttonList[8].text == "X" && botonElegido4.text == buttonList[8].text && buttonList[0].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[0].transform.parent.gameObject.name + " ganas, y haces trio");
        }

    }

    //para el boton 4 con todas las casillas vecinas y colocar la ficha vencedora
    public void MirarCasillasAdyacentes8YColocarTerceraEnemy(TMP_Text botonElegido4)
    {

        //vemos si hay un duo entre el boton 4 y el boton 0 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 0 y 4 seria 8
        if (botonElegido4.text == "0" && buttonList[0].text == "0" && botonElegido4.text == buttonList[0].text && buttonList[8].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[8].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 1 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 1 y 4 seria 7
        if (botonElegido4.text == "0" && buttonList[1].text == "0" && botonElegido4.text == buttonList[1].text && buttonList[7].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 2 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 2 y 4 seria 6
        if (botonElegido4.text == "0" && buttonList[2].text == "0" && botonElegido4.text == buttonList[2].text && buttonList[6].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[6].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 3 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 3 y 4 seria 5
        if (botonElegido4.text == "0" && buttonList[3].text == "0" && botonElegido4.text == buttonList[3].text && buttonList[5].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[5].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 5 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 5 y 4 seria 3
        if (botonElegido4.text == "0" && buttonList[5].text == "0" && botonElegido4.text == buttonList[5].text && buttonList[3].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[7].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 6 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 6 y 4 seria 2
        if (botonElegido4.text == "0" && buttonList[6].text == "0" && botonElegido4.text == buttonList[6].text && buttonList[2].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[2].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 7 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 7 y 4 seria 1
        if (botonElegido4.text == "0" && buttonList[7].text == "0" && botonElegido4.text == buttonList[7].text && buttonList[1].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[1].transform.parent.gameObject.name + " ganas, y haces trio");
        }

        //vemos si hay un duo entre el boton 4 y el boton 8 con X y comprobamos espacio vacio de la casilla que seria la victoria
        //en este caso la casilla de victoria entre 8 y 4 seria 0
        if (botonElegido4.text == "0" && buttonList[8].text == "0" && botonElegido4.text == buttonList[8].text && buttonList[0].text == "")
        {
            Debug.Log("Si colocas la ficha en " + buttonList[0].transform.parent.gameObject.name + " ganas, y haces trio");
        }

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

        }
        //si playerSide es "Y" activamos playerY en pantalla
    }

    public void RestartGame()
    {
        gameSide = "X";
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
