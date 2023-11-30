using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuidaLateral : MonoBehaviour
{
    //huida al activarse el script

    [SerializeField]
    private Vector3 puntoHuidaEspacio;

    [SerializeField]
    private float duration;
    private void OnEnable()
    {
        HuidaEspacio();
    }

    private void HuidaEspacio()
    {
        this.transform.DOMove(puntoHuidaEspacio, duration);
    }
}
