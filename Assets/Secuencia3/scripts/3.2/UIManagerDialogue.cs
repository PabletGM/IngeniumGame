using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerDialogue : MonoBehaviour
{



    public static UIManagerDialogue instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetDialoguePanelGrande();
    }

    public void SetDialoguePanelGrande()
    {
        this.gameObject.transform.DOScale(new Vector3(1.84f, 1.84f, 1.84f),1f);
        Debug.Log("he");
    }

    public void SetDialoguePanelPequeno()
    {
        Debug.Log("ha");
        this.gameObject.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);
    }
}
