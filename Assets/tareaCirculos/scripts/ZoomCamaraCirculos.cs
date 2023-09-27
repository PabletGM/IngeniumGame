using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamaraCirculos : MonoBehaviour
{
    public bool ZoomActive;
    public Camera cam;
    public float speed;
    public float zoomDuration = 1.0f; // Duración total del efecto de zoom

    private float zoomOrthographicSize = 2.3f;
    private float originalOrthographicSize = 1.8f;
    private float zoomStartTime;

    private void Start()
    {
        cam = Camera.main;
    }

    public void ZoomIn()
    {
        ZoomActive = true;
        zoomStartTime = Time.time;
    }

    public void ZoomOut()
    {
        ZoomActive = false;
        zoomStartTime = Time.time;
    }

    private void LateUpdate()
    {
        if (ZoomActive)
        {
            float elapsedTime = Time.time - zoomStartTime;
            float t = Mathf.Clamp01(elapsedTime / zoomDuration);
            cam.orthographicSize = Mathf.Lerp(originalOrthographicSize, zoomOrthographicSize, t);
        }
        else
        {
            float elapsedTime = Time.time - zoomStartTime;
            float t = Mathf.Clamp01(elapsedTime / zoomDuration);
            cam.orthographicSize = Mathf.Lerp(zoomOrthographicSize, originalOrthographicSize, t);
        }
    }
}
