using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{   
    [SerializeField] private AudioSource _tapSound;
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private AudioSource _winSound;
    [SerializeField] private AudioSource _loseSound;

    private bool isSoundEnabled = true;
    private bool isMusicEnabled = true;


    private void Awake()
    {
        LoadSoundSettings();
        LoadMusicSettings();
    }   

    private void LoadSoundSettings()
    {
        isSoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        float volume = isSoundEnabled ? 1 : 0;
        _tapSound.volume = volume;
        _winSound.volume = volume;
        _loseSound.volume = volume;
    }

    private void LoadMusicSettings()
    {
        isMusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        float volume = isMusicEnabled ? 1 : 0;
        _backgroundMusic.volume = volume;       
    }

    public void PlayButtonSound()
    {
        if (_tapSound != null && _tapSound.isActiveAndEnabled)
        {
            _tapSound.Play();
        }
    }

    public void PlayWinSound()
    {
        if (_winSound != null && _winSound.isActiveAndEnabled)
        {
            _winSound.Play();
        }
    }

    public void PlayLoseSound()
    {
        if (_loseSound != null && _loseSound.isActiveAndEnabled)
        {
            _loseSound.Play();
        }
    }
   
}
