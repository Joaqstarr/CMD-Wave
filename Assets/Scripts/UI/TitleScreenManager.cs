using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public CanvasGroup _baseCanvas;
    // Start is called before the first frame update
    void Start()
    {

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
}
