using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogue;


   

    public void ActivarDialogo()
    {
        dialogue.SetActive(true);

    }
}
