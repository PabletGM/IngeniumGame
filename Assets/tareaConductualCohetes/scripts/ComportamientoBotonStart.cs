using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ComportamientoBotonStart : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameManagerTareaBengalas _myGameManagerBengalas;
    //tiempo que lleva el timer de bengala desde que despega
    private float restante=0;
    //permiso para volar la bengala
    private bool enMarcha = false;
    //para ver si boton presionado
    private bool estaPresionado = false;
    //5 segundos es lo que tarda el cohete en llegar a las nubes
    private int tiempoMaximo= 4;

    // Start is called before the first frame update
    void Start()
    {
        _myGameManagerBengalas = GameManagerTareaBengalas.GetInstanceGM();
    }

    #region Pulsar boton

        //accion cuando se presiona boton
        public void OnPointerDown(PointerEventData eventData)
        {
            estaPresionado = true;
            // Acción cuando se presiona el botón
            BotonPulsado();
        }

        //si se pulsa boton se inicia timer
        public void BotonPulsado()
        {
            //iniciamos timer
            enMarcha = true;
            //lanzamos cohete
            LanzamientoCohete();
        }

    #endregion

    #region EfectoPulsarBoton
        public void LanzamientoCohete()
        {
            //inicialmente queremos que el cohete suba al pulsar el boton
            _myGameManagerBengalas.LanzamientoCohete();

            //aplicamos esa fuerza al cohete y a la explosion de este
        }
    #endregion

    #region Soltar boton

    //accion cuando se suelta boton
    public void OnPointerUp(PointerEventData eventData)
            {
                estaPresionado = false;
                // Acción cuando se suelta el botón
                BotonSoltado();
            }
            //si se suelta el boton se para el timer
            public void BotonSoltado()
            {
                //timer para
                enMarcha = false;
                //reiniciamos timer
                restante = 0;
                //explotamos cohete
                ExplosionCohete();
            }

    #endregion

    #region EfectoSoltarBoton

        public void ExplosionCohete()
        {
            //inicialmente queremos que el cohete suba al pulsar el boton
            _myGameManagerBengalas.ExplosionCohete();

            //vemos cuanto tiempo se ha pulsado el botón

            //aplicamos esa fuerza al cohete y a la explosion de este
        }
    #endregion

    //comprueba el timer y limite de tiempo
    void Update()
    { 
            #region timer
            //si se ha pulsado boton se inicia timer
            if (enMarcha)
            {
                //va sumando segundos
                restante += Time.deltaTime;
                Debug.Log("Button pressed: " + restante);
                //explosion automatica
                if (restante > tiempoMaximo) { BotonSoltado(); }
            }
            #endregion
    }
}
