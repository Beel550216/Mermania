using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeSettings : MonoBehaviour
{

    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    
    
    public const string Mixer_Music = "MusicVolume";
    public const string Mixer_SFX = "SFXVolume";

    void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.Music_Key, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_Key, 1f);

    }

    
    void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.Music_Key, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_Key, sfxSlider.value);
    }

    void SetMusicVolume(float value)
    {
        mixer.SetFloat(Mixer_Music, Mathf.Log10(value)*20);
    }

    void SetSFXVolume(float value)
    {
        mixer.SetFloat(Mixer_SFX, Mathf.Log10(value)*20);

    }

    
}
