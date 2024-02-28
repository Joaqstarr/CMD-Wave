using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class CommandContext
{
    [SerializeField]
    List<CommandBase> _commands = new List<CommandBase>();

    public string[] CheckAndExecuteCommand(string command, out bool shouldClear)
    {

        for (int i = 0; i < _commands.Count; i++)
        { 
            if (_commands[i].CheckCommand(command))
            {
                shouldClear = _commands[i].ShouldClear;
                return _commands[i].Execute();

            }
        }
        shouldClear = false;

        return null;
    }

}
public class CommandLineManager : MonoBehaviour
{

    private EventSystem _eventSystem;
    private TMP_InputField _textBox;

    public delegate void OutputDisplay(string[] text, bool shouldClear);
    public OutputDisplay OutputLine;
    [SerializeField]
    private List<CommandContext> _commands = new List<CommandContext>();
    private CommandContext _commandOveride;
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
        _textBox.text = string.Empty;
        _textBox.ActivateInputField();
    }

    public void CommandEntered(string command)
    {
        if (command == string.Empty) return;

        Debug.Log("Command Entered: " + command);
        _textBox.text = string.Empty;
        bool foundCommand = false;

        if (_commandOveride != null)
        {
            string[] output = _commandOveride.CheckAndExecuteCommand(command, out bool clear);
            if (output != null)
            {
                OutputLine(output, clear);
                foundCommand = true;
               
            }
        }
        else
        for(int i = 0; i < _commands.Count; i++)
        {
            bool clear;
            string[] executeResult = _commands[i].CheckAndExecuteCommand(command, out clear);
            if (executeResult != null)
            {
                
                OutputLine(executeResult, clear);
                foundCommand = true;
                break;
            }
        }

        if (!foundCommand)
        {
            
            OutputLine(StringToArray("Command Not Found :("), false);
        }

    }

    public static string[] StringToArray(string str)
    {
        string[] array = new string[1];
        array[0] = str;
        return array;

    }

    public CommandContext CommandOveride
    {
        get { return _commandOveride; }
        set { _commandOveride = value; }
    }
}
