using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _mainMixer;
    [SerializeField]
    private Slider _masterSlider;
    [SerializeField]
    private Slider _environmentSlider;
    [SerializeField]
    private Slider _interactionsSlider;
    [SerializeField]
    private Slider _musicSlider;
    // Start is called before the first frame update
    void Start()
    {
        SetVolumeSliders();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolumeSliders() {
        float currentVolume;
        _mainMixer.GetFloat("masterVolume", out currentVolume);
        _masterSlider.value = Mathf.Pow(10, currentVolume / 20);
        _mainMixer.GetFloat("environmentVolume", out currentVolume);
        _environmentSlider.value = Mathf.Pow(10, currentVolume / 20);
        _mainMixer.GetFloat("interactionsVolume", out currentVolume);
        _interactionsSlider.value = Mathf.Pow(10, currentVolume / 20);
        _mainMixer.GetFloat("musicVolume", out currentVolume);
        _musicSlider.value = Mathf.Pow(10, currentVolume / 20);
    }

    //Volume
    public void SetMasterVolume(float volume) {
        _mainMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetEnvironmentVolume(float volume) {
        _mainMixer.SetFloat("environmentVolume", Mathf.Log10(volume) * 20);
    }

    public void SetInteractionsVolume(float volume) {
        _mainMixer.SetFloat("interactionsVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume) {
        _mainMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
    }
}
