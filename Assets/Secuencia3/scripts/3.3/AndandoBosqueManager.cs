using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndandoBosqueManager : MonoBehaviour
{

    [SerializeField]
    private GameObject telefonoPopUp;
    //tiempo espera siguiente escena
    [SerializeField]
    private float timeNextSceneWait;
    public void FuncionalidadTelefono()
    {
        Invoke("TelefonoPopUpAparecer", 0f);
        //INVOCAMOS
        Invoke("NextScene", timeNextSceneWait);
    }

    public void TelefonoPopUpAparecer()
    {

        //activamos popUp robot
        telefonoPopUp.SetActive(true);
        //activamos animator
        telefonoPopUp.GetComponent<Animator>().enabled = true;
    }

    private void NextScene()
    {
        Debug.Log("nEXT SCENE");
        SceneManager.LoadScene("3.4ConversacionJefeExploracion");
    }
}
