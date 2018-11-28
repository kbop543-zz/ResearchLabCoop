using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    public AudioSource clickSound;
    public GameObject station;
    void Start()
    {
        station = GameObject.Find("HoleStation(Clone)");
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
        station.GetComponent<StationStatus>().stopsucksound();
        pausePanel.SetActive(true);
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(pausePanel.transform.Find("buttons/Resume Button").gameObject);
        //Disable scripts that still work while timescale is set to 0
    }

    public void ContinueGame()
    {
        station.GetComponent<StationStatus>().playucksound();
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts again
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
