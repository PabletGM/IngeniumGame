using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public float transitionDuration; // Duración de la transición en segundos
    private Image transitionImage;

    private void Start()
    {
        transitionImage = GetComponentInChildren<Image>();
    }

    public void TransitionToScene(string sceneName)
    {
        AudioManagerCirculos.instance.PlaySFX("fadeIn");
        StartCoroutine(FadeOut(sceneName));
       
    }

   

    //ocultar progresivamente una imagen
    private IEnumerator FadeOut(string sceneName)
    {
        float elapsedTime = 0f;
        Color startColor = transitionImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);
        
        while (elapsedTime < transitionDuration)
        {
            transitionImage.color = Color.Lerp(startColor, endColor, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transitionImage.color = endColor;
        SceneManager.LoadScene("circulosNave");
        //aparece la escena al cambiarla
        
    }
}
