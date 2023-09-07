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
        SceneManager.LoadScene("tutorial");
    }
}
