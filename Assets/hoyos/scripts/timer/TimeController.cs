using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{

    [SerializeField] int min, seg;
    [SerializeField] Text tiempo;

    private float restante;
    private bool enMarcha = false;

    [SerializeField]
    private int tiempoMaximo;

    private void Awake()
    {
        restante = 0;
    }

    public void ActivarTimer()
    {
        enMarcha = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(enMarcha)
        {
            //va sumando segundos
            restante += Time.deltaTime;
            //en caso de superar el tiempo maximo 90 segundos
            if(restante >tiempoMaximo)
            {
                enMarcha = false;
            }
            //minutos que llevamos
            int tempMin = Mathf.FloorToInt(restante / 60);
            //segundos
            int tempSeg = Mathf.FloorToInt(restante % 60);
            //cambiamos texto
            tiempo.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg);
        }
    }
}
