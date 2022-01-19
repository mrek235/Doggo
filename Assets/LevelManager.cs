using System;
using System.Collections;
using System.Collections.Generic;
//using ElephantSDK;

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoSingleton<LevelManager>
{
    public int currentLevel;

    [SerializeField] private GameObject[] levelPrefabs = default;
    
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("_level", 0);
        YsoCorp.GameUtils.YCManager.instance.OnGameStarted(currentLevel);
        LoadCurrentLevel();
    }

    private void OnApplicationQuit()
    {
        //PlayerPrefs.SetInt("_level", currentLevel);
    }

    public void LoadCurrentLevel()
    {
        Instantiate(levelPrefabs[currentLevel % levelPrefabs.Length], null);
        
        // Elephant level started
        //Elephant.LevelStarted(currentLevel);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, currentLevel.ToString());
    }

    public void LoadNextLevel()
    {
        PlayerPrefs.SetInt("_level", currentLevel + 1);
        YsoCorp.GameUtils.YCManager.instance.OnGameFinished(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReloadLevel()
    {
        YsoCorp.GameUtils.YCManager.instance.OnGameFinished(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}