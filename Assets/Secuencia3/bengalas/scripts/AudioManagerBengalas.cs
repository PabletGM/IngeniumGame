using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManagerBengalas : MonoBehaviour
{
    public static AudioManagerBengalas instance;
    public SoundBengalas[] musicSounds, sfxSounds, dialogueSounds;
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
       //PlayMusicNature();
    }

    public void PlayMusicNature()
    {
        PlayMusic("nature",1f);
    }

    public void PlayMusic(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundBengalas s = Array.Find(musicSounds, x => x.name == name);

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

    public void PlayDialogue(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundBengalas s = Array.Find(dialogueSounds, x => x.name == name);

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

    public void PlaySFX(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundBengalas s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource.volume = volume;
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void StopSFX()
    {
        sfxSource.Stop();      
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }


    //sonidos de canvas como botones se llaman desde aqui

    public void PulsarBotonSound()
    {
        //sonido pala golpe al acabar animacion
        PlaySFX("clickButton",1f);
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

    public void DialogueVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void BotonDialogos()
    {
        PlaySFX("clickButton", 1f);
    }
}
