using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject reproductor;

    [SerializeField]
    private GameObject panelDiscosNormal;

    [SerializeField]
    private GameObject panelDiscosGrandePequeño;


    [SerializeField]
    private GameObject menuInicio;

    [SerializeField]
    private Button PanelbotonPlayVideoCorrectWin;

    [SerializeField]
    private Button PanelbotonPlayVideoIncorrect;

    [SerializeField]
    private GameObject portadaVideoPanelDiscosNormal;

    [SerializeField]
    private GameObject portadaVideoPanelDiscosGrandePequeño;

    [SerializeField]
    private GameObject marcoVideoCorrect;

    [SerializeField]
    private GameObject marcoVideoIncorrect;

    //color del buton
    private Color colorButton;

    [SerializeField]
    private GameObject videoCorrect;

    [SerializeField]
    private GameObject videoIncorrect;

    private VideoPlayer videoPlayerCorrect;
    private VideoPlayer videoPlayerIncorrect;


    bool verVideoIncorrect = false;
    bool verVideoCorrectWin = false;

    private void Start()
    {
        colorButton = PanelbotonPlayVideoIncorrect.image.color;
        videoPlayerCorrect = videoCorrect.GetComponent<VideoPlayer>();
        videoPlayerIncorrect = videoIncorrect.GetComponent<VideoPlayer>();
    }

    //al hacer click en boton video disco sobre otro correcto del menuInicio
    public void OnClickDiscoSobreOtroCorrecto()
    {
        //cerramos menu inicio
        menuInicio.SetActive(false);
        //abrimos reproductor
        reproductor.SetActive(true);
        //activamos panel discos normal para poner video
        panelDiscosNormal.SetActive(true);
        //desactivamos el otro panel de video
        panelDiscosGrandePequeño.SetActive(false);
    }

    //al hacer click en boton video disco sobre otro incorrect del menuInicio
    public void OnClickDiscoSobreOtroIncorrect()
    {
        //cerramos menu inicio
        menuInicio.SetActive(false);
        //abrimos reproductor
        reproductor.SetActive(true);
        //activamos panel discos incorrect para poner video
        panelDiscosGrandePequeño.SetActive(true);
        //desactivamos el otro panel de video
        panelDiscosNormal.SetActive(false);
    }

    public void SetVideoIncorrect()
    {
        //cambiamos valor de estado
        verVideoIncorrect =! verVideoIncorrect;
        //este ocupa todo el video y es un panel que la opacidad es 0 al darle a play
        //si se esta viendo el video
        if(verVideoIncorrect)
        {
            //damos a Play el video
            videoPlayerIncorrect.Play();
            PanelbotonPlayVideoIncorrect.image.color = new Color(255f, 0f, 0f, 0f);
            //quitamos portada
            portadaVideoPanelDiscosGrandePequeño.SetActive(false);
        }
        //sino se esta viendo el video
        else
        {
            videoPlayerIncorrect.Stop();
            PanelbotonPlayVideoIncorrect.image.color = colorButton;
            //activamos portada
            portadaVideoPanelDiscosGrandePequeño.SetActive(true);
        }
        
    }

    public void SetVideoCorrect()
    {
        //cambiamos valor de estado
        verVideoCorrectWin = !verVideoCorrectWin;
        //este ocupa todo el video y es un panel que la opacidad es 0 al darle a play
        //si se esta viendo el video
        if (verVideoCorrectWin)
        {
            //damos a Play el video
            videoPlayerCorrect.Play();
            PanelbotonPlayVideoCorrectWin.image.color = new Color(255f, 0f, 0f, 0f);
            //quitamos portada
            portadaVideoPanelDiscosNormal.SetActive(false);
        }
        //sino se esta viendo el video
        else
        {
            videoPlayerCorrect.Stop();
            PanelbotonPlayVideoCorrectWin.image.color = colorButton;
            //ponemos portada
            portadaVideoPanelDiscosNormal.SetActive(true);
        }

    }

    public void BackToMenuInicio()
    {
        //paramos ambos videos
        videoPlayerCorrect.Stop();
        videoPlayerIncorrect.Stop();
        //abrimos menu inicio
        menuInicio.SetActive(true);
        //cerramos reproductor
        reproductor.SetActive(false);
        //desactivamos panel discos incorrect para poner video
        panelDiscosGrandePequeño.SetActive(false);
        //desactivamos el otro panel de video
        panelDiscosNormal.SetActive(false);
    }

    public void IniciarPartida()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(1, UnityEngine.SceneManagement.LoadSceneMode.Single);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Hanoi");
    }
}
