using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTareaBengalas : MonoBehaviour
{
    #region references
    static private GameManagerTareaBengalas _instanceGMTareaBengalas;
    UIManagerTareaBengalas UITareaBengalas;
    #endregion

    [SerializeField]
    private GameObject bengalaParaDespegar;

    private void Awake()
    {

        //si la instancia no existe se hace este script la instancia
        if (_instanceGMTareaBengalas == null)
        {
            _instanceGMTareaBengalas = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }

    static public GameManagerTareaBengalas GetInstanceGM()
    {
        return _instanceGMTareaBengalas;
    }

    public void LanzamientoCohete()
    {
        //llama a la funcion del cohete que lo propulsa para arriba
        bengalaParaDespegar.GetComponent<ComportamientoBengalaADisparar>().DespegarCohete();
        bengalaParaDespegar.GetComponent<ComportamientoBengalaADisparar>().DespegueVFX();
        bengalaParaDespegar.GetComponent<ComportamientoBengalaADisparar>().PrepararPropulsion();
    }

    public void ExplosionCohete()
    {
        //Explota Cohete
        bengalaParaDespegar.GetComponent<ComportamientoBengalaADisparar>().ExplosionCohete();
    
    }


}
