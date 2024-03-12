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
    private List<CommandBase> _commands = new List<CommandBase>();
    public string[] CheckAndExecuteCommand(string command, out bool shouldClear, out CommandContext overrideContext, string args = null)
    {
        for (int i = 0; i < _commands.Count; i++)
        { 
            if (_commands[i].CheckCommand(command))
            {

                shouldClear = _commands[i].ShouldClear;
                string[] result = _commands[i].Execute(out overrideContext, args);
                return result;

            }
        }
        shouldClear = false;
        overrideContext = null;
        return null;
    }

    public void AddCommand(CommandBase command)
    {
        _commands.Add(command);
    }

    public void RemoveCommand(CommandBase command)
    {
        _commands.Remove(command);
    }
    public int Count
    {
        get { return _commands.Count; }
    }
    public CommandBase GetCommandAt(int index)
    {
        return _commands[index];
    }

    public void Clear()
    {
        _commands.Clear();
    }
}
public class CommandLineManager : MonoBehaviour
{

    private TMP_InputField _textBox;

    public delegate void OutputDisplay(string[] text, bool shouldClear);
    public OutputDisplay OutputLine;
    [SerializeField]
    private List<CommandContext> _commands = new List<CommandContext>();
    private CommandContext _commandOveride;
    public static CommandLineManager Instance;
    private bool _enteringCommand = false;

    [SerializeField]
    private CommandBase _helpCommand;
    private void Awake()
    {

        if (Instance == null)
            Instance = this;
    }
    void Start()
    {


        _textBox = GetComponentInChildren<TMP_InputField>();

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
        if (_enteringCommand) return;
        _enteringCommand = true;
        _textBox.text = string.Empty;
        _textBox.ActivateInputField();
    }

    public void CommandEntered(string command)
    {
        _enteringCommand = false;
        if (command == string.Empty) return;

        _textBox.text = string.Empty;
        bool foundCommand = false;

        string argument = string.Empty;
        if(command.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length > 1)
        {
            argument = command.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1];

        }
        int count = command.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;
        command = command.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0];
        Debug.Log("Command Entered: " + command + ", argument: " + argument + ", count: "+ count);

        if(_helpCommand.CheckCommand(command))
        {
            OutputLine(_helpCommand.Execute(out _commandOveride), _helpCommand.ShouldClear);
            return;
        }


        if (_commandOveride != null && _commandOveride.Count > 0)
        {
            string[] output = _commandOveride.CheckAndExecuteCommand(command, out bool clear, out CommandContext overrideContext, argument);
            _commandOveride = overrideContext;
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
                
            string[] executeResult = _commands[i].CheckAndExecuteCommand(command, out clear, out CommandContext overrideContext, argument);
            _commandOveride = overrideContext;

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

    public void AddCommand(CommandBase command)
    {
        _commands[0].AddCommand(command);
    }

    public CommandBase[] GetPossibleCommands()
    {
        List<CommandBase> possibleCommands = new List<CommandBase>();

        if(_commandOveride != null)
        {
            for(int i = 0; i < _commandOveride.Count; i++)
            {
                possibleCommands.Add(_commandOveride.GetCommandAt(i));
            }
        }
        else
        {
            for(int i = 0; i< _commands.Count; i++)
            {
                for(int j = 0; j < _commands[i].Count; j++)
                {
                    possibleCommands.Add(_commands[i].GetCommandAt(j));
                }
            }
        }


        return possibleCommands.ToArray();
    }

    public void AddContext(CommandContext context)
    {
        _commands.Add(context);
    }

    public bool IsTyping
    {
        get { return _enteringCommand; }
    }
}
