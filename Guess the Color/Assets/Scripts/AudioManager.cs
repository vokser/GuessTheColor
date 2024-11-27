using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sprite _soundOnIcon;
    [SerializeField] private Sprite _soundOffIcon;
    [SerializeField] private Image _soundToggleButtonIcon;
    [SerializeField] private Sprite _musicOnIcon;
    [SerializeField] private Sprite _musicOffIcon;
    [SerializeField] private Image _musicToggleButtonIcon;
    [SerializeField] private AudioSource _tapSound;
    [SerializeField] private AudioSource _backgroundMusic;

    private bool isSoundEnabled = true;
    private bool isMusicEnabled = true;


    private void Awake()
    {
        LoadSoundSettings();
        LoadMusicSettings();
    }
  
    private void SaveSoundSettings()
    {
        PlayerPrefs.SetInt("SoundEnabled", isSoundEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void SaveMusicSettings()
    {
        PlayerPrefs.SetInt("MusicEnabled", isMusicEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateSoundIcon()
    {
        if (_soundToggleButtonIcon != null)
            _soundToggleButtonIcon.sprite = isSoundEnabled ? _soundOnIcon : _soundOffIcon;
    }

    private void UpdateMusicIcon()
    {
        if (_musicToggleButtonIcon != null)
            _musicToggleButtonIcon.sprite = isMusicEnabled ? _musicOnIcon : _musicOffIcon;
    }

    private void LoadSoundSettings()
    {
        isSoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        float volume = isSoundEnabled ? 1 : 0;
        _tapSound.volume = volume;
        UpdateSoundIcon();

    }

    private void LoadMusicSettings()
    {
        isMusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        float volume = isMusicEnabled ? 1 : 0;
        _backgroundMusic.volume = volume;
        UpdateMusicIcon();
    }

    public void PlayButtonSound()
    {
        if (_tapSound != null && _tapSound.isActiveAndEnabled)
        {
            _tapSound.Play();
        }
    }

    private void ToggleSound()
    {
        isSoundEnabled = !isSoundEnabled;
        float volume = isSoundEnabled ? 1 : 0;
        _tapSound.volume = volume;
        SaveSoundSettings();
        UpdateSoundIcon();
    }

    private void ToggleMusic()
    {
        isMusicEnabled = !isMusicEnabled;
        float volume = isMusicEnabled ? 1 : 0;
        _backgroundMusic.volume = volume;
        SaveMusicSettings();
        UpdateMusicIcon();
    }
}
