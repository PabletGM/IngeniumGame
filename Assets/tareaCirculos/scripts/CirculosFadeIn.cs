using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CirculosFadeIn : MonoBehaviour
{
    public float transitionDuration; // Duración de la transición en segundos
    private Image transitionImage;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject gameManager;

    [SerializeField]
    private GameObject TransitionCanvas;
    // Start is called before the first frame update
    void Start()
    {
        transitionImage = GetComponentInChildren<Image>();
        StartCoroutineFadeIn();
        if(gameManager==null)
        {
            gameManager = GameObject.Find("GameManager");
        }
        
    }

    public void StartCoroutineFadeIn()
    {
        AudioManagerCirculos.instance.PlaySFX("fadeOut");
        StartCoroutine(FadeIn());
        Invoke("ActivarCanvas", 1.5f);
        Invoke("ActivarGameManager", 1.5f);
        Invoke("DesactivarTransitionCanvas", 1.5f); 


    }

    //mostrar progresivamente una imagen
    private IEnumerator FadeIn()
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

    private void ActivarCanvas()
    {
        canvas.SetActive(true);
    }

    private void ActivarGameManager()
    {
        gameManager.SetActive(true);
    }

    private void DesactivarTransitionCanvas()
    {
        TransitionCanvas.SetActive(false);
    }
}
