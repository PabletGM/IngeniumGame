using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startTest1 : MonoBehaviour
{
    public void StartTest()
    {
        SceneManager.LoadScene("confianza");
    }
    public void FinishTest()
    {
        Application.Quit();
    }
}
