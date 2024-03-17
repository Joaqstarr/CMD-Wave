using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolManager : MonoBehaviour
{
    [SerializeField]
    private Slider _sfxSlider;
    [SerializeField]
    private Slider _musicSlider;
    [SerializeField]
    private AudioMixer _audioMixer;
    // Start is called before the first frame update
    void Start()
    {

        _sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1);
        _musicSlider.value = PlayerPrefs.GetFloat("Music", 1);

    }


    public void SetSFXVol(float vol)
    {
        PlayerPrefs.SetFloat("SFX", vol);

        vol *= 80;
        vol -= 80;
        _audioMixer.SetFloat("SFXVol", vol);

    }

    public void SetMusicVol(float vol)
    {
        PlayerPrefs.SetFloat("Music", vol);
        vol *= 80;
        vol -= 80;
        _audioMixer.SetFloat("MUSICVol", vol);
    }
}
