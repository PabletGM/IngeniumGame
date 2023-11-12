using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startTest1 : MonoBehaviour
{
    public void StartTest()
    {
        LevelLoader.LoadLevel("confianza");
        //SceneManager.LoadScene("confianza");
    }
    public void FinishTest()
    {
        LevelLoader.LoadLevel("BengalasPrueba");
        //Application.Quit();
    }
}
