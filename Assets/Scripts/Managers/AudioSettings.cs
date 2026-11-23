using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    private float aux;

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void SetMasterVolume( float volume)
    {
        audioMixer.SetFloat("Master", MathF.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void SetMusicVolume(float volume) 
    {
        audioMixer.SetFloat("Music", MathF.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", MathF.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
