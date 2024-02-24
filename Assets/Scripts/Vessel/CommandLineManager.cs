using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandLineManager : MonoBehaviour
{

    private EventSystem _eventSystem;
    private InputField _textBox;
    void Start()
    {
        _textBox = GetComponentInChildren<InputField>();
        _eventSystem = EventSystem.current;
    }

    void Update()
    {
        
    }

    public void StartCommandLine()
    {
        _eventSystem.SetSelectedGameObject(_eventSystem.gameObject);
    }

    public void CommandEntered(string command)
    {
        Debug.Log("Command Entered: " + command);     
    }
}
