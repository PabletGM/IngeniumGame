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
    string baseUrl = "https://simplebackendingenuity.onrender.com/";
    private string access_token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkdGVydHJlNTkiLCJleHAiOjE2OTM5OTU3MTF9.XtcoDHHQYrOFrgBtBJrIcpGXYQRrlED0Bbs47Kkp7ew";

    private void Start()
    {
        //gameManager
        _myGameManager = GameManager.GetInstance();
    }

    public void RecolectarArgumentosHoyos()
    {
        int totalTime = _myGameManager.NumSecsPartidaReturn();
        int numExcavacionesTotales = _myGameManager.NumExcavacionesTotales();
        //declaramos tamaño array hoyos
        numpicadasHoyosIndiv = new int[6];
        //numero picadas totales cada hoyo
        numpicadasHoyosIndiv = _myGameManager.DevolverPicadasHoyo();
        //se empieza corrutina hoyosMongoDB
        StartCoroutine(PutHoyosMongoDB(totalTime, numExcavacionesTotales, numpicadasHoyosIndiv));
        //recolectar token de script login register

    }
    

    IEnumerator PutHoyosMongoDB(int totalTime, int numExcavacionesTotales, int[] numPicadasHoyoIndiv)
    {
        int[] numbersArray = new int[4]; // Crear un array de enteros con capacidad para 3 elementos

        numbersArray[0] = 10;
        numbersArray[1] = 20;
        numbersArray[2] = 30;
        numbersArray[3] = 30;

        string numPicadasHoyoIndivString = string.Join(",", numPicadasHoyoIndiv);

        string uri = $"{baseUrl + "Users/gameData/hoyos"}";
        string body = "{ \"numExcavacionesTotales\": 0, \"totalTime\": 2, \"numPicadasCadaHoyo\": {\"0\": 0, \"1\": 1, \"2\": 2, \"3\": 3 } }";

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
