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

    //transform inicial bengala
    [SerializeField]
    private Transform transformBengala;
    private Vector3 posInicialBengala;

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

    private void Start()
    {
        //ponemos pos inicial bengala
        posInicialBengala = transformBengala.position;
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

    //metodo que pone en pos inicial la bengala y la activa de nuevo
    public void SiguienteLanzamiento()
    {
        //Activa bengala
        bengalaParaDespegar.SetActive(true);
        //la pone en pos inicial
        bengalaParaDespegar.transform.position = posInicialBengala;
    }


}
