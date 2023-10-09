using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gamecontroller : MonoBehaviour
{
    public TMP_Text[] buttonList;
    private string playerSide;

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
        playerSide = "X";
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

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        moveCount++;
        //diferentes maneras de acabar juego o lineas para hacer 3 en raya

        //1 ---
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver();
        }

        //2 ---
        if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver();
        }

        //3 ---
        if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }

        //4 ---
        if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver();
        }

        //5 ---
        if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver();
        }

        //6 ---
        if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }

        //7 
        if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }

        //8
        if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver();
        }

        if(moveCount >= 9)
        {
            gameover.SetActive(true);
            gameoverText.text = "Turno";
        }

        ChangeSides();



    }

    void GameOver()
    {
        SetBoardInteractable(false);

        gameover.SetActive(true);
        gameoverText.text = playerSide + "Hola!";
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "0" : "X";

       
    }

    void TurnoJugadorCanvas()
    {
        //si playerSide es "X" activamos playerX en pantalla
        if (playerSide == "X")
        {

            playerX.SetActive(true);
            player0.SetActive(false);

        }
        else if (playerSide == "0")
        {
            playerX.SetActive(false);
            player0.SetActive(true);

        }
        //si playerSide es "Y" activamos playerY en pantalla
    }

    public void RestartGame()
    {
        playerSide = "X";
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
