using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class OutputLog : MonoBehaviour
{
    [SerializeField]
    private CommandLineManager _commandManager;

    private TMP_Text _textComponent;
    private void Start()
    {
        _textComponent = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _commandManager.OutputLine += PrintToLog;
    }
    private void OnDisable()
    {
        _commandManager.OutputLine -= PrintToLog;

    }

    public void PrintToLog(string msg, bool shouldClear)
    {
        if (shouldClear)
        {
            _textComponent.text = "";
        }

        msg += "\n";

        _textComponent.text = _textComponent.text + msg;


    }

    public void PrintToLog(string[] msgs, bool shouldClear)
    {
        if (shouldClear)
        {
            _textComponent.text = "";
        }
        foreach (string msg in msgs)
        {
            PrintToLog(msg, false);
        }
    }
}
