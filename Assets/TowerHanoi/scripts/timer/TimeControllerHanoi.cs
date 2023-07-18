using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControllerHanoi: MonoBehaviour
{
    private float restante;
    private bool enMarcha = false;

    [SerializeField]
    private int tiempoMaximo;

    GameManagerHanoi _myGameManagerHanoi;

    private void Awake()
    {
        restante = 0;
    }
    private void Start()
    {
        _myGameManagerHanoi = GameManagerHanoi.GetInstance();
    }

    public void ActivarTimer()
    {
        enMarcha = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (enMarcha)
        {
            //va sumando segundos
            restante += Time.deltaTime;
            //informamos del tiempo al gameManager en cada segundo
            InformarTimeGameManager();
            //en caso de superar el tiempo maximo 90 segundos
            if (restante > tiempoMaximo)
            {
                enMarcha = false;
            }
            //minutos que llevamos
            int tempMin = Mathf.FloorToInt(restante / 60);
            //segundos
            int tempSeg = Mathf.FloorToInt(restante % 60);

        }
    }

    public void InformarTimeGameManager()
    {
        _myGameManagerHanoi.SetTiempoTotalHanoiRegistrado((int)restante);
    }
}
