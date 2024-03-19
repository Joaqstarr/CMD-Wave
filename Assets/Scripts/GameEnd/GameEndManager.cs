using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndManager : MonoBehaviour
{
    public static GameEndManager Instance;

    private CanvasGroup _endScreen;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;

       _endScreen = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndScreen()
    {
        StartCoroutine(ShowEndScreen());
    }


    private IEnumerator ShowEndScreen()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
        _endScreen.alpha = 1f;
        _endScreen.interactable = true;
        _endScreen.blocksRaycasts = true;
    }
}
