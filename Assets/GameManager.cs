using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public bool isGameActive = false;

    public bool isControlsActive = false;
    private UIManager uiManager;
    
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        isGameActive = true;
        isControlsActive = true;
    }

    public void Fail()
    {
        isControlsActive = false;
        uiManager.showRestartButton();
    }

    public void Win()
    {
        isControlsActive = false;
        uiManager.showWinButton(); 
    }
}
