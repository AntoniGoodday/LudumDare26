using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OptionsScreen : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField]
    Slider slider;
    float volume = 0;

    public void SetVolume()
    {
        volume = slider.value;
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
