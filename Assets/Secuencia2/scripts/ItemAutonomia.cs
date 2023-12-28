using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemAutonomia : MonoBehaviour
{
    [SerializeField]
    private GameObject itemMenuInicio;


    private void Start()
    {
        //Temporal hasta tener audioManager unificado
        if(AudioManagerBengalas.instance!=null)
        {
            AudioManagerBengalas.instance.PlaySFX("sonidoItem", 1f);
        }
        
        Invoke("SetSizeItemBig", 1f);
    }

    private void SetSizeItemBig()
    {
        itemMenuInicio.transform.DOScale(new Vector3(1f, 1f, 1f), 1f);
    }

    public void SetSizeItemLittle()
    {
        itemMenuInicio.transform.DOScale(new Vector3(0.001f, 0.001f, 0.001f), 1f);
        if(SceneManager.GetActiveScene().name =="escenaItem")
        {
            Invoke("NextScene", 1f);
        }
        else if (SceneManager.GetActiveScene().name == "escenaItemAutonomia2")
        {
            Invoke("NextScene2", 1f);
        }
        else if (SceneManager.GetActiveScene().name == "3.5Item")
        {
            AudioManagerBengalas.instance.PlaySFX("clickButton", 1f);
            Invoke("NextScene3", 1f);
        }

    }

    private void NextScene()
    {
        Debug.Log("next scene");
        SceneManager.LoadScene("escenaConversacionRobot4");
    }

    private void NextScene2()
    {
        Debug.Log("next scene");
        SceneManager.LoadScene("escenaConversacionRobot5");
    }

    private void NextScene3()
    {
        Debug.Log("next scene");
        SceneManager.LoadScene("3.6AndandoBosqueManager");
    }
}
