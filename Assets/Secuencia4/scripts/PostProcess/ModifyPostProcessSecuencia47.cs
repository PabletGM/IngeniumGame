using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ModifyPostProcessSecuencia47 : MonoBehaviour
{
    public Volume _ppv;
    private Vignette _vignette;

    [Header("Parameters Change Vignette")]
    [SerializeField]
    private float initialIntensityVignette; // Intensidad inicial del vignette
    [SerializeField]
    private float finalIntensityVignette;   // Intensidad final del vignette
    [SerializeField]
    private float changeDuration;         // Duración de cada cambio en segundos

    void Start()
    {
        //asociamos vignette
        _ppv.profile.TryGet(out _vignette);
        

        // Iniciar las corrutinas para subir y bajar la intensidad del vignette
        SubirIntensidadVignette();

    }

    private void SubirIntensidadVignette()
    {
        StartCoroutine(ChangeVignetteIntensity(initialIntensityVignette, finalIntensityVignette));
        Invoke("BajarIntensidadVignette", changeDuration);
    }

    private void BajarIntensidadVignette()
    {
        StartCoroutine(ChangeVignetteIntensity(finalIntensityVignette, initialIntensityVignette));
        Invoke("SubirIntensidadVignette", changeDuration);
    }

    // Corrutina para cambiar gradualmente la intensidad del vignette
    IEnumerator ChangeVignetteIntensity(float startIntensity, float endIntensity)
    {
        float elapsedTime = 0f;
        float currentIntensity = startIntensity;

        while (elapsedTime < changeDuration)
        {
            currentIntensity = Mathf.Lerp(startIntensity, endIntensity, elapsedTime / changeDuration);
            _vignette.intensity.value = currentIntensity;

            elapsedTime += Time.deltaTime;
            yield return null; // Esperar al siguiente frame
        }

        // Asegurar que la intensidad final sea la correcta
        _vignette.intensity.value = endIntensity;
    }


}
