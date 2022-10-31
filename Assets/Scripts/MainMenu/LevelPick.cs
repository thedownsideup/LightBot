using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPick : MonoBehaviour
{
    public static LevelPick Instance;
    private int levelNumber;
    private const int GAME_SCENE_INDEX = 1;
    void Start()
    {
        if (Instance != null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void LoadScene(int chosenLevelNumber)
    {
        levelNumber = chosenLevelNumber;
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == GAME_SCENE_INDEX)
        {
            LoadLevel();
        }
    }

    private void LoadLevel()
    {
        Level_Manager levelManager = GameObject.Find("Level_Manager").GetComponent<Level_Manager>();
        levelManager.levelNumber = levelNumber;
        levelManager.LoadNextLevel();
    }
}
