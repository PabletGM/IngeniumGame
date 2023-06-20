using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;
using TMPro;
using System;

public class DataBaseManager : MonoBehaviour
{
    public InputField Name;
    public InputField Gold;

    public Text NameText;
    public Text GoldText;

    private string userID;
    private DatabaseReference dbReference;
    //Start is called before the first frame update
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    //creamos nuevo usuario en base de datos
    public void CreateUser()
    {
        //registramos los 2 valores que es nombre y numero en el objeto
        User newUser = new User(Name.text, int.Parse(Gold.text));
        //guardamos info del objeto como un string
        string json = JsonUtility.ToJson(newUser);
        //escribimos en carpeta users ese string con la info guardada
        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }

    //obtener nombre de carpeta users
    public IEnumerator GetName(Action<string> onCallback)
    {
        //hacemos carpeta Users
        var userNameData = dbReference.Child("users").Child(userID).Child("name").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if(userNameData!=null)
        {
            DataSnapshot snapshot = userNameData.Result;

            onCallback.Invoke(snapshot.Value.ToString());
        }
    }

    //obtener numero de carpeta users
    public IEnumerator GetGold(Action<int> onCallback)
    {
        //hacemos carpeta Users
        var userGoldData = dbReference.Child("users").Child(userID).Child("gold").GetValueAsync();

        yield return new WaitUntil(predicate: () => userGoldData.IsCompleted);

        if (userGoldData != null)
        {
            DataSnapshot snapshot = userGoldData.Result;

            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }

    //coge datos de la base de datos gold y name y pone su resultado y lo escribe
    public void GetUserInfo()
    {
        StartCoroutine(GetGold((int gold) => {
            GoldText.text = "Gold: " + gold.ToString();


        }));

        StartCoroutine(GetName((string name) => {
            NameText.text = "Name: " +name;


        }));

        
    }

    public void UpdateName()
    {
        dbReference.Child("users").Child(userID).Child("name").SetValueAsync(Name.text);
    }

    public void UpdateGold()
    {
        dbReference.Child("users").Child(userID).Child("gold").SetValueAsync(Gold.text);
    }
}
