using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    private CanvasGroup _canvasGroup;
    private bool _paused = false;
    void Start()
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
        Time.timeScale = 1;
    }
    public void Pause()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
        _paused = true;
        Time.timeScale = 0;
    }

    public bool IsPause
    {
        get { return _paused; }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
