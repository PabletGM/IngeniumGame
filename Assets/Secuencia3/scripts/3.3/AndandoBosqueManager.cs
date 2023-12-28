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
        AudioManagerBengalas.instance.PlaySFX("pop-Up", 1f);
        Invoke("SoundCogerLlamada", 1f);
        //activamos popUp robot
        telefonoPopUp.SetActive(true);
        //activamos animator
        telefonoPopUp.GetComponent<Animator>().enabled = true;
    }

    private void SoundCogerLlamada()
    {
        AudioManagerBengalas.instance.PlaySFX("cogerLlamada", 1f);
    }

    private void NextScene()
    {
        Debug.Log("nEXT SCENE");
        SceneManager.LoadScene("3.4ConversacionJefeExploracion");
    }

    private void Awake()
    {
        //cambiamos a musica de bosque
        AudioManagerBengalas.instance.PlayMusic("selvaMusic", 1f);
    }
}
