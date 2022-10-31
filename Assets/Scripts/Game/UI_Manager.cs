using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Text levelOverText;
    [SerializeField] private Button nextLevelButton;
    public void LevelOver()
    {
        levelOverText.gameObject.SetActive(true);
        nextLevelButton.gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        levelOverText.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
    }

    public void OnBackClicked()
    {
        SceneManager.LoadScene(0);
    }
}
