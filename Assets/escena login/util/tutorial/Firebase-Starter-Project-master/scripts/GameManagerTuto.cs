using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerTuto : MonoBehaviour
{
    public static GameManagerTuto instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    private void Start()
    {
        
    }
}
