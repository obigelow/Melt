using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;

    public GameObject melt;
    bool gameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GameOver()
    {
        gameOver = true;

        gameOverPanel.SetActive(true);

    }

    public void Play()
    {
        SceneManager.LoadScene("AutoGeneration");
    }

    public void DialogueScene()
    {
        SceneManager.LoadScene("AcceptQuest");
    }

    public void Restart()
    {
        SceneManager.LoadScene("AutoGeneration");
    }
}
