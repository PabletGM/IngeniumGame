using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Networking;

public class PutMethod : MonoBehaviour
{
    InputField outputArea;

    [System.Obsolete]
    void Start()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        GameObject.Find("ButtonA").GetComponent<Button>().onClick.AddListener(PutData);
    }

    [System.Obsolete]
    void PutData() => StartCoroutine(PutData_Coroutine());

    [System.Obsolete]
    IEnumerator PutData_Coroutine()
    {
        Debug.Log("Hola");
        outputArea.text = "loading...";

        string baseUrl = "https://eu-west-1.aws.data.mongodb-api.com/app/ingenuity-app-0-mmgty/endpoint/Ingenuity/Test";
        string paramName1 = "surName";
        string paramValue1 = "dtertre59";

        string uri =  $"{baseUrl}?{paramName1}={paramValue1}";
        string body = "{ \"surName\": \"pablo\", \"field2\": 2, \"field3\": {\"field3.1\": 23, \"field3.2\": \"dos\"} }";

        using (UnityWebRequest request = UnityWebRequest.Put(uri, body))
        {
            yield return request.SendWebRequest();
            /*
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }*/
            if (request.isNetworkError || request.isHttpError)
            {
                outputArea.text = request.error;
                Debug.Log("ERRORRRRR");
            }
            else
            {
                outputArea.text = request.downloadHandler.text;
                Debug.Log("BIEN");
            }
        }   
    } 
}
