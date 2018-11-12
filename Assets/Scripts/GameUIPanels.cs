using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIPanels : MonoBehaviour {

    [SerializeField] private GameObject waveCompletePanel;
    [SerializeField] private GameObject gameCompletePanel;
    [SerializeField] private GameObject gameOverPanel;
    public GameObject gm;
    public bool curWaveComplete;
    public bool finalWaveComplete;
    public bool playerLose;

    void Start()
    {
        waveCompletePanel.SetActive(false);
        gameCompletePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        gm = GameObject.FindWithTag("GameManager");
        curWaveComplete = gm.GetComponent<GameConstants>().curWaveComplete;
        finalWaveComplete = gm.GetComponent<GameConstants>().completeLvl3;
    }

    void Update()
    {
        curWaveComplete = gm.GetComponent<GameConstants>().curWaveComplete;
        finalWaveComplete = gm.GetComponent<GameConstants>().completeLvl3;

        if (curWaveComplete)
        {
            if (!waveCompletePanel.activeInHierarchy)
            {
                PauseGame();
            }
        }

        if (finalWaveComplete)
        {
            if (!gameCompletePanel.activeInHierarchy)
            {
                GameComplete();
            }
        }

        if (playerLose)
        {
            if (!gameOverPanel.activeInHierarchy)
            {
                GameOver();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        waveCompletePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }

    private void GameComplete()
    {
        Time.timeScale = 0;
        gameCompletePanel.SetActive(true);
    }

    public void NextWave()
    {
        gm.GetComponent<GameConstants>().curWaveComplete = false;
        Time.timeScale = 1;
        waveCompletePanel.SetActive(false);
        //enable the scripts again
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        gm.GetComponent<GameConstants>().gameOver = false;

        if (gameCompletePanel.activeInHierarchy)
        {
            gameCompletePanel.SetActive(false);
        }

        if (gameOverPanel.activeInHierarchy)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void MainMenu()
    {
        gm.GetComponent<GameConstants>().gameOver = false;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
