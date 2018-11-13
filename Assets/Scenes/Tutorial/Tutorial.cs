using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public GameObject athenaUI;
    public Text athenaDialogue;
    public bool dialogueTriggered = false;
    public GameObject hud;

    public GameObject blackCanvas1;
    public GameObject blackCanvas2;
    public GameObject waveProg1;

    private int timesPressed = 0;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;

        hud = gameObject.GetComponent<LevelManager>().hud;
        blackCanvas1 = hud.transform.Find("Black Canvas (1)").gameObject;
        blackCanvas2= hud.transform.Find("Black Canvas (2)").gameObject;
        waveProg1 = hud.transform.Find("Wave Progression (1)").gameObject;
        //athenaUI.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown("y") || Input.GetKey(KeyCode.JoystickButton1)) && !dialogueTriggered)
        {
            // We are in danger scientists! I am Athena, an AI security protocol that you created.
            // I've just detected an enemy intrusion and we must act quickly to defend the lab.
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            dialogueTriggered = true;
            timesPressed++;
        } 
        else if ((Input.GetKeyDown("y") || Input.GetKey(KeyCode.JoystickButton1)) && timesPressed == 1)
        {
            // I have activated the interface that displays your vital statistics.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            blackCanvas1.SetActive(true);

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKey(KeyCode.JoystickButton1)) && timesPressed == 2)
        {
            // The two bars on the bottom displays the amount of health each of you have remaining.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            blackCanvas1.SetActive(false);
            blackCanvas2.SetActive(true);

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKey(KeyCode.JoystickButton1)) && timesPressed == 3)
        {
            // If any of these icons here are active, you are afflicted with a status.
            // These statuses lower your performance or weaken you, and the icons show you when the status will go away.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKey(KeyCode.JoystickButton1)) && timesPressed == 4)
        {
            //This bar on top is the progress bar, which shows you how far you have progressed through the current wave.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            blackCanvas1.SetActive(true);
            blackCanvas2.SetActive(false);
            waveProg1.SetActive(true);

            timesPressed++;
        }

    }

}
