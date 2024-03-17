using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControlSettingsUiManager : MonoBehaviour
{
    [SerializeField]
    private Slider _mouseSens;
    [SerializeField]
    private Toggle _togglePauseTyping;
    [SerializeField]
    private Toggle _colorBlindToggle;

    // Start is called before the first frame update
    void Start()
    {

        _mouseSens.value = PlayerPrefs.GetFloat("MouseSens", 0.8f);
        _togglePauseTyping.isOn = PlayerPrefs.GetInt("PauseType", 0) == 1;
        _colorBlindToggle.isOn = PlayerPrefs.GetInt("Colorblind", 0) == 1;

    }


    public void SetSFXVol(float value)
    {
        PlayerPrefs.SetFloat("MouseSens", value);
        PlayerControls.MouseSens = value;

    }

    public void SetPauseWhenTyping(bool value)
    {
        int val = value ? 1 : 0;
        PlayerPrefs.SetInt("PauseType", val);

        PauseManager.Instance._pauseWhenTyping = value;
    }

    public void SetColorblind(bool value)
    {
        int val = value ? 1 : 0;
        PlayerPrefs.SetInt("Colorblind", val);
        PlayerSubData.Colorblind = value;
    }

}
