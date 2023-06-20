using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;

public class DataBaseManager : MonoBehaviour
{
    public InputField Name;
    public InputField Gold;

    private string userID;
    private DatabaseReference dbReference;
    // Start is called before the first frame update
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    //creamos nuevo usuario en base de datos
    private void CreateUser()
    {
        User newUser = new User(Name.text, int.Parse(Gold.text));
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(userID);
    }
}
