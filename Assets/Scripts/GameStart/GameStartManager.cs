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
    public bool LockSubPosition = false;
    private void Awake()
    {
        Time.timeScale = 1.0f;
        if(Instance == null)
            Instance = this;

        IsGameStarted = false;
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

        if (VesselRoomHandler.Instance.ContainsRoom("SA"))
        {
            SaveWiper.NewGame = false;
            SaveManager.Instance.Load();

            if (GameContinued != null)
            {
                GameContinued();

            }
            CommandLineManager.Instance.OutputLine(CommandLineManager.StringToArray("Data Succesfully Loaded."), true);
        }
        else
        {
            SaveWiper.NewGame = true;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        
    }
}
