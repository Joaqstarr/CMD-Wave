using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenManager : MonoBehaviour
{
    public static DeathScreenManager Instance;

    public bool IsDead = false;
    private CanvasGroup _canvasGroup;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        _canvasGroup = GetComponent<CanvasGroup>();
    }



    public void KillScreen()
    {
        IsDead = true;

        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void Revive()
    {
        IsDead = false;

        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}
