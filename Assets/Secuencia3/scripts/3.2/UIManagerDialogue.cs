using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerDialogue : MonoBehaviour
{

    [SerializeField]
    private GameObject robotPopUp;

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
        this.gameObject.transform.DOScale(new Vector3(0.76f, 0.76f, 0.76f),1f);
        Debug.Log("he");
    }

    public void SetDialoguePanelPequeno()
    {
        Debug.Log("ha");
        this.gameObject.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);
    }

    public void RobotPopUpAparecer()
    {
        //activamos popUp robot
        robotPopUp.SetActive(true);
        //activamos animator
        robotPopUp.GetComponent<Animator>().enabled = true;
    }

    public void RobotPopQuitar()
    {
        //activamos popUp robot
        robotPopUp.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f);
    }
}
