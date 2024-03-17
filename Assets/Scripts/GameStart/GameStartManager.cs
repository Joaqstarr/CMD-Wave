using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    public delegate void StartOverGame();
    public static StartOverGame GameStarted;
    public static StartOverGame GameContinued;

    [SerializeField]
    private bool OverrideNewGame = false;

    public static GameStartManager Instance;
    public bool IsGameStarted = false;
    public bool LockSubPosition = true;
    private void Awake()
    {
        Time.timeScale = 1.0f;
        if(Instance == null)
            Instance = this;

        IsGameStarted = false;
        LockSubPosition = true;
    }

    public void StartGame()
    {
        if (OverrideNewGame)
        {
            IsGameStarted = true;
            LockSubPosition = false;

            return;
        }

        IsGameStarted = true;
        LockSubPosition = true;
        if (GameStarted != null)
            GameStarted();
    }

    public void Continue()
    {
        bool loaded = SaveManager.Instance.Load();

        if(!loaded )
        {
            if (GameContinued != null)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            if (GameContinued != null)
                GameContinued();
        }
    }
}
