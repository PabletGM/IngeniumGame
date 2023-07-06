using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerHanoi : MonoBehaviour
{
    //singleton
    static private UIManagerHanoi _instanceUIHanoi;

    [SerializeField] private GameObject vfxFireworks;
    [SerializeField] private GameObject ImageWin;
    [SerializeField] private GameObject zoom;

    private void Awake()
    {
        //si la instancia no existe se hace este script la instancia
        if (_instanceUIHanoi == null)
        {
            _instanceUIHanoi = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }

    static public UIManagerHanoi GetInstanceUI()
    {
        return _instanceUIHanoi;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFireworksWin(bool set)
    {
        vfxFireworks.SetActive(set);
        ImageWin.SetActive(set);
        zoom.GetComponent<ZoomAutomatic>().ZoomOut();
        ImageWin.transform.DOScale(new Vector3(0.8f, 0.8f, 1f), 2).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        //boton quit tween
        
    }
}
