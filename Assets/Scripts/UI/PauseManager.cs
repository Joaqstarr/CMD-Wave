using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    private CanvasGroup _canvasGroup;
    private bool _paused = false;

    private float _pauseTimeScale = 1f;
    private float _timeScalar = 1f;

    public bool _pauseWhenTyping;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        _canvasGroup = GetComponent<CanvasGroup>();

    }

    public void TogglePause()
    {
        if (_paused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    public void Unpause()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _paused = false;
        _pauseTimeScale = 1;
    }
    public void Pause()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
        _paused = true;
        _pauseTimeScale = 0;
    }

    public bool IsPause
    {
        get { return _paused; }
    }

    public void Quit()
    {
        Application.Quit();
    }
    private void Update()
    {

        
        if (_pauseWhenTyping)
        {
            _timeScalar = CommandLineManager.Instance.IsTyping ? 0 : 1;
        }
        else
        {
            _timeScalar = 1;
        }
        Time.timeScale = _timeScalar * _pauseTimeScale;
    }
}
