using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class yarnSpinnerTemporizador : MonoBehaviour
{
    [SerializeField]
    private float contadorPasarSiguienteTexto;

    [SerializeField]
    private LineView pasarSiguienteTexto;

    // Método principal que se ejecuta al iniciar el script
    void Start()
    {
        Debug.Log(pasarSiguienteTexto);
        // Iniciar la corrutina
        StartCoroutine(RealizarAccionPasarSiguienteTexto());
    }

    // Corrutina que realiza una acción cada 6 segundos
    IEnumerator RealizarAccionPasarSiguienteTexto()
    {
        while (true)
        {
            // Realizar la acción aquí
            pasarSiguienteTexto.OnContinueClicked();

            // Esperar 6 segundos antes de realizar la próxima acción
            yield return new WaitForSeconds(contadorPasarSiguienteTexto);
        }
    }
}
