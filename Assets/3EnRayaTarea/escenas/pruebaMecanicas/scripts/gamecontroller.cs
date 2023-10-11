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
            //puede hacer pareja con el 1,3 y 4 y pasas referencia con playerSide
            MirarCasillasAdyacentes3Player(1, 3, 4, botonElegido);

            //puede hacer pareja con el 1,3 y 4 y pasas referencia con enemySide
            MirarCasillasAdyacentes3Enemy(1, 3, 4, botonElegido);
        }
        //miramos si es buttonList[1] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[1])
        {
            //puede hacer pareja con el 0,2,4 y pasas referencia con playerSide
            MirarCasillasAdyacentes3Player(0, 2, 4, botonElegido);

            //puede hacer pareja con el 1,3 y 4 y pasas referencia con enemySide
            MirarCasillasAdyacentes3Enemy(0, 2, 4, botonElegido);
        }
        //miramos si es buttonList[2] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[2])
        {
            //puede hacer pareja con el 1,4,5 y pasas referencia con playerSide
            MirarCasillasAdyacentes3Player(1, 4, 5, botonElegido);

            //puede hacer pareja con el 1,3 y 4 y pasas referencia con enemySide
            MirarCasillasAdyacentes3Enemy(1, 4, 5, botonElegido);
        }
        //miramos si es buttonList[3] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[3])
        {
            //puede hacer pareja con el 0,4,6 y pasas referencia con playerSide
            MirarCasillasAdyacentes3Player(0, 4 , 6, botonElegido);

            //puede hacer pareja con el 1,3 y 4 y pasas referencia con enemySide
            MirarCasillasAdyacentes3Enemy(0, 4, 6, botonElegido);
        }
        //miramos si es buttonList[4] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[4])
        {
            //como está en el centro el 4 puede hacer pareja con todos así que comprobamos
            MirarCasillasAdyacentes8Player(botonElegido);

            //como está en el centro el 4 puede hacer pareja con todos así que comprobamos
            MirarCasillasAdyacentes8Enemy(botonElegido);

        }
        //miramos si es buttonList[5] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[5])
        {
            //puede hacer pareja con el 2,4,8 y pasas referencia con playerSide
            MirarCasillasAdyacentes3Player(2,4,8, botonElegido);

            //puede hacer pareja con el 2,4,8 y pasas referencia con enemySide
            MirarCasillasAdyacentes3Enemy(2,4,8, botonElegido);
        }
        //miramos si es buttonList[6] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[6])
        {
            //puede hacer pareja con el 3,4,7 y pasas referencia con playerSide
            MirarCasillasAdyacentes3Player(3,4,7, botonElegido);

            MirarCasillasAdyacentes3Enemy(3, 4, 7, botonElegido);
        }
        //miramos si es buttonList[7] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[7])
        {
            //puede hacer pareja con el 6,4,8 y pasas referencia con playerSide
            MirarCasillasAdyacentes3Player(6,4,8, botonElegido);

            MirarCasillasAdyacentes3Enemy(6,4,8, botonElegido);
        }
        //miramos si es buttonList[8] ya que sus casillas vecinas serán unas
        if (botonElegido == buttonList[8])
        {
            //puede hacer pareja con el 7,4,5 y pasas referencia con playerSide
            MirarCasillasAdyacentes3Player(7,4,5, botonElegido);

            MirarCasillasAdyacentes3Enemy(7,4,5, botonElegido);
        }
       
    }

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
