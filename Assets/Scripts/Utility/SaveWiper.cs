using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveWiper : MonoBehaviour
{
    public static bool NewGame = false;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
