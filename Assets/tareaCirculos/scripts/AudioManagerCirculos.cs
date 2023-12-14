using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerCirculos : MonoBehaviour
{
    public static AudioManagerCirculos instance;
    public SoundCirculos[] musicSounds, sfxSounds, dialogueSound;
    public AudioSource musicSource, sfxSource, dialogueSource;

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
        PlayMusicNature();
    }

    public void PlayMusicNature()
    {
        PlayMusic("music");
    }

    public void PlayMusic(string name)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundCirculos s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundCirculos s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlaySFXDuracion(string name, float duracion)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundCirculos s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            //bajamos volumen de musica normal
            musicSource.volume = 0.2f;
            sfxSource.PlayOneShot(s.clip);
            Invoke("PonerVolumenNormal", duracion);
        }
    }

    public void PlayDialogue(string name, int duracionDialogue)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundCirculos s = Array.Find(dialogueSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            //bajamos volumen de musica normal
            musicSource.volume = 0.2f;
            dialogueSource.PlayOneShot(s.clip);
            Invoke("PonerVolumenNormal", duracionDialogue);
        }
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }




    //sonidos de canvas como botones se llaman desde aqui

    public void PulsarBotonSound()
    {
        //sonido pala golpe al acabar animacion
        PlaySFX("boton");
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
