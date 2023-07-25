using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UserData;

public class script1 : MonoBehaviour
{
    /*
    private string username;
    private string password;
    */
    
    private UserData userData;

    // Start is called before the first frame update
    void Start()
    {
        //userData = FindObjectOfType<UserData>();
        userData = new UserData();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void ReadStringEmail(string s)
    {
        //username = s;
        userData.username = s;
        Debug.Log("username: " +  userData.username);
    }
    public void ReadStringPassword(string s)
    {
        //password = s;
        userData.password = s;
        
        Debug.Log("password: " +  userData.password);
    }

    public void SendAll()
    {
        Debug.Log("username: " +  userData.username  + "\npassword: " +  userData.password );
    }

    /*
    public void CambioScenaDos()
    {
        Debug.Log("cambiode escena a la 2");
        SceneManager.LoadScene("scene2");
    }
    */
    
}
