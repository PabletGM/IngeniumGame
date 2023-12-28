using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{

    //array de textos
    [SerializeField]
    private GameObject[] textos;

    [SerializeField]
    private GameObject DialoguePanel;

    private int numMaxTextos = 2;

    private int numTextoActual = 0; //puede ser el 0,1 o 2

    private float tiempoEsperaEntreTextos = 6f;

    [SerializeField]
    private int tiempoNarracionTexto1;
    [SerializeField]
    private int tiempoNarracionTexto2;
    [SerializeField]
    private int tiempoNarracionTexto3;
    [SerializeField]
    private int tiempoNarracionTexto4;

    [SerializeField]
    private Animator robotAnim;



    private void Awake()
    {
        numMaxTextos = textos.Length;
        Debug.Log(numMaxTextos);
    }
    //metodo que quita todos los textos menos el que elijas
    public void PonerTextoActivo(GameObject textoElegido)
    {
        //quita todos los textos 
        for(int i=0; i<numMaxTextos; i++)
        {
            if (textos[i] == textoElegido)
            {
                textos[i].SetActive(true);
            }
            else
            {
                textos[i].SetActive(false);
            }
            
        }
    }

    private void OnEnable()
    {
        //comenzar dialogos y textos
        Invoke("StartTextDialogue", 0f);
    }

    private void StartTextDialogue()
    {
        if (numTextoActual < textos.Length && DialoguePanel.activeInHierarchy)
        {

            #region Secuencia 1
            //si escena actual es SalidaTierra
            if (SceneManager.GetActiveScene().name =="SalidaTierra")
            {
                if (numTextoActual == 0)
                {
                    //si es texto 1,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    AudioManagerIntro.instance.PlayDialogue("dialogueNarracion1SalidaTierra", tiempoNarracionTexto1);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto1);
                    
                    
                }
                else if (numTextoActual == 1)
                {
                    //si es texto 2,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    AudioManagerIntro.instance.PlayDialogue("dialogueNarracion2SalidaTierra", tiempoNarracionTexto2);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto2);
                    
                }
                else if (numTextoActual == 2)
                {
                    //si es texto 1,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    AudioManagerIntro.instance.PlayDialogue("dialogueNarracion3SalidaTierra", tiempoNarracionTexto3);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto3);
                    
                }
            }
            else if(SceneManager.GetActiveScene().name == "viajeGalaxia")
            {
                AudioManagerIntro.instance.PlayDialogue("dialogueNarracion4ViajeGalaxia", tiempoNarracionTexto1);
                Invoke("PasarSiguienteTexto", tiempoNarracionTexto1);
            }
            #endregion

            #region Secuencia 2
            if (SceneManager.GetActiveScene().name == "escenaIntro")
            {
                //primer texto
                if (numTextoActual == 0)
                {
                    //si es texto 1,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    AudioManagerCirculos.instance.PlayDialogue("robotMinimalista1", tiempoNarracionTexto1);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto1);
                }

                //segundo texto
                else if (numTextoActual == 1)
                {
                    //si es texto 2,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    AudioManagerCirculos.instance.PlayDialogue("robotMinimalista2", tiempoNarracionTexto2);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto2);

                }
            }
            else if (SceneManager.GetActiveScene().name == "escenaConversacionRobot2")
            {
                //primer texto
                if (numTextoActual == 0)
                {
                    //si es texto 1,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    AudioManagerCirculos.instance.PlayDialogue("robotMinimalista1", tiempoNarracionTexto1);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto1);
                }

                //segundo texto
                else if (numTextoActual == 1)
                {
                    //si es texto 2,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    AudioManagerCirculos.instance.PlayDialogue("robotMinimalista3", tiempoNarracionTexto2);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto2);

                }
            }
            else if (SceneManager.GetActiveScene().name == "escenaConversacionRobot3")
            {
                //primer texto
                if (numTextoActual == 0)
                {
                    //si es texto 1,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    AudioManagerCirculos.instance.PlayDialogue("robotMinimalista1", tiempoNarracionTexto1);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto1);
                }

                //segundo texto
                else if (numTextoActual == 1)
                {
                    //si es texto 2,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    AudioManagerCirculos.instance.PlayDialogue("robotMinimalista3", tiempoNarracionTexto2);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto2);

                }
            }
            else if (SceneManager.GetActiveScene().name == "escenaConversacionRobot4")
            {
                //primer texto
                if (numTextoActual == 0)
                {
                    //si es texto 1,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    AudioManagerCirculos.instance.PlayDialogue("robotMinimalista1", tiempoNarracionTexto1);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto1);
                }
            }
            else if (SceneManager.GetActiveScene().name == "escenaConversacionRobot5")
            {
                //primer texto
                if (numTextoActual == 0)
                {
                    //si es texto 1,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    AudioManagerCirculos.instance.PlayDialogue("robotMinimalista1", tiempoNarracionTexto1);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto1);
                }
            }
            #endregion

            #region Secuencia 3
            if (SceneManager.GetActiveScene().name == "3.2ConversacionJefeExploracion")
            {
                //primer texto
                if (numTextoActual == 0)
                {
                    //si es texto 1,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    //AudioManagerCirculos.instance.PlayDialogue("robotMinimalista1", tiempoNarracionTexto1);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto1);
                }

                //segundo texto
                else if (numTextoActual == 1)
                {
                    //si es texto 2,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    //AudioManagerCirculos.instance.PlayDialogue("robotMinimalista2", tiempoNarracionTexto2);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto2);

                }

                //segundo texto
                else if (numTextoActual == 2)
                {
                    //si es texto 2,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    //AudioManagerCirculos.instance.PlayDialogue("robotMinimalista2", tiempoNarracionTexto2);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto3);

                }

                //segundo texto
                else if (numTextoActual == 3)
                {
                    //si es texto 2,empezamos la voz, pasamos el tiempoEsperaEntreTextos
                    //audioManager texto narracion 1
                    //AudioManagerCirculos.instance.PlayDialogue("robotMinimalista2", tiempoNarracionTexto2);
                    Invoke("PasarSiguienteTexto", tiempoNarracionTexto4);

                }
            }
            #endregion
        }
    }


    public void  PasarSiguienteTexto()
    {
        //provisional hasta tener AudioManager generico
        if(AudioManagerBengalas.instance!=null)
        {
            AudioManagerBengalas.instance.BotonDialogos();
        }
        
        //sumamos uno TODO: <= EN VEZ DE <
        if (numTextoActual< textos.Length-1 && DialoguePanel.activeInHierarchy)
        {
            //efectos penultimo dialogo
            if (numTextoActual < textos.Length - 2)
            {
                EfectosSegunEscenaPenultimoDialogo();
            }
            numTextoActual++;
            //llamas otra vez a nuevo dialogo
            StartTextDialogue();
        }
        //si ha llegado al final
        else
        {
            //antes de quitar texto vemos los efectos
            Invoke("EfectosSegunEscenaUltimoDialogo", 1f);
            Invoke("QuitarTexto", 2f);
            
            //animacion desaparecer robot
            
            if(robotAnim!=null)
            {
                SetAnimRobotDesaparecer();
            }

            Invoke("PasarSiguienteEscenaIntermedia", 1.5f);         
        }
       
        //ponemos texto
        if(DialoguePanel.activeInHierarchy)
        {
            PonerTextoActivo(textos[numTextoActual]);
        }
       
    }

    private void EfectosSegunEscenaUltimoDialogo()
    {
        if (SceneManager.GetActiveScene().name == "3.2ConversacionJefeExploracion")
        {
            
            //efecto empequeñecer dialogo
            UIManagerDialogue.instance.SetDialoguePanelPequeno();
            
        }
    }

    private void EfectosSegunEscenaPenultimoDialogo()
    {
        if (SceneManager.GetActiveScene().name == "3.2ConversacionJefeExploracion")
        {
            UIManagerDialogue.instance.RobotPopUpAparecer();
            //efecto robot pop-up

        }
    }

    private void QuitarTexto()
    {
        //quitamos texto
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

    private void SetAnimRobotDesaparecer()
    {
        Debug.Log(robotAnim);
        robotAnim.SetBool("aparecer", false);
    }

    public void VolverAnteriorTexto()
    {
        //quitamos uno si esta active el gameObject
        if(numTextoActual>0&& DialoguePanel.activeInHierarchy)
        {
            numTextoActual--;
        }
        
        //ponemos texto
        if(DialoguePanel.activeInHierarchy)
        {
            PonerTextoActivo(textos[numTextoActual]);
        }
        
    }

    public void PasarSiguienteEscenaIntermedia()
    {
        #region Secuencia 1
        if (SceneManager.GetActiveScene().name == "SalidaTierra")
        {
            //cargas escena intermedia
            SceneManager.LoadScene("ExplosionTierra");
        }

        else if(SceneManager.GetActiveScene().name == "LlegadaPlaneta")
        {
            SceneManager.LoadScene("LogoTitulo");
        }
        else if (SceneManager.GetActiveScene().name == "viajeGalaxia")
        {
            SceneManager.LoadScene("LlegadaPlaneta");
        }
        #endregion

        #region Secuencia 2
        if (SceneManager.GetActiveScene().name == "escenaIntro")
        {
            //cargas escena intermedia
            SceneManager.LoadScene("escenaRadar3D");
        }
        else if (SceneManager.GetActiveScene().name == "escenaConversacionRobot2")
        {
            //cargas escena intermedia
            SceneManager.LoadScene("tareaCaras2");
        }
        else if (SceneManager.GetActiveScene().name == "escenaConversacionRobot3")
        {
            //cargas escena intermedia
            SceneManager.LoadScene("escenaItem");
        }
        else if (SceneManager.GetActiveScene().name == "escenaConversacionRobot4")
        {
            //cargas escena intermedia
            SceneManager.LoadScene("escenaItemAutonomia2");
        }
        else if (SceneManager.GetActiveScene().name == "escenaConversacionRobot5")
        {
            //cargas escena intermedia
            SceneManager.LoadScene("aterrizajePlaneta");
        }
        #endregion

        #region Secuencia3
        if (SceneManager.GetActiveScene().name == "3.2ConversacionJefeExploracion")
        {
            //efecto empequeñecer dialogo
            //cargas siguiente escena
            SceneManager.LoadScene("3.3AdentrarseBosque");
        }
        #endregion
    }




}
