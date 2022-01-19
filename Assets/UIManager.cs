using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    // Start is called before the first frame update
    private GameManager gameManager;
    private bool gameStarted = false;
    [SerializeField] private GameObject UI;
    private Transform _canvas;
    private Transform startButton;
    private Transform restartButton;
    private Transform winButton;
    void Start()
    {
        _canvas = UI.transform.GetChild(0);
        startButton = _canvas.transform.GetChild(0);
        restartButton = _canvas.transform.GetChild(1);
        winButton = _canvas.transform.GetChild(2);
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive && !gameStarted)
        {
            startButton.gameObject.SetActive(false);
            gameStarted = true;
        }
    }

    public void showRestartButton()
    {
        restartButton.gameObject.SetActive(true);
    }
    
    public void showWinButton()
    {
        winButton.gameObject.SetActive(true);
    }

    public void writeLevelNo()
    {
        _canvas.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Level " + (PlayerPrefs.GetInt("_level", 0)+1);
    }
}
