using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAutomatic : MonoBehaviour
{
    public bool ZoomActive;

    public Vector3[] target;

    public Camera cam;

    public float speed;

    private void Start()
    {

        cam = Camera.main;
    }

    public void ZoomIn()
    {
        AudioManagerHanoi.Instance.PlaySFX("zoom");
        ZoomActive = true;
    }

    public void ZoomOut()
    {
        AudioManagerHanoi.Instance.PlaySFX("zoom");
        ZoomActive = false;
    }


    public void LateUpdate()
    {
        if(ZoomActive)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 1.2f, speed);
        }
        else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3f, speed);
        }
    }


}
