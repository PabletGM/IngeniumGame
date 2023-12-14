using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BotonPasarTextoVisualEffect : MonoBehaviour
{
    [SerializeField]
    private Vector3 sizeBig = new Vector3(0.8f, 0.8f, 0.8f);
    [SerializeField]
    private Vector3 sizeSmall = new Vector3(0.6f, 0.6f, 0.6f);

    private float duracion = 1f;
    private void OnEnable()
    {
        Invoke("TweenSizeBig",duracion);
    }

    private void TweenSizeBig()
    {
        this.gameObject.transform.DOScale(sizeBig,duracion);
        Invoke("TweenSizeSmall", duracion);
    }

    private void TweenSizeSmall()
    {
        this.gameObject.transform.DOScale(sizeSmall, duracion);
        Invoke("TweenSizeBig", duracion);
    }
}
