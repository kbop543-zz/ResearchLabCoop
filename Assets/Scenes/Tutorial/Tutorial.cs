using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public GameObject athenaUI;
    public Text athenaDialogue;

    private int timesPressed = 0;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;

        athenaDialogue.text = "Greetings scientists! I am Athena, an AI security protocol that you created. " +
            "I've just detected an enemy intrusion and we must act quickly to defend the lab.";
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKeyDown("y") || Input.GetKey(KeyCode.JoystickButton1)) && timesPressed == 0)
        {
            athenaDialogue.text = "I have activated the interface that displays your vital statistics.";
            timesPressed++;
            Debug.Log(timesPressed);
        }

        if ((Input.GetKeyDown("y") || Input.GetKey(KeyCode.JoystickButton1)) && timesPressed == 1)
        {
            athenaDialogue.text = "The two bars on the bottom displays the amount of health each of you have remaining.";
            timesPressed++;
            Debug.Log(timesPressed);
        }

        if ((Input.GetKeyDown("y") || Input.GetKey(KeyCode.JoystickButton1)) && timesPressed == 2)
        {
            athenaDialogue.text = "If any of these icons here are active, you are affliected with a status. " +
                "These statuses lower your performance or weaken you, and the icons show you when the status will go away.";
            timesPressed++;
            Debug.Log(timesPressed);
        }

        if ((Input.GetKeyDown("y") || Input.GetKey(KeyCode.JoystickButton1)) && timesPressed == 3)
        {
            athenaDialogue.text = "This bar on top is the progress bar, which shows you how far you have progressed through the current wave.";
            timesPressed++;
            Debug.Log(timesPressed);
        }
    }
}
