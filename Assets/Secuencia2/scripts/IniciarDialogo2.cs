using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciarDialogo2 : MonoBehaviour
{

    [SerializeField]
    private GameObject DialoguePanel;
    [SerializeField]
    private GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ComenzarTextoRobot", 0.5f);

    }

    private void ComenzarTextoRobot()
    {
        //inicias robot
        robot.SetActive(true);
        Invoke("DialogoPanel", 1f);

    }

    private void DialogoPanel()
    {
        //activamos texto y animacion robot
        DialoguePanel.SetActive(true);
        TweenAumentarTamañoDialogo();
    }

    public void TweenAumentarTamañoDialogo()
    {
        //1 segundo de tween 
        DialoguePanel.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f);

    }


}
