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


    [SerializeField]
    private GameObject nave;
    void OnEnable()
    {
        StartCoroutineAparecerTitulo();
    }

    public void StartCoroutineAparecerTitulo()
    {

        StartCoroutine(AparecerTitulo());
        TweenAumentarTamañoTitulo();
        //Invoke("TweenDisminuirTamañoTitulo", 3f);
        Invoke("CorrutinaDesaparecerTitulo", 3f);
        Invoke("DesactivarTitulo", 6f);


    }

    public void CorrutinaDesaparecerTitulo()
    {
        StartCoroutine("DesaparecerTitulo");
    }

    public void TweenAumentarTamañoTitulo()
    {
        //1 segundo de tween 
        this.transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 1f);

    }
    public void TweenDisminuirTamañoTitulo()
    {
        //1 segundo de tween 
        this.transform.DOScale(new Vector3(0.001f, 0.001f, 0.001f), 1f);

    }

    //mostrar progresivamente una imagen
    private IEnumerator AparecerTitulo()
    {
        
        Color startColor = this.gameObject.GetComponent<TextMeshProUGUI>().color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < transitionDuration/2)
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().color = Color.Lerp(startColor, endColor, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        this.gameObject.GetComponent<TextMeshProUGUI>().color = endColor;

        
    }

    private IEnumerator DesaparecerTitulo()
    {

        Color startColor = this.gameObject.GetComponent<TextMeshProUGUI>().color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < transitionDuration)
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().color = Color.Lerp(startColor, endColor, elapsedTime / transitionDuration);
            elapsedTime += (0.05f+ Time.deltaTime);
            yield return null;
        }

        this.gameObject.GetComponent<TextMeshProUGUI>().color = endColor;

    }

    private void DesactivarTitulo()
    {
        //mover naves hacia arriba que suban, activarlas
        nave.SetActive(true);

        //ruido cohete despegando
        AudioManagerIntro.instance.PlaySFX("cohete");
        //tras acabar animacion
        Invoke("DesactivarNaves", 25f);

        this.gameObject.SetActive(false);
        
        //aparecer texto
        if (SceneManager.GetActiveScene().name == "SalidaTierra")
        {
            texto.SetActive(true);
        }

    }

   

    private void DesactivarNaves()
    {
        nave.SetActive(false);
    }

}
