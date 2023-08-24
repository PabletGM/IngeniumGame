using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Networking;

public class DeleteMethod : MonoBehaviour
{
    InputField outputArea;

    // Start is called before the first frame update
    void Start()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        GameObject.Find("ButtonA").GetComponent<Button>().onClick.AddListener(DeleteData);
    
    }
    
    void DeleteData() => StartCoroutine(DeleteData_Coroutine());

    IEnumerator DeleteData_Coroutine()
    {
        Debug.Log("Hola");
        outputArea.text = "loading...";

        string baseUrl = "https://eu-west-1.aws.data.mongodb-api.com/app/ingenuity-app-0-mmgty/endpoint/Ingenuity/Test";
        string paramName1 = "username";
        string paramValue1 = "pablo";

        string uri =  $"{baseUrl}?{paramName1}={paramValue1}";

        using (UnityWebRequest request = UnityWebRequest.Delete(uri))
        {
            // Agregar encabezado personalizado si es necesario
            //request.SetRequestHeader("Authorization", "Bearer your_access_token");

            yield return request.SendWebRequest();

            /*
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
            */
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("DELETE request successful!");
                Debug.Log("Response: " + request.responseCode);
                outputArea.text = "Response: " + request.responseCode;

            }
        }
        
    }
    
}
