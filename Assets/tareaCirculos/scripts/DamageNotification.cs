using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNotification : MonoBehaviour
{
    public SpriteRenderer notificationSpriteRenderer;
    private float notificationDuration = 1.1f;
    private float fadeDuration = 1f; // Duración de la transición de opacidad en segundos.

    private Color startColor;
    private float startTime;

    private void Start()
    {
        notificationSpriteRenderer.gameObject.SetActive(false);
    }

    public void ShowDamageNotification()
    {
        startColor = notificationSpriteRenderer.color;
        startTime = Time.time;

        notificationSpriteRenderer.gameObject.SetActive(true);

        // Invoca el método HideDamageNotification después de la duración de la notificación.
        Invoke("HideDamageNotification", notificationDuration);
    }

    private void Update()
    {
        if (notificationSpriteRenderer.gameObject.activeSelf)
        {
            float elapsedTime = Time.time - startTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            notificationSpriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        }
    }

    private void HideDamageNotification()
    {
        notificationSpriteRenderer.gameObject.SetActive(false);
        // Restaurar la opacidad completa al ocultar.
        notificationSpriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
    }
}
