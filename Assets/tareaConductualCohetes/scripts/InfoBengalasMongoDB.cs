using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InfoBengalasMongoDB : MonoBehaviour
{
    //conexion con GameManager
    GameManagerTareaBengalas _myGameManagerBengalas;
    UIManagerLogin _myUIManagerLogin;
    string baseUrl = "https://simplebackendingenuity.onrender.com/";
    //por defecto uno puesto a mano
    private string access_token = "";

    private void Start()
    {
        //gameManager
        _myGameManagerBengalas = GameManagerTareaBengalas.GetInstanceGM();
        //para recolectar el token
        _myUIManagerLogin = UIManagerLogin.GetInstanceUI();
    }

    [System.Obsolete]
    public void RecolectarArgumentosBengalas()
    {
        //recolectar parametros, altura tipo int
        int[] alturaCohetes = _myGameManagerBengalas.AlturasCohetes();
        //recolectar token de script login register
        access_token = _myUIManagerLogin.GetAccessToken();
        //se empieza corrutina hoyosMongoDB
        StartCoroutine(PutBengalasMongoDB(alturaCohetes));
    }

    [System.Obsolete]
    IEnumerator PutBengalasMongoDB(int[] alturaCohetes)
    {
        string uri = $"{baseUrl + "Users/gameData/bengalas"}";

        // Convierte el arreglo de enteros a una cadena JSON válida
        string alturaCohete = string.Join(",", alturaCohetes);

        // Construye el cuerpo JSON con la estructura deseada
        string body = $"{{ \"alturaCohete\": [{alturaCohete}] }}";

        Debug.Log(alturaCohete);

        using (UnityWebRequest request = UnityWebRequest.Put(uri, body))
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
