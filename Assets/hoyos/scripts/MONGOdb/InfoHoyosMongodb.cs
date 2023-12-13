using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InfoHoyosMongodb : MonoBehaviour
{
    //numero de picadas en cada hoyo
    private int[] numpicadasHoyosIndiv;
    //conexion con GameManager
    GameManager _myGameManager;
    UIManagerLogin _myUIManagerLogin;
    string baseUrl = "https://simplebackendingenuity.onrender.com/";
    //por defecto uno puesto a mano
    private string access_token = "";

    private void Start()
    {
        //gameManager
        _myGameManager = GameManager.GetInstance();
        //para recolectar el token
        _myUIManagerLogin =UIManagerLogin.GetInstanceUI();
    }

    [System.Obsolete]
    public void RecolectarArgumentosHoyos()
    {
        int totalTime = _myGameManager.NumSecsPartidaReturn();
        int numExcavacionesTotales = _myGameManager.NumExcavacionesTotales();
        //declaramos tamaño array hoyos
        numpicadasHoyosIndiv = new int[6];
        //numero picadas totales cada hoyo
        numpicadasHoyosIndiv = _myGameManager.DevolverPicadasHoyo();
        //recolectar token de script login register
        access_token = _myUIManagerLogin.GetAccessToken();
        //se empieza corrutina hoyosMongoDB
        StartCoroutine(PutHoyosMongoDB(totalTime, numExcavacionesTotales, numpicadasHoyosIndiv));
       

    }

    [System.Obsolete]
    IEnumerator PutHoyosMongoDB(int totalTime, int numExcavacionesTotales, int[] numPicadasHoyoIndiv)
    {
        string numPicadasHoyoIndivString = string.Join(",", numPicadasHoyoIndiv);

        string uri = $"{baseUrl + "Users/me/gameData/hoyos"}";

        string body2= $"{{ \"numExcavacionesTotales\": {numExcavacionesTotales}, \"totalTime\": {totalTime}, \"numPicadasCadaHoyo\": [{numPicadasHoyoIndivString}]}}";

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
