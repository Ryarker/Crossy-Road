using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class SettingManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    

    
    public const string MIXER_MUSIC = "Music Volume";
    public const string MIXER_MASTER = "Master Volume";
    public const string MIXER_SFX = "SFX Volume";

    private void Awake() {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start() {
        masterSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 1f);
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
        
        
    }
    void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MASTER_KEY, masterSlider.value);
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }
    private void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, MathF.Log10(value)* 20);
    }
    private void SetMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER, MathF.Log10(value)* 20);
    }
    
    private void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, MathF.Log10(value)* 20);
    }

    public void MuteHandler (bool mute) {
        if(mute){
            AudioListener.volume = 1e-06f;
        }else
        {
            AudioListener.volume = 1;
        }
    }
    
}
