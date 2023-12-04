using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComportamientoLogo : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Invoke("NextScene", 6f);
    }

    private void NextScene()
    {
        SceneManager.LoadScene("loginCorrect");
    }


}
