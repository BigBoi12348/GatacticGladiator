using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Scene IDs")]
    public const int MAINMENUSCENE = 1;
    public const int GAMESCENE = 2;
    public const int UPGRADESCENE = 3;
    public const int TUTSCENE = 4;
    public const int CREDITS = 5;


    [Header("Scene Control Variables")]
    [SerializeField] private int _lastSceneID;
    [SerializeField] private int _currentSceneID;

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

    private void Start() 
    {
        Debug.Log("Gamemanger start");
        //RoundData.Wave = 1;
        //This is cause there is no menu yet
        //GameEvents.gameStartSetUp?.Invoke();
    }
    
    private void OnEnable() 
    {
        //GameEvents.playerStartGame += UnFreezeGame;
        //GameEvents.playerFinsihedGame += FreezeGame;
    }

    private void OnDisable() 
    {
        //GameEvents.playerStartGame -= UnFreezeGame;
        //GameEvents.playerFinsihedGame -= FreezeGame;
    }

    public void LoadThisScene(int levelID)
    {
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
        if(_currentSceneID == MAINMENUSCENE)
        {
            UnFreezeGame();
        }
    }

    public void UnFreezeGame()
    {
        Time.timeScale = 1;
    }

    public void FreezeGame()
    {
        Time.timeScale = 0;
    }
}
