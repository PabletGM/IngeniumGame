using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;
using TMPro;
using System;
public class DataBaseManagerHoyos : MonoBehaviour
{
    public InputField NamePlayer;
    public InputField MailPlayer;

    //public Text NamePlayerText;
    //public Text MailPlayerText;

    private string userID;
    private DatabaseReference dbReference;

    //conexion con GameManager
    GameManager _myGameManager;


    //Start is called before the first frame update
    void Start()
    {
        //coge el identificador unico de cada ordenador 
        userID = SystemInfo.deviceUniqueIdentifier;
        //hace referencia a el firebase
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        //gameManager
       _myGameManager = GameManager.GetInstance();
    }

    //creamos nuevo usuario en base de datos
    public void CreateUserInicial()
    {
        //Habiendo puesto al principio el nombre y el correo electronico estará guardado en las 2 variables InputField
        //que se podrán acceder desde un método
        //despues debemos registrar con llamada a GameManager con otro método el time y el numero de ticks totales

        //registramos los 2 valores que es nombre y correo en el objeto clase user
        //los otros 2 valores de 0 por ahora se quedan así
        HoyoInfo newUser = new HoyoInfo(NamePlayer.text, MailPlayer.text,0,0);
        //guardamos info del objeto como un string
        string json = JsonUtility.ToJson(newUser);
        //escribimos en carpeta users ese string con la info guardada
        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }

    //metodo que se llama cuando se acaba la prueba y que actualiza los datos del usuario
    public void CreateUserTotal()
    {
        //Habiendo puesto al principio el nombre y el correo electronico estará guardado en las 2 variables InputField
        //que se podrán acceder desde un método
        //despues debemos registrar con llamada a GameManager con otro método el time y el numero de ticks totales
        int totalTime = _myGameManager.NumSecsPartidaReturn();

        //registramos los 2 valores que es nombre y correo en el objeto clase user
        //los otros 2 valores de 0 por ahora se quedan así
        HoyoInfo newUser = new HoyoInfo(NamePlayer.text, MailPlayer.text, 0, totalTime);
        //guardamos info del objeto como un string
        string json = JsonUtility.ToJson(newUser);
        //escribimos en carpeta users ese string con la info guardada
        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }



    //obtener nombre de carpeta users, valor de namePlayer
    public IEnumerator GetNamePlayer(Action<string> onCallback)
    {
        //hacemos carpeta Users
        var userNamePlayerData = dbReference.Child("users").Child(userID).Child("namePlayer").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNamePlayerData.IsCompleted);

        if (userNamePlayerData != null)
        {
            DataSnapshot snapshot = userNamePlayerData.Result;

            onCallback.Invoke(snapshot.Value.ToString());
        }
    }

    //obtener numero de carpeta users, valor de mailPlayer
    public IEnumerator GetMailPlayer(Action<string> onCallback)
    {
        //hacemos carpeta Users
        var userMailPlayerData = dbReference.Child("users").Child(userID).Child("mailPlayer").GetValueAsync();

        yield return new WaitUntil(predicate: () => userMailPlayerData.IsCompleted);

        if (userMailPlayerData != null)
        {
            DataSnapshot snapshot = userMailPlayerData.Result;

            onCallback.Invoke(snapshot.Value.ToString());
        }
    }

    ////coge datos de la base de datos namePlayer y mailPlayer, su resultado y lo escribe
    //public void GetUserInfo()
    //{
    //    StartCoroutine(GetMailPlayer((string mailPlayer) => {
    //        MailPlayerText.text = "Mail: " + mailPlayer.ToString();


    //    }));

    //    StartCoroutine(GetNamePlayer((string namePlayer) => {
    //        NamePlayerText.text = "Name: " + namePlayer.ToString();


    //    }));


    //}

    //public void UpdateName()
    //{
    //    dbReference.Child("users").Child(userID).Child("namePlayer").SetValueAsync(NamePlayerText.text);
    //}

    //public void UpdateGold()
    //{
    //    dbReference.Child("users").Child(userID).Child("mailPlayer").SetValueAsync(MailPlayerText.text);
    //}

}
