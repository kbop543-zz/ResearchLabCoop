using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    public AudioSource clickSound;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Joystick1Button9) || Input.GetKeyDown(KeyCode.Joystick2Button9))
        {
            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
                if (Input.GetKey(KeyCode.JoystickButton0))
                {
                    clickSound.Play();
                }
            }
            else if (pausePanel.activeInHierarchy)
            {
                ContinueGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(pausePanel.transform.GetChild(2).GetChild(0).gameObject);
        //Disable scripts that still work while timescale is set to 0
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts again
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
