using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class InfoTestsMongoDB : MonoBehaviour
{
    static private InfoTestsMongoDB _instanceTests;

    //conexion con ConfianzaManager y CapacidadManager
    ConfianzaManager _myconfianzaManager;
    CapacidadAdaptacionManager _mycapacidadAdaptacionManager;
    UIManagerLogin _myUIManagerLogin;
    string baseUrl = "https://simplebackendingenuity.onrender.com/";
    //por defecto uno puesto a mano
    private string access_token = "";

    private void Awake()
    {

        //si la instancia no existe se hace este script la instancia
        if (_instanceTests == null)
        {
            _instanceTests = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        _mycapacidadAdaptacionManager = CapacidadAdaptacionManager.GetInstanceCapacidadAdaptacionManager();
        //gameManager
        _myconfianzaManager = ConfianzaManager.GetInstanceConfianzaManager();
        //para recolectar el token
        _myUIManagerLogin = UIManagerLogin.GetInstanceUI();
    }

    [System.Obsolete]
    public void RecolectarArgumentosCONF_1()
    {
        //para recolectar el token
        _myUIManagerLogin = UIManagerLogin.GetInstanceUI();
        _myconfianzaManager = ConfianzaManager.GetInstanceConfianzaManager();

        int totalTime = _myconfianzaManager.TiempoPartidaConfianza1();
        string itemNameConfianza1 = _myconfianzaManager.itemNameCONF_1();
        string[] softskillConfianza1 = _myconfianzaManager.softskillCONF_1();
        int type = _myconfianzaManager.typeCONF_1();
        float[] puntuacionConfianza1 = _myconfianzaManager.puntuacionCONF_1();

        //recolectar token de script login register
        access_token = _myUIManagerLogin.GetAccessToken();
        //se empieza corrutina hoyosMongoDB
        StartCoroutine(PutTestConfianza1MongoDB(itemNameConfianza1, softskillConfianza1, type, puntuacionConfianza1, totalTime));
    }

    [System.Obsolete]
    public void RecolectarArgumentosCONF_2()
    {
        //para recolectar el token
        _myUIManagerLogin = UIManagerLogin.GetInstanceUI();
        _myconfianzaManager = ConfianzaManager.GetInstanceConfianzaManager();

        int totalTime = _myconfianzaManager.TiempoPartidaConfianza2();
        string itemNameConfianza2 = _myconfianzaManager.itemNameCONF_2();
        string[] softskillConfianza2 = _myconfianzaManager.softskillCONF_2();
        int type2 = _myconfianzaManager.typeCONF_2();
        float[] puntuacionConfianza2 = _myconfianzaManager.puntuacionCONF_2();

        ////recolectar token de script login register
        //access_token = _myUIManagerLogin.GetAccessToken();
        //se empieza corrutina hoyosMongoDB
        StartCoroutine(PutTestConfianza1MongoDB(itemNameConfianza2, softskillConfianza2, type2, puntuacionConfianza2,totalTime));
    }

    
    public void RecolectarArgumentosCAPAC_1()
    {
        //para recolectar el token
        _myUIManagerLogin = UIManagerLogin.GetInstanceUI();
        _mycapacidadAdaptacionManager = CapacidadAdaptacionManager.GetInstanceCapacidadAdaptacionManager();

        int totalTime = _mycapacidadAdaptacionManager.TiempoPartidaCapacidad1();
        string itemNameCapac1 = _mycapacidadAdaptacionManager.itemNameCAPAC_1();
        string[] softskillCapac1 = _mycapacidadAdaptacionManager.softskillCAPAC_1();
        int typeCapac1 = _mycapacidadAdaptacionManager.typeCapac_1();
        float[] puntuacionCapac1 = _mycapacidadAdaptacionManager.puntuacionCAPAC_1();

        //recolectar token de script login register
        access_token = _myUIManagerLogin.GetAccessToken();
        
        //se empieza corrutina MongoDB reutilizada
        StartCoroutine(PutTestConfianza1MongoDB(itemNameCapac1, softskillCapac1, typeCapac1, puntuacionCapac1,totalTime));
    }

   
    public void RecolectarArgumentosCAPAC_2()
    {
        //para recolectar el token
        _myUIManagerLogin = UIManagerLogin.GetInstanceUI();
        _mycapacidadAdaptacionManager = CapacidadAdaptacionManager.GetInstanceCapacidadAdaptacionManager();

        int totalTime = _mycapacidadAdaptacionManager.TiempoPartidaCapacidad2();
        string itemNameCapac2 = _mycapacidadAdaptacionManager.itemNameCAPAC_2();
        string[] softskillCapac2 = _mycapacidadAdaptacionManager.softskillCAPAC_2();
        int typeCapac2 = _mycapacidadAdaptacionManager.typeCapac_2();
        float[] puntuacionCapac2 = _mycapacidadAdaptacionManager.puntuacionCAPAC_2();

        ////recolectar token de script login register
        //access_token = _myUIManagerLogin.GetAccessToken();
        //se empieza corrutina MongoDB reutilizada
        StartCoroutine(PutTestConfianza1MongoDB(itemNameCapac2, softskillCapac2, typeCapac2, puntuacionCapac2,totalTime));
    }

    [System.Obsolete]
    IEnumerator PutTestConfianza1MongoDB(string itemNameConfianza1, string[] softskillConfianza1, int type, float[] puntuacionConfianza1,int totalTime)
    {


        string uri = $"{baseUrl + "Users/me/gameData/item"}";

        //"itemName": "CONF_2",
        //"softSkill": [  "confianza"],
        //"type": 1,
        //"puntuacion": [1.2]


        string softskillConfianza1join = string.Join(",", softskillConfianza1);
        string puntuacionConfianza1join = puntuacionConfianza1[0].ToString("0.0", CultureInfo.InvariantCulture);

        string body2 = $"{{ \"itemName\": \"{itemNameConfianza1}\", \"softSkill\": [\"{softskillConfianza1join}\"], \"type\": {type}, \"puntuacion\": [{puntuacionConfianza1join}], \"totalTime\": \"{totalTime}\" }}";

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
