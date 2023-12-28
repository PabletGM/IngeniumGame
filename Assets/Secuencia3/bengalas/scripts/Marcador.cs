using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marcador : MonoBehaviour
{
    [SerializeField]
    private GameObject animacion;
    [SerializeField]
    private GameObject ubicacion;

    Vector3 ultimaPosBengala;



    #region MovimientoUbicacion

    private float distanciaMovimiento = 0.1f; // Distancia total que el sprite se moverá hacia arriba y hacia abajo
    private float duracionMovimiento = 1.0f; // Duración de un ciclo de movimiento (hacia arriba y hacia abajo)

    private float posYInicial;
    private float posYFinal;
    #endregion



    //recoge la info de la ultima posicion de la bengala tirada
    public void RegisterUltimaPosBengala(Vector3 ultimaPosBengalaTirada)
    {
        ultimaPosBengala = ultimaPosBengalaTirada;

        //movemos marcador a la posicion y,z del cohete, pero con la posicion x de antes
        this.gameObject.transform.position = new Vector3(transform.position.x, ultimaPosBengala.y, ultimaPosBengala.z);

        //su animacion dura 1 sec, así que al acabar quitamos esta
        Invoke("QuitarAnimacion", 1f);
    }

    private void QuitarAnimacion()
    {
        //desaparecer tween
        animacion.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
        animacion.SetActive(false);
        //activamos ubicacion
        ubicacion.SetActive(true);
        //activamos movimiento ubicacion
        MovimientoUbicacion();
    }

    private void MovimientoUbicacion()
    {
        posYInicial = ubicacion.transform.position.y;
        posYFinal = posYInicial + distanciaMovimiento;

        // Iniciar el movimiento hacia arriba y abajo
        MoverSprite();
    }

    private void MoverSprite()
    {
        // Mover el sprite hacia la posición final en Y y luego volver a la posición inicial en Y en un bucle infinito
        ubicacion.transform.DOMoveY(posYFinal, duracionMovimiento)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                ubicacion.transform.DOMoveY(posYInicial, duracionMovimiento)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => MoverSprite());
            });
    }
}
