using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    public static AudioManager instance;
    public const string MASTER_KEY = "MasterVolume";
    public const string MUSIC_KEY = "MusicVolume";
    public const string SFX_KEY = "SFXVolume";

    private void Awake() {
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
        LoadVolume();
    }
    void LoadVolume(){

        float MasterVolume =PlayerPrefs.GetFloat(MASTER_KEY, 1f); 
        float MusicVolume =PlayerPrefs.GetFloat(MUSIC_KEY, 1f); 
        float SFXVolume =PlayerPrefs.GetFloat(SFX_KEY, 1f); 

        mixer.SetFloat(SettingManager.MIXER_MASTER, Mathf.Log10(MasterVolume)* 20);
        mixer.SetFloat(SettingManager.MIXER_MUSIC, Mathf.Log10(MusicVolume)* 20);
        mixer.SetFloat(SettingManager.MIXER_SFX, Mathf.Log10(SFXVolume)* 20);
    }
}
