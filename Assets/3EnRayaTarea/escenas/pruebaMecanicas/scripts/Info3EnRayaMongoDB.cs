using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Info3EnRayaMongoDB : MonoBehaviour
{

    //conexion con el GameManager que guarda puntuacion del 3 en raya
    PuntuacionTest3EnRaya _myPuntuacionTest3EnRaya;
    UIManagerLogin _myUIManagerLogin;
    string baseUrl = "https://simplebackendingenuity.onrender.com/";
    //por defecto uno puesto a mano
    private string access_token = "";

    private float[] puntuacionTest3EnRaya;

    private void Start()
    {
        puntuacionTest3EnRaya = new float[3];
        //gameManager
        _myPuntuacionTest3EnRaya = PuntuacionTest3EnRaya.GetInstanceGM();
        //para recolectar el token
        _myUIManagerLogin = UIManagerLogin.GetInstanceUI();
    }

    //este metodo se llama cada vez que se pulsa una opcion del item


    public void RecolectarArgumentos3EnRaya()
    {
        //recoger argumentos de 3 en Raya en pregunta final de ultimo test en modo normal 
        puntuacionTest3EnRaya = _myPuntuacionTest3EnRaya.DevolverArrayRespuestas3EnRaya();
        //recolectar token de script login register
        access_token = _myUIManagerLogin.GetAccessToken();
        //se empieza corrutina hoyosMongoDB
        StartCoroutine(PutPuntuacion3EnRayaMongoDB());


    }

    [System.Obsolete]
    IEnumerator PutPuntuacion3EnRayaMongoDB()
    {
        string uri = $"{baseUrl}Users/gameData/tresEnRaya";

        // Convierte el arreglo de flotantes a una cadena JSON válida
        string victoria = string.Join(", ", puntuacionTest3EnRaya.Select(f => f.ToString("0.0", CultureInfo.InvariantCulture)));

        // Construye el cuerpo JSON con la estructura deseada
        string body = $"{{ \"victorias\": [{victoria}] }}";

        Debug.Log(victoria);

        using (UnityWebRequest request = UnityWebRequest.Put(uri, body))
        {
            request.SetRequestHeader("Authorization", "Bearer " + access_token);
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("ERRORRRRR");
            }
            else
            {
                Debug.Log("BIEN");
            }
        }
    }
}
