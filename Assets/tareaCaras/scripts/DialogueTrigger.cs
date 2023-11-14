using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject lineView;
    public GameObject optionsListView;


   

    public void ActivarDialogo()
    {
        lineView.SetActive(true);
        optionsListView.SetActive(true);
    }
}
