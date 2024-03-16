using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public delegate void StartOverGame();
    public static StartOverGame GameStarted;



    public void StartGame()
    {
        GameStarted();
    }
}
