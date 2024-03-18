using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class TitleScreenManager : MonoBehaviour
{
    public CanvasGroup _baseCanvas;
    public CanvasGroup _newGameText;
    public CanvasGroup _textContinue;

    public TMP_Text _textComponent;
    private string _textToType;

    private int startingLength = 0;
    [SerializeField]
    private float _timePerChar = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        /*startingLength = _textComponent.text.Length;
        _textToType = _textComponent.text;
        _textComponent.text = "";
        Debug.Log(_textToType);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableBase()
    {
        _baseCanvas.alpha = 1.0f;
        _baseCanvas.interactable = true;
        _baseCanvas.blocksRaycasts = true;
    }

    public void DisableBase()
    {
        _baseCanvas.alpha = 0f;
        _baseCanvas.interactable = false;
        _baseCanvas.blocksRaycasts = false;
    }

    public void NewGameText()
    {
        _newGameText.alpha = 1.0f;
        _newGameText.interactable = true;
        _newGameText.blocksRaycasts = true;

        PrintToLog(_textComponent.text, true);
    }

    public void EnableTextContinue()
    {
        _textContinue.alpha = 1.0f;
        _textContinue.interactable = true;
        _textContinue.blocksRaycasts = true;
    }

    public void PrintToLog(string msg, bool shouldClear)
    {
        if (shouldClear)
        {
            //_textComponent.text = "";
        }

        _textComponent.maxVisibleCharacters = startingLength;
        StartCoroutine(DisplayText(_timePerChar));
    }

    private IEnumerator DisplayText(float time)
    {
        yield return new WaitForSeconds(0.6f);

        while (_textComponent.maxVisibleCharacters != _textComponent.text.Length)
        {
            yield return new WaitForSeconds(time);
            _textComponent.maxVisibleCharacters += 1;
        }

        yield return new WaitForSeconds(1.2f);
        EnableTextContinue();
    }

}
