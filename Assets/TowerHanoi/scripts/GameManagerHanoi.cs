using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerHanoi : MonoBehaviour
{

    //singleton
    static private GameManagerHanoi _instanceHanoi;

    private void Awake()
    {
        //si la instancia no existe se hace este script la instancia
        if (_instanceHanoi == null)
        {
            _instanceHanoi = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
