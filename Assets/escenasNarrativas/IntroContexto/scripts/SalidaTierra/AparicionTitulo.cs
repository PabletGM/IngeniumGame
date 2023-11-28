using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AparicionTitulo : MonoBehaviour
{
    [SerializeField]
    private float transitionDuration;

    float elapsedTime = 0f;

    [SerializeField]
    private GameObject texto;
    //al ser active
    void OnEnable()
    {
        StartCoroutineAparecerTitulo();
    }

    public void StartCoroutineAparecerTitulo()
    {

        StartCoroutine(AparecerTitulo());
        TweenAumentarTamañoTitulo();
        Invoke("DesactivarTitulo", 5f);
    }

    public void TweenAumentarTamañoTitulo()
    {
        //1 segundo de tween 
        this.transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 1f);

    }

    //mostrar progresivamente una imagen
    private IEnumerator AparecerTitulo()
    {
        
        Color startColor = this.gameObject.GetComponent<TextMeshProUGUI>().color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < transitionDuration)
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().color = Color.Lerp(startColor, endColor, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        this.gameObject.GetComponent<TextMeshProUGUI>().color = endColor;
       
    }

    private void DesactivarTitulo()
    {
        this.gameObject.SetActive(false);
        //aparecer texto
        if (SceneManager.GetActiveScene().name == "SalidaTierra")
        {
            texto.SetActive(true);
        }

    }

}
