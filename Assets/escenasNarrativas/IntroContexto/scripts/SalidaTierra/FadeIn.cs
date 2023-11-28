using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public float transitionDuration; // Duración de la transición en segundos
    private Image transitionImage;

    [SerializeField]
    private GameObject TransitionCanvas;

    [SerializeField]
    private GameObject titulo;
    // Start is called before the first frame update
    void Start()
    {
        transitionImage = GetComponentInChildren<Image>();
        StartCoroutineFadeIn();
    }

    public void StartCoroutineFadeIn()
    {
        StartCoroutine(FadeInn());
        Invoke("DesactivarTransitionCanvas", transitionDuration);
    }

    //mostrar progresivamente una imagen
    private IEnumerator FadeInn()
    {
        float elapsedTime = 0f;
        Color startColor = transitionImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < transitionDuration)
        {
            transitionImage.color = Color.Lerp(startColor, endColor, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transitionImage.color = endColor;
    }

    private void DesactivarTransitionCanvas()
    {
        TransitionCanvas.SetActive(false);

        //si la escena actual es "SalidaTierra" activamos manualmente la camara y su script de moverse hacia arriba
        if (SceneManager.GetActiveScene().name == "SalidaTierra")
        {
            titulo.SetActive(true);
            Camera.main.GetComponent<MoverCamaraArriba>().enabled = true;
        }
    }
}
