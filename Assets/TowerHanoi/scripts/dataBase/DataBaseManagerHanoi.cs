using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseManagerHanoi : MonoBehaviour
{
   

    private string userID;
    private DatabaseReference dbReference;

    //conexion con GameManager
    GameManagerHanoi _myGameManagerHanoi;


    //Start is called before the first frame update
    void Start()
    {
        //coge el identificador unico de cada ordenador 
        userID = SystemInfo.deviceUniqueIdentifier;
        //hace referencia a el firebase
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        //gameManager
        _myGameManagerHanoi = GameManagerHanoi.GetInstance();
    }

    //creamos nuevo usuario en base de datos
    public void CreateUserInicial( )
    {

        //recogemos info
        int TotalTime = _myGameManagerHanoi.GetTiempoTotalHanoiRegistrado();
        int numJugadas = _myGameManagerHanoi.GetnumJugadasTotalHanoiRegistrado();
        int numMovimientosIncorrectos = _myGameManagerHanoi.GetnumMovsIncorrectosHanoiRegistrado();
        int numMovimientosOutOfLimits = _myGameManagerHanoi.GetnumMovsOutOfLimitsHanoiRegistrado();


        //Habiendo puesto al principio el nombre y el correo electronico estará guardado en las 2 variables InputField
        //que se podrán acceder desde un método
        //despues debemos registrar con llamada a GameManager con otro método el time y el numero de ticks totales
        HanoiInfo newUser = new HanoiInfo(TotalTime, numJugadas, numMovimientosIncorrectos, numMovimientosOutOfLimits);
        //guardamos info del objeto como un string
        string json = JsonUtility.ToJson(newUser);
        //escribimos en carpeta users ese string con la info guardada
        dbReference.Child("pruebaHanoi").Child(userID).SetRawJsonValueAsync(json);
    }

    //metodo que se llama cuando se acaba la prueba y que actualiza los datos del usuario
    public void CreateUserTotal()
    {
        //recogemos info
        int TotalTime = _myGameManagerHanoi.GetTiempoTotalHanoiRegistrado();
        int numJugadas = _myGameManagerHanoi.GetnumJugadasTotalHanoiRegistrado();
        int numMovimientosIncorrectos = _myGameManagerHanoi.GetnumMovsIncorrectosHanoiRegistrado();
        int numMovimientosOutOfLimits = _myGameManagerHanoi.GetnumMovsOutOfLimitsHanoiRegistrado();


        //registramos los 2 valores que es nombre y correo en el objeto clase user
        //los otros 2 valores de 0 por ahora se quedan así
        HanoiInfo newUser = new HanoiInfo(TotalTime, numJugadas, numMovimientosIncorrectos, numMovimientosOutOfLimits);
        //guardamos info del objeto como un string
        string json = JsonUtility.ToJson(newUser);
        //escribimos en carpeta users ese string con la info guardada
        dbReference.Child("pruebaHanoi").Child(userID).SetRawJsonValueAsync(json);
    }
}
