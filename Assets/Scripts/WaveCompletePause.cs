using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCompletePause : MonoBehaviour {

    [SerializeField] private GameObject waveCompletePanel;
    public GameObject gm;
    public bool curWaveComplete;

    void Start()
    {
        waveCompletePanel.SetActive(false);

        gm = GameObject.Find("GameManagerTest");
        curWaveComplete = gm.GetComponent<GameConstants>().curWaveComplete;
    }

    void Update()
    {
        curWaveComplete = gm.GetComponent<GameConstants>().curWaveComplete;

        if (curWaveComplete)
        {
            if (!waveCompletePanel.activeInHierarchy)
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        waveCompletePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }

    public void NextWave()
    {
        gm.GetComponent<GameConstants>().curWaveComplete = false;
        Time.timeScale = 1;
        waveCompletePanel.SetActive(false);
        //enable the scripts again
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
