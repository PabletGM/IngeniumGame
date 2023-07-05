using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerHanoi : MonoBehaviour
{
    //singleton
    static private UIManagerHanoi _instanceUIHanoi;

    [SerializeField] private GameObject vfxFireworks;

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

    static public UIManagerHanoi GetInstance()
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

    public void SetFireworks(bool set)
    {
        vfxFireworks.SetActive(set);
    }
}
