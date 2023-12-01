using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlanetaHabitableComportamiento : MonoBehaviour
{

    #region ZoomIn
        public bool ZoomActive;
        public Camera cam;
        public float speed;
    #endregion

    #region FadeIn
        public float transitionDuration; // Duración de la transición en segundos
        [SerializeField]
        private Image transitionImage;
    #endregion

    public void ZoomIn()
    {
        ZoomActive = true;
        StartCoroutine(FadeInn());
        Debug.Log("Detectado");
    }



    public void LateUpdate()
    {
        if (ZoomActive)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 1.2f, speed);
            
        }
       
    }

    private IEnumerator FadeInn()
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
    }

    public void PasarSiguienteEscenaIntermedia()
    {
        Invoke("PasarSiguienteEscenaLogo", 3f);
    }

    public void PasarSiguienteEscenaLogo()
    {


        if (SceneManager.GetActiveScene().name == "LlegadaPlaneta")
        {
            SceneManager.LoadScene("LogoTitulo");
        }
    }

}
