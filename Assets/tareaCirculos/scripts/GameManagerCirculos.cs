using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerCirculos : MonoBehaviour
{
    #region references
    static private GameManagerCirculos _instanceGMCirculos;
    UIManagerCirculos UICirculos;
    #endregion

    private int puntuacionInicial = 100;

    private int puntuacionActual = 100;

    private int golpeChoqueNivel1 = 2;

    private void Awake()
    {

        //si la instancia no existe se hace este script la instancia
        if (_instanceGMCirculos == null)
        {
            _instanceGMCirculos = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }

    static public GameManagerCirculos GetInstanceGM()
    {
        return _instanceGMCirculos;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //se ha salido fuera del circulo
    public void PerderPuntuacion()
    {
        //si estamos en primera escena al chocarnos perdemos 2 puntos
        puntuacionActual -= golpeChoqueNivel1;
        string puntuacionActualString = puntuacionActual.ToString();
        //lo ponemos en el UIManager
        UIManagerCirculos.GetInstanceUI().CambiarPuntuacion(puntuacionActualString);
    }
}
