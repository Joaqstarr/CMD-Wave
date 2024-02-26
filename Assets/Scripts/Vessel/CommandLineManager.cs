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

    public delegate void OutputDisplay(string text, bool shouldClear);
    public OutputDisplay OutputLine;

    List<CommandBase> _commands = new List<CommandBase>();
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
        for(int i = 0; i < _commands.Count; i++)
        {
            if (_commands[i].CheckCommand(command))
            {
                {
                    OutputLine(_commands[i].Execute(), _commands[i].ShouldClear);
                    foundCommand = true;
                }

                break;
            }
        }
        if (!foundCommand)
        {
            OutputLine("Command Not Found :(", false);
        }

    }
}
