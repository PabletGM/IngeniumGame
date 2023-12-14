using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InfoCirculosMongoDB : MonoBehaviour
{
    //numero de picadas en cada hoyo
    private int[] patronRondas;
    //conexion con GameManager
    GameManagerCirculos _myGameManagerCirculos;
    UIManagerLogin _myUIManagerLogin;
    string baseUrl = "https://simplebackendingenuity.onrender.com/";
    //por defecto uno puesto a mano
    private string access_token = "";

    private void Start()
    {
        //gameManager
        _myGameManagerCirculos = GameManagerCirculos.GetInstanceGM();
        //para recolectar el token
        _myUIManagerLogin = UIManagerLogin.GetInstanceUI();
    }

    //este metodo se llame desde la escena final o cuando sale el panelRonda

    [System.Obsolete]
    public void RecolectarArgumentosCirculos()
    {
        int totalTime = _myGameManagerCirculos.TiempoPartidaCirculos();
        //recoger argumentos
        patronRondas = _myGameManagerCirculos.DevolverRondasJugador();
        //recolectar token de script login register
        access_token = _myUIManagerLogin.GetAccessToken();
        //se empieza corrutina hoyosMongoDB
        StartCoroutine(PutCirculosMongoDB(totalTime));


    }

    [System.Obsolete]
    IEnumerator PutCirculosMongoDB(int totalTime)
    {
        string patronRondasString = string.Join(",", patronRondas);

        string uri = $"{baseUrl + "Users/me/gameData/circulos"}";

        string body2 = $"{{ \"nivel\": [{patronRondasString}], \"totalTime\": \"{totalTime}\" }}";

        Debug.Log(body2);

        using (UnityWebRequest request = UnityWebRequest.Put(uri, body2))
        {
            request.SetRequestHeader("Authorization", "Bearer " + access_token);
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            /*
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }*/
            if (request.isNetworkError || request.isHttpError)
            {
                //outputArea.text = request.error;
                Debug.Log("ERRORRRRR");
            }
            else
            {
                //outputArea.text = request.downloadHandler.text;
                Debug.Log("BIEN");
            }
        }
    }
}

