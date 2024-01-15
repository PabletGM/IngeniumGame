using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundSceneSecuencia4 : MonoBehaviour
{
    [Header("Sound SFX")]
    [SerializeField]
    private float volumeSFX1;
    [SerializeField]
    private float volumeSFX2;
    [SerializeField]
    private float volumeSFX3;

    [Header("Sound Music")]
    [SerializeField]
    private float volumeMusic;

    [Header("Sound Dialogue")]
    [SerializeField]
    private float volumeDialogue1;
    [SerializeField]
    private float volumeDialogue2;
    [SerializeField]
    private float volumeDialogue3;

    [Header("Sound Transition")]
    [SerializeField]
    private float volumeTransition;



    public static PlaySoundSceneSecuencia4 instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    #region SFX
    public void PlaySFX1(string sfx1)
    {
        AudioManagerSecuencia4.instance.PlaySFX1(sfx1,volumeSFX1);
    }

    public void PlaySFX2(string sfx2)
    {
        AudioManagerSecuencia4.instance.PlaySFX2(sfx2, volumeSFX2);
    }

    public void PlaySFX3(string sfx3)
    {
        AudioManagerSecuencia4.instance.PlaySFX3(sfx3, volumeSFX3);
    }

    public void ChangeVolumeSFX1(float volume)
    {
        volume = volumeSFX1;
    }

    public void ChangeVolumeSFX2(float volume)
    {
        volume = volumeSFX2;
    }

    public void ChangeVolumeSFX3(float volume)
    {
        volume = volumeSFX3;
    }

    public void StopAllSFX()
    {
        AudioManagerSecuencia4.instance.StopSFX();
    }

    public void stopSFX1(string sfx1)
    {
        AudioManagerSecuencia4.instance.StopSFX1();
    }

    public void stopSFX2(string sfx2)
    {
        AudioManagerSecuencia4.instance.StopSFX2();
    }

    public void stopSFX3(string sfx3)
    {
        AudioManagerSecuencia4.instance.StopSFX3();
    }



    #endregion


    //=================================================================================================


    #region Music
    public void PlayMusic(string music)
    {
        AudioManagerSecuencia4.instance.PlayMusic(music, volumeMusic);
    }

    public void ChangeVolumeMusic(float volume)
    {
        volume = volumeMusic;
    }

    public void StopAllMusic()
    {
        AudioManagerSecuencia4.instance.StopMusic();
    }
    #endregion



    //=================================================================================================

    #region Dialogue
    public void PlayDialogue1(string music)
    {
        AudioManagerSecuencia4.instance.PlayDialogue1(music, volumeDialogue1);
    }

    public void PlayDialogue2(string music)
    {
        AudioManagerSecuencia4.instance.PlayDialogue2(music, volumeDialogue2);
    }

    public void PlayDialogue3(string music)
    {
        AudioManagerSecuencia4.instance.PlayDialogue3(music, volumeDialogue3);
    }

    public void ChangeVolumeDialogue1(float volume)
    {
        volume = volumeDialogue1;
    }

    public void ChangeVolumeDialogue2(float volume)
    {
        volume = volumeDialogue2;
    }

    public void ChangeVolumeDialogue3(float volume)
    {
        volume = volumeDialogue3;
    }

    public void StopAllDialogue()
    {
        AudioManagerSecuencia4.instance.StopDialogue();
    }
    #endregion


    //=================================================================================================

    #region Transition

    public void PlayTransition(string music)
    {
        AudioManagerSecuencia4.instance.PlayTransition(music, volumeTransition);
    }

    public void ChangeVolumeTransition(float volume)
    {
        volume = volumeTransition;
    }

    public void StopAllTransition()
    {
        AudioManagerSecuencia4.instance.StopTransition();
    }
    #endregion
}
