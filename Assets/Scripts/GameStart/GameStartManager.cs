using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public delegate void StartOverGame();
    public static StartOverGame GameStarted;

    [SerializeField]
    private bool OverrideNewGame = false;

    public static GameStartManager Instance;
    public bool IsGameStarted = false;
    public bool LockSubPosition = true;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        IsGameStarted = false;
        LockSubPosition = true;
    }

    public void StartGame()
    {
        if (OverrideNewGame) return;

        IsGameStarted = true;
        LockSubPosition = true;
        if (GameStarted != null)
            GameStarted();
    }
}
