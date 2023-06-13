using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameManager _myGameManager;
    //lista de waypoints a los que se podrá desplazar el jugador
    [SerializeField]
    private Transform[] waypoint;
    //saber posicion del waypoint
    public Transform target;
    public float speed;
    //mientras no haya llegado a la meta
    private bool movimiento = false;
    // Start is called before the first frame update
    void Update()
    {
        if(movimiento)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
        }
    }

    //pregunta al GameManager que botón se ha pulsado y nos lo devuelve
    //una vez que sabemos el boton pulsado cambiamos el waypoint acorde a el
    public void EncontrarBotonPulsado()
    {
       
        SelectedButton instance = _myGameManager.buttonPressed();
        //averiguamos el nombre del gameObject del boton
        string nameButtonPressed = instance.gameObject.name;
        //lo imprimimos por pantalla
        //segun el nombre que tenga el boton seleccionado cambiamos el target a un waypoint diferente
        switch(nameButtonPressed)
        {
            case "Hoyo0":
                
                target = waypoint[0];
                break;

            case "Hoyo1":
                
                target = waypoint[1];
                break;

            case "Hoyo2":
                
                target = waypoint[2];
                break;

            case "Hoyo3":
                
                target = waypoint[3];
                break;

            case "Hoyo4":
                
                target = waypoint[4];
                break;

            case "Hoyo5":
                
                target = waypoint[5];
                break;

            default:
                
                break;
        }
    }
    
    //comenzamos la corrutina de desplazamiento del player
    public void EmpezarMoverJugador()
    {
        //según que boton haya pulsado debemos cambiar el target
        EncontrarBotonPulsado();
        StartCoroutine(WalkingWaypoint());
    }


    //se hacen corrutinas de andar
    public IEnumerator WalkingWaypoint()
    {

            yield return new WaitForSeconds(0f);
        //mientras que no haya llegado a la meta sigue andando
        Invoke("seguirandando", 0f);


        //si ha llegado a la meta se paran las corrutinas
        //else
        //{
        //    StopAllCoroutines();
        //    meta = false;
        //}
    }

    //loop de mover jugador hasta que llegue a la meta
    private void seguirandando()
    {
        MoverJugador();
        EmpezarMoverJugador();
        //StartCoroutine(WalkingWaypoint());
    }
    public void MoverJugador()
    {
        //das permiso para que se mueva
        movimiento = true;
        
    }
    private void Start()
    {
        _myGameManager = GameManager.GetInstance();
    }


}
