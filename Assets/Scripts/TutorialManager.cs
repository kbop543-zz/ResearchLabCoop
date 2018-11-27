using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    public GameObject athenaUI;
    public Text athenaDialogue;
    public bool dialogueTriggered = false;
    public GameObject hud;
    public Queue<string> sentences;

    private int timesPressed = 0;

    // Use this for initialization
    void Start () {
        Time.timeScale = 0;

        hud = gameObject.GetComponent<LevelManager>().hud;
        athenaUI = hud.transform.Find("Athena Canvas").gameObject;
        sentences = hud.transform.Find("Dialogue Manager").gameObject.GetComponent<DialogueManager>().sentences;
    }
	
	// Update is called once per frame
	void Update () {

        if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && !dialogueTriggered)
        {
            // We are in danger scientists! I am Athena, an AI security protocol that you created.
            // I've just detected an enemy intrusion and we must act quickly to defend the lab.
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            dialogueTriggered = true;
            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1))) // && timesPressed == 1)
        {
            // I have activated the interface that displays your vital statistics.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if (Input.GetKey("t"))
        {
            athenaUI.SetActive(false);
            Time.timeScale = 1;
        }

        //if (sentences.Count == 0 && (Input.GetKeyDown("y") || Input.GetKeyDown("t")))
        //{
        //    athenaUI.SetActive(false);
        //    Time.timeScale = 1;
        //}

    }
}
