using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GetMethod : MonoBehaviour
{
    InputField outputArea;

    private string json_;

    [System.Obsolete]
    void Start()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<InputField>(); // Agregamos el paréntesis de cierre aquí
        GameObject.Find("GetButton").GetComponent<Button>().onClick.AddListener(GetData);
    }

    [System.Obsolete]
    public void GetData() => StartCoroutine(GetData_Coroutine());

    [System.Obsolete]
    IEnumerator GetData_Coroutine()
    {
        outputArea.text = "Loading...";
        string uri = "https://eu-west-1.aws.data.mongodb-api.com/app/ingenuity-app-0-mmgty/endpoint/Ingenuity/Test";
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                outputArea.text = request.error;
            }
            else
            {
                outputArea.text = request.downloadHandler.text;
                //Debug.Log(request.downloadHandler.text);
                json_ = request.downloadHandler.text;
            }
        }
    }



    public void VerJSON()
    {
        Debug.Log(json_);
    }
}