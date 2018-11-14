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
    public GameObject blackCanvas3;
    public GameObject blackCanvas4;
    public GameObject blackCanvas5;
    public GameObject blackCanvas6;
    public GameObject waveProg1;

    public GameObject shrinkBattery;
    public GameObject holeBattery;

    private int timesPressed = 0;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;

        hud = gameObject.GetComponent<LevelManager>().hud;
        athenaUI = hud.transform.Find("Athena Canvas").gameObject;
        blackCanvas1 = hud.transform.Find("Black Canvas (1)").gameObject;
        blackCanvas2 = hud.transform.Find("Black Canvas (2)").gameObject;
        blackCanvas3 = hud.transform.Find("Black Canvas (3)").gameObject;
        blackCanvas4 = hud.transform.Find("Black Canvas (4)").gameObject;
        blackCanvas5 = hud.transform.Find("Black Canvas (5)").gameObject;
        blackCanvas6 = hud.transform.Find("Black Canvas (6)").gameObject;
        waveProg1 = hud.transform.Find("Wave Progression (1)").gameObject;
        //athenaUI.GetComponent<DialogueTrigger>().TriggerDialogue();
        blackCanvas1.SetActive(true);

        shrinkBattery = gameObject.GetComponent<LevelManager>().shrink.transform.Find("BatteryCanvas").gameObject;
        holeBattery = gameObject.GetComponent<LevelManager>().hole.transform.Find("BatteryCanvas").gameObject;

        // Set both the battery for the shrink and hole station as inactive
        shrinkBattery.SetActive(false);
        holeBattery.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && !dialogueTriggered)
        {
            // We are in danger scientists! I am Athena, an AI security protocol that you created.
            // I've just detected an enemy intrusion and we must act quickly to defend the lab.
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            dialogueTriggered = true;
            timesPressed++;
        } 
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 1)
        {
            // I have activated the interface that displays your vital statistics.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            blackCanvas1.SetActive(true);

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 2)
        {
            // The two bars on the bottom displays the amount of health each of you have remaining.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            blackCanvas1.SetActive(false);
            blackCanvas2.SetActive(true);

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 3)
        {
            // The circle icons right above your health bar will become active if you are affected by any statuses.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 4)
        {
            // These statuses lower your performance or weaken you, and the icons will become inactive when the statuses go away.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 5)
        {
            // This bar on top is the progress bar, which fills up as you defeat enemies in the current wave.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            blackCanvas1.SetActive(true);
            blackCanvas2.SetActive(false);
            waveProg1.SetActive(true);

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 6)
        {
            // There aren’t not many things around the lab you can use to fight the enemies,
            // but I’ve switched on the science stations you two have been researching.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 7)
        {
            // This is the shrink ray.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            blackCanvas1.SetActive(false);
            blackCanvas3.SetActive(true);

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 8)
        {
            // These here are the black holes.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            blackCanvas3.SetActive(false);
            blackCanvas4.SetActive(true);

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 9)
        {
            // This is the freeze station.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            blackCanvas4.SetActive(false);
            blackCanvas5.SetActive(true);

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 10)
        {
            // And here we have the electric coil.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            blackCanvas5.SetActive(false);
            blackCanvas6.SetActive(true);

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 11)
        {
            // It appears that the intruders might have caused a power outage and not all the stations are powered.
            // We will need to wait for the backup power supply.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            blackCanvas6.SetActive(false);
            blackCanvas1.SetActive(true);

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 12)
        {
            // You can activate the stations by pressing X when you are close enough to the stations.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 13)
        {
            // Once the stations are switched on, their effects will take place in the 
            // striped areas and it will need to recharge before you can use it again.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 14)
        {
            // Be careful not to walk into the striped areas while the stations are activated! 
            // Or else, you might get blasted too!
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 15)
        {
            // I have also equipped both of you with a multifunctional gun.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 16)
        {
            // There are two gun modes. The shockwave mode and oil mode.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 17)
        {
            // You can switch between the two gun modes by pressing (triangle) and you can shoot by pressing RT.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 18)
        {
            // The science stations and gun modes by themselves aren’t harmful, 
            // but if you were to combine the effects together they could deadly.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 19)
        {
            // You can shrink your enemies and then drop them into the black holes when they are small enough!
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 20)
        {
            // You can also freeze your enemies and then shatter them with the impact of your shockwave gun.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 21)
        {
            // Or you could drench your enemies in oil and ignite them into flames with the electric coil.
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 22)
        {
            // This is all I have time for! Good luck scientists!
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 23)
        {
            // Here comes your first enemy!
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            timesPressed++;
        }
        else if ((Input.GetKeyDown("y") || Input.GetKeyDown(KeyCode.JoystickButton1)) && timesPressed == 24)
        {
            // Tutorial enemies incoming!!
            blackCanvas1.SetActive(false);
            waveProg1.SetActive(false);
            athenaUI.SetActive(false);
            shrinkBattery.SetActive(true);
            holeBattery.SetActive(true);
            Time.timeScale = 1;

            timesPressed++;
        }
    }

}
