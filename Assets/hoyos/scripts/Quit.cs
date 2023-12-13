using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    public void QuitarApp()
    {
        Application.Quit();
    }

    //metodo temporal pasar a escena Hanoi
    public void ChangeSceneHanoi()
    {
        //SceneManager.LoadScene("tutorial");
        //LevelLoader.LoadLevel("tutorial");
        LevelLoader.LoadLevel("EscenaInicial3EnRaya");
    }

    public void ChangeNextScene()
    {
        //SceneManager.LoadScene("tutorial");
        //LevelLoader.LoadLevel("inicioTest1");
    }
}
