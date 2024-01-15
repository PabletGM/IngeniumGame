using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerSecuencia4 : MonoBehaviour
{
    public static AudioManagerSecuencia4 instance;
    public SoundSecuencia4[] musicSounds, sfxSounds, dialogueSounds, transitionSounds;
    public AudioSource musicSource, sfxSource1, sfxSource2, sfxSource3, dialogueSource, dialogueSource2, dialogueSource3, transitionSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void Start()
    {
        
    }


    public void PlayMusic(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundSecuencia4 s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            musicSource.volume = volume;
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    //primer sonido de dialogo
    public void PlayDialogue1(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundSecuencia4 s = Array.Find(dialogueSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            dialogueSource.clip = s.clip;
            dialogueSource.volume = volume;
            dialogueSource.Play();
        }
    }

    //segundo sonido de dialogo
    public void PlayDialogue2(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundSecuencia4 s = Array.Find(dialogueSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            dialogueSource2.clip = s.clip;
            dialogueSource2.volume = volume;
            dialogueSource2.Play();
        }
    }

    //tercer sonido de dialogo
    public void PlayDialogue3(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundSecuencia4 s = Array.Find(dialogueSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            dialogueSource3.clip = s.clip;
            dialogueSource3.volume = volume;
            dialogueSource3.Play();
        }
    }

    //primer sonido VFX
    public void PlaySFX1(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundSecuencia4 s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource1.volume = volume;
            sfxSource1.PlayOneShot(s.clip);
        }
    }

    //SEGUNDO sonido VFX
    public void PlaySFX2(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundSecuencia4 s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource2.volume = volume;
            sfxSource2.PlayOneShot(s.clip);
        }
    }

    //tercer sonido VFX
    public void PlaySFX3(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundSecuencia4 s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource3.volume = volume;
            sfxSource3.PlayOneShot(s.clip);
        }
    }

    //primer sonido TRANSITION
    public void PlayTransition(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundSecuencia4 s = Array.Find(transitionSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            transitionSource.volume = volume;
            transitionSource.PlayOneShot(s.clip);
        }
    }

    public void StopSFX()
    {
        sfxSource1.Stop();
        sfxSource2.Stop();
        sfxSource3.Stop();
    }

    public void StopSFX1()
    {
        sfxSource1.Stop();      
    }

    public void StopSFX2()
    {
        sfxSource2.Stop();
    }

    public void StopSFX3()
    {
        sfxSource3.Stop();
    }



    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void StopDialogue()
    {
        dialogueSource.Stop();
        dialogueSource2.Stop();
        dialogueSource3.Stop();
    }

    public void StopTransition()
    {
        transitionSource.Stop();
       
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource1.mute = !sfxSource1.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume1(float volume)
    {
        sfxSource1.volume = volume;
    }

    public void SFXVolume2(float volume)
    {
        sfxSource2.volume = volume;
    }

    public void SFXVolume3(float volume)
    {
        sfxSource3.volume = volume;
    }

    public void DialogueVolume(float volume)
    {
        dialogueSource.volume = volume;
    }

    public void DialogueVolume2(float volume)
    {
        dialogueSource2.volume = volume;
    }

    public void DialogueVolume3(float volume)
    {
        dialogueSource3.volume = volume;
    }

    public void TransitionVolume(float volume)
    {
        transitionSource.volume = volume;
    }





    #region FuncionalidadExclusiva_Secuencia4
    //sonidos de canvas como botones se llaman desde aqui
    public void BotonDialogos()
    {
        PlaySFX1("clickButton", 1f);
    }

    public void PulsarBotonSound()
    {
        //sonido pala golpe al acabar animacion
        PlaySFX1("clickButton", 1f);
    }


    #endregion
}
