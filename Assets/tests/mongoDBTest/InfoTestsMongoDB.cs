using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class InfoTestsMongoDB : MonoBehaviour
{
    //conexion con GameManager
    ConfianzaManager _myconfianzaManager;
    UIManagerLogin _myUIManagerLogin;
    string baseUrl = "https://simplebackendingenuity.onrender.com/";
    //por defecto uno puesto a mano
    private string access_token = "";

    private void Start()
    {
        //gameManager
        _myconfianzaManager = ConfianzaManager.GetInstanceConfianzaManager();
        //para recolectar el token
        _myUIManagerLogin = UIManagerLogin.GetInstanceUI();
    }

    [System.Obsolete]
    public void RecolectarArgumentosCONF_1()
    {
        //int TotalTime = _myGameManagerHanoi.GetTiempoTotalHanoiRegistrado();
        //int numJugadas = _myGameManagerHanoi.GetnumJugadasTotalHanoiRegistrado();
        //int numMovimientosIncorrectos = _myGameManagerHanoi.GetnumMovsIncorrectosHanoiRegistrado();
        //int numMovimientosOutOfLimits = _myGameManagerHanoi.GetnumMovsOutOfLimitsHanoiRegistrado();

        string itemNameConfianza1 = _myconfianzaManager.itemNameCONF_1();
        string[] softskillConfianza1 = _myconfianzaManager.softskillCONF_1();
        int type = _myconfianzaManager.typeCONF_1();
        float[] puntuacionConfianza1 = _myconfianzaManager.puntuacionCONF_1();

        //recolectar token de script login register
        access_token = _myUIManagerLogin.GetAccessToken();
        //se empieza corrutina hoyosMongoDB
        StartCoroutine(PutTestConfianza1MongoDB(itemNameConfianza1, softskillConfianza1, type, puntuacionConfianza1));
    }

    [System.Obsolete]
    public void RecolectarArgumentosCONF_2()
    {
        //int TotalTime = _myGameManagerHanoi.GetTiempoTotalHanoiRegistrado();
        //int numJugadas = _myGameManagerHanoi.GetnumJugadasTotalHanoiRegistrado();
        //int numMovimientosIncorrectos = _myGameManagerHanoi.GetnumMovsIncorrectosHanoiRegistrado();
        //int numMovimientosOutOfLimits = _myGameManagerHanoi.GetnumMovsOutOfLimitsHanoiRegistrado();

        string itemNameConfianza2 = _myconfianzaManager.itemNameCONF_2();
        string[] softskillConfianza2 = _myconfianzaManager.softskillCONF_2();
        int type2 = _myconfianzaManager.typeCONF_2();
        float[] puntuacionConfianza2 = _myconfianzaManager.puntuacionCONF_2();

        //recolectar token de script login register
        access_token = _myUIManagerLogin.GetAccessToken();
        //se empieza corrutina hoyosMongoDB
        StartCoroutine(PutTestConfianza1MongoDB(itemNameConfianza2, softskillConfianza2, type2, puntuacionConfianza2));
    }

    [System.Obsolete]
    IEnumerator PutTestConfianza1MongoDB(string itemNameConfianza1, string[] softskillConfianza1, int type, float[] puntuacionConfianza1)
    {


        string uri = $"{baseUrl + "Users/gameData/item"}";

        //"itemName": "CONF_2",
        //"softSkill": [  "confianza"],
        //"type": 1,
        //"puntuacion": [1.2]


        string softskillConfianza1join = string.Join(",", softskillConfianza1);
        string puntuacionConfianza1join = puntuacionConfianza1[0].ToString("0.0", CultureInfo.InvariantCulture);

        string body2 = $"{{ \"itemName\": \"{itemNameConfianza1}\", \"softSkill\": [\"{softskillConfianza1join}\"], \"type\": {type}, \"puntuacion\": [{puntuacionConfianza1join}] }}";

        Debug.Log(body2);

        using (UnityWebRequest request = UnityWebRequest.Put(uri, body2))
        {
            request.SetRequestHeader("Authorization", "Bearer " + access_token);
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            
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
