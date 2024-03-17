using DG.Tweening;
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
    public string[] CheckAndExecuteCommand(string command, out bool shouldClear, out CommandContext overrideContext, out AudioClip sfx, string args = null)
    {
        for (int i = 0; i < _commands.Count; i++)
        { 
            if (_commands[i].CheckCommand(command))
            {

                shouldClear = _commands[i].ShouldClear;
                string[] result = _commands[i].Execute(out overrideContext,out sfx, args);
                return result;

            }
        }
        sfx = null;
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
    [Header("Sounds")]
    [SerializeField]
    private AudioClip _beginCommandLine;
    [SerializeField]
    private AudioClip _defaultCommandEnteredSound;
    private AudioSource _audioSource;

    private RectTransform _rectTransform;
    [SerializeField]
    private RectSettings _startingRect;
    [SerializeField]
    private RectSettings _inGameRect;
    [SerializeField]
    private RectSettings _typingRect;
    private bool _lockSize = false;

    [Serializable]
    public struct RectSettings
    {
        public float XPosition;
        public float Width;
        public TweenData tweenData;
    }


    [Header("New Game Settings")]
    [SerializeField]
    private CommandContext _newGameContext;

    private void Awake()
    {

        if (Instance == null)
            Instance = this;
        _rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {

        _audioSource = GetComponent<AudioSource>();
        _textBox = GetComponentInChildren<TMP_InputField>();
        
    }

    private void OnEnable()
    {
        PlayerSubControls.openCommandLine += StartCommandLine;
        GameStartManager.GameStarted += NewGame;
    }
    private void OnDisable()
    {
        PlayerSubControls.openCommandLine -= StartCommandLine;
        GameStartManager.GameStarted -= NewGame;


    }
    public void StartCommandLine()
    {
        if (_enteringCommand) return;
        _enteringCommand = true;
        _textBox.text = string.Empty;
        _textBox.ActivateInputField();
        PlaySound(_beginCommandLine);
        SwitchRectSettings(_typingRect);
    }

    public void CommandEntered(string command)
    {
        SwitchRectSettings(_inGameRect);
        _enteringCommand = false;
        if (command == string.Empty) return;

        _textBox.text = string.Empty;
        bool foundCommand = false;
        AudioClip sfx = null;

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
            OutputLine(_helpCommand.Execute(out _commandOveride, out sfx),_helpCommand.ShouldClear);
            PlaySound(sfx);

            return;
        }


        if (_commandOveride != null && _commandOveride.Count > 0)
        {
            string[] output = _commandOveride.CheckAndExecuteCommand(command, out bool clear, out CommandContext overrideContext, out sfx, argument);
            if(GameStartManager.Instance.LockSubPosition && overrideContext == null)
            {

            }
                else
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
                
            string[] executeResult = _commands[i].CheckAndExecuteCommand(command, out clear, out CommandContext overrideContext, out sfx, argument);

            _commandOveride = overrideContext;

            if (executeResult != null)
            {
                OutputLine(executeResult, clear);
                foundCommand = true;
                break;
            }
        }


        if (sfx == null)
            sfx = _defaultCommandEnteredSound;

        PlaySound(sfx);
        

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

    private void PlaySound(AudioClip clip)
    {
        if(clip != null)
        if (_audioSource != null)
            _audioSource.PlayOneShot(clip);
    }

    public void SwitchRectSettings(RectSettings newRect, float timeOverride = -1)
    {
        if (_lockSize) return;

        float time = newRect.tweenData.Duration;
        if (timeOverride >= 0)
            time = timeOverride;
        _rectTransform.DOAnchorPosX(newRect.XPosition, time).SetEase(newRect.tweenData.Ease);
        _rectTransform.DOSizeDelta(new Vector2(newRect.Width, _rectTransform.rect.height), time).SetEase(newRect.tweenData.Ease);
    }

    private void NewGame()
    {
        _commandOveride = _newGameContext;
        SwitchRectSettings(_startingRect);
        _lockSize = true;

    }

    public void PowerOn()
    {
        _lockSize = false;
        SwitchRectSettings(_inGameRect, 1f);

    }
}
