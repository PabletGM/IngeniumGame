using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Networking;

public class PostMethod : MonoBehaviour
{
    InputField outputArea;

    //private int[] = num;


    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        GameObject.Find("ButtonA").GetComponent<Button>().onClick.AddListener(PostData);
    
    }

    [System.Obsolete]
    void PostData() => StartCoroutine(PostData_Coroutine());

    /*
    IEnumerator PostData_Coroutine()
    {
        Debug.Log("Hola");
        yield return null; // Pausa de un frame (opcional)
        Debug.Log("adios");
    }
    */


    [System.Obsolete]
    IEnumerator PostData_Coroutine()
    {
        Debug.Log("Hola");
        outputArea.text = "loading...";
        string uri = "https://eu-west-1.aws.data.mongodb-api.com/app/ingenuity-app-0-mmgty/endpoint/Ingenuity/Test";

        //WWWForm form = new WWWForm();
        //form.AddField("title", "test data");

        //num = new int [6];


        int num = 11;


        int[] numbersArray = new int[3]; // Crear un array de enteros con capacidad para 3 elementos

        numbersArray[0] = 10;
        numbersArray[1] = 20;
        numbersArray[2] = 30;

        string numbersArrayString = string.Join(",", numbersArray); // Convierte el array en una cadena separada por comas

        Debug.Log(numbersArray);
        Debug.Log(numbersArrayString);

        //se pone el form en el segundo argumento de la funcion post
        string body = $"{{ \"username\": \"pablo\", \"password\": \"1234\", \"field3\": {{\"field3.-1\": {num},\"field3.0\": [{numbersArrayString}],\"field3.1\": [\"uno\",1], \"field3.2\": \"dos\"}} }}";

        using (UnityWebRequest request = UnityWebRequest.Post(uri, body, "application/json"))
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
