using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandLineManager : MonoBehaviour
{

    private EventSystem _eventSystem;
    [SerializeField]
    private TMP_InputField _textBox;
    void Start()
    {
        _textBox = GetComponentInChildren<TMP_InputField>();
        _eventSystem = EventSystem.current;
    }

    private void OnEnable()
    {
        PlayerSubControls.openCommandLine += StartCommandLine;
    }
    private void OnDisable()
    {
        PlayerSubControls.openCommandLine -= StartCommandLine;

    }
    public void StartCommandLine()
    {
        Debug.Log("Command");
        _textBox.ActivateInputField();
    }

    public void CommandEntered(string command)
    {
        Debug.Log("Command Entered: " + command);     
    }
}
