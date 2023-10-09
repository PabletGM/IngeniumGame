using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GripSpace : MonoBehaviour
{
    public Button button;
    public TMP_Text buttonText;
    public string playerSide;

    private gamecontroller GameController;

    public void SetSpace()
    {
        buttonText.text = GameController.GetPlayerSide();
        button.interactable = false;
        GameController.EndTurn();
    }

    public void SetGameControllerReference(gamecontroller controller)
    {
        GameController = controller;
    }
}
