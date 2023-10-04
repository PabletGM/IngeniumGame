using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveBackground : MonoBehaviour
{
    public Transform backgroundTransform;
    public float moveSpeed; // Velocidad de movimiento del fondo.
    public float moveDuration; // Duraci�n de la animaci�n en segundos.

    private bool isMoving = false;
    private float startTime;

    [SerializeField] private GameObject dialogoRobot;
    [SerializeField] private GameObject robot;
    [SerializeField] private GameObject playGame;

    private float duracionAnimacion = 1.2f;

    private void Start()
    {
        // Al inicio, aseg�rate de que el fondo no se est� moviendo.
        isMoving = false;
        
    }

    private void Update()
    {
        // Si el fondo est� en movimiento, actualiza su posici�n.
        if (isMoving)
        {
            float elapsedTime = Time.time - startTime;
            if (elapsedTime < moveDuration)
            {
                float moveDistance = moveSpeed * Time.deltaTime;
                backgroundTransform.Translate(Vector3.left * moveDistance);
            }
            else
            {
                // Det�n el movimiento despu�s de la duraci�n especificada.
                isMoving = false;
                //inicias robot
                robot.SetActive(true);
                //inicias texto que explica lo del criosue�o
                Invoke("SetDialogoRobotActivar",duracionAnimacion);
            }
        }
    }

    //activar dialogo robot
    private void SetDialogoRobotActivar()
    {
        dialogoRobot.SetActive(true);
        //activamos PlayGame
        playGame.SetActive(true);
    }

    public void StartMove()
    {
        // Comienza el movimiento cuando se llama a este m�todo.
        if (!isMoving)
        {
            startTime = Time.time;
            isMoving = true;
        }
    }

    public void EmpezarGameCirculos()
    {
        SceneManager.LoadScene("PanelRadar");
    }
}

