using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

using SimpleJSON;

public class pruebas : MonoBehaviour
{
    private string url;

    void Start()
    {
        //StartCoroutine(Pruebauno());

        // A correct website page.
        //StartCoroutine(GetRequest("https://eu-west-1.aws.data.mongodb-api.com/app/ingenuity-application-lfzzr/endpoint/Ingenuity/Test"));

        // A non-existing page.
        //StartCoroutine(GetRequest("https://error.html"));
    }

    public void Pruebauno()
    {


        Debug.Log("Inicio del ejemplo");

        // Llamamos a la función que hace la pausa
        StartCoroutine(PausaDeUnSegundo());

        Debug.Log("Fin del ejemplo212222222222222222222222222");
        
        url = "https://eu-west-1.aws.data.mongodb-api.com/app/ingenuity-application-lfzzr/endpoint/Ingenuity/Test";
        UnityWebRequest www = UnityWebRequest.Get(url);


        Debug.Log("text: " + www.result);

        //yield return webRequest.SendWebRequest();

        /*
        if (www.isNetworkError || www.isHttpError){
            Debug.Log(www.error);
        }else{
            Debug.Log("text: " + www.downloadHandler.text);

            JSONNode data = JSON.Parse(www.downloadHandler.text);

            Debug.Log("json text: " + data);

            string filtro = data["data"];
            Debug.Log("filtro: " + filtro);
        }
        */
    }

    private IEnumerator PausaDeUnSegundo()
    {
        Debug.Log("Inicio de la funcion");
        url = "https://eu-west-1.aws.data.mongodb-api.com/app/ingenuity-application-lfzzr/endpoint/Ingenuity/Test";
        UnityWebRequest www = UnityWebRequest.Get(url);
        // Hacer una pausa de 1 segundo
        yield return new WaitForSeconds(10);
        Debug.Log("fin de la pausa");
        Debug.Log("text dos: " + www.downloadHandler.text);
        Debug.Log("fin de la funcion");
    }
}