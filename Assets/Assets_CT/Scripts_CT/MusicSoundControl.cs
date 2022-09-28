using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSoundControl : MonoBehaviour
{
    [SerializeField] Slider MusicVolumeSlider;
    private float baslangıcVolume;
    void Start()
    {
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("musicvolume");
    }

    void Update()
    {

    }

    public void VolumeSliderChanged()
    {
        PlayerPrefs.SetFloat("musicvolume", MusicVolumeSlider.value);
        AudioListener.volume = MusicVolumeSlider.value;
    }
}
