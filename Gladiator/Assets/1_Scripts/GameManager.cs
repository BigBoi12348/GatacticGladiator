using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Scene IDs")]
    const int MAINMENUSCENE = 0;
    const int GAMESCENE = 1;


    [Header("Scene Control Variables")]
    [SerializeField] private int _lastSceneID;
    [SerializeField] private int _currentSceneID;


    [Header("Saving Data Variables")]
    const string SAVEDATAPATH = "gameData";


    public int CurrentSceneID{get{return _currentSceneID;} private set{_currentSceneID = value;}}
    public int LastSceneID{get{return _lastSceneID;} private set{_lastSceneID = value;}}

    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _currentSceneID = SceneManager.GetActiveScene().buildIndex;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable() 
    {
        GameEvents.playerStartGame += UnFreezeGame;
        GameEvents.playerFinsihedGame += FreezeGame;
    }

    private void OnDisable() 
    {
        GameEvents.playerStartGame -= UnFreezeGame;
        GameEvents.playerFinsihedGame -= FreezeGame;
    }

    public void LoadThisScene(int levelID)
    {
        Debug.Log("Load Scene");
        StartCoroutine(LoadSceneAsync(levelID));
    }

    IEnumerator LoadSceneAsync(int levelID)
    {
        _lastSceneID = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelID);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            yield return null;
        }
        
        _currentSceneID = SceneManager.GetActiveScene().buildIndex;
    }

    public IEnumerator StartGame()
    {
        //Makes sure that the Scene is the GAME Scene so that the events that are called are actually received
        while (_currentSceneID != GAMESCENE)
        {
            yield return new WaitForSecondsRealtime(0);
        }
        GameEvents.gameStartSetUp?.Invoke();;
    }

    public void UnFreezeGame()
    {
        Debug.Log("Unfreeze");
        Time.timeScale = 1;
    }

    public void FreezeGame(bool x)
    {
        Debug.Log("freeze");
        Time.timeScale = 0;
    }
}
