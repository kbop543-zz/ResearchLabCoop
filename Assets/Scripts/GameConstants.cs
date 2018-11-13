using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameConstants : MonoBehaviour {

    public int maxLabHealth = 100;
    public int maxOrbs = 5;
    public int curLabHealth;
    public int curOrbs;
    public bool gameOver;
    public bool completeLvl1, completeLvl2, completeLvl3;
    public bool curWaveComplete;
    public GameObject[] players;
    GameObject p1;
    GameObject p2;
    public AudioSource beesintro;
    public GameObject gameUI;
    public Text test;

    // For win condition, grab enemy death count and enemy spawn limit
    public int enemyKillCount = 0;
    public int comboFalling = 0;
    public int comboShatter = 0;
    public int comboBoom = 0;

    public GameObject spawner, secondSpawner, thirdSpawner;
    public int curWaveSpawnLimit;

    // Use this for initialization
    void Start () {
        curLabHealth = maxLabHealth;
        gameOver = false;
        curOrbs = 0;
        completeLvl1 = false;
        completeLvl2 = false;
        completeLvl3 = false;
        curWaveComplete = false;
        // Grab each gameobject for win condition
        players = GameObject.FindGameObjectsWithTag("Player");
        p1 = GameObject.Find("P1(Clone)");
        p2 = GameObject.Find("P2(Clone)");

        // Get enemy spawner gameobjects
        spawner = GameObject.Find("Spawner(Clone)");
        secondSpawner = GameObject.Find("SecondSpawnManager(Clone)");
        thirdSpawner = GameObject.Find("ThirdSpawnManager(Clone)");
        // assign the enemy spawn limit of first enemy spawner to a variable that will update after each wave
        curWaveSpawnLimit = spawner.GetComponent<SpawnManager>().spawnLimit;

        gameUI = GameObject.Find("HUDCanvas(Clone)");
        Time.timeScale = 1.0f;
        if (spawner.GetComponent<SpawnManager>().prevSpawner == null){
            spawner.GetComponent<SpawnManager>().activated = true;
        }

    }
    	
	// Update is called once per frame
	void Update () {
        // Lose Condition 1): lab health drops to 0
        if (!gameOver && p1.GetComponent<PlayerHealth>().playerIsDead && p2.GetComponent<PlayerHealth>().playerIsDead) {
            // Enter GAMEOVER scene
            Debug.Log("GAME OVER");
            //gameUI.GetComponent<PlayerUI>().gameState.text = "GAME OVER";
            gameOver = true;
            gameUI.GetComponent<GameUIPanels>().playerLose = true;
            //Time.timeScale = 0;
        }
        if (gameOver) {
            Debug.Log("PRESS R TO RESTART GAME");
            if ((Input.GetKey("r"))|| (Input.GetKey(KeyCode.JoystickButton9))){

                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
                gameOver = false;
            }
            if ((Input.GetKey("e")) || (Input.GetKey(KeyCode.JoystickButton8))){
                gameOver = false;
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(0);


            }
           
        }
        // Win Condition 1): Kill same amount of enemy with enemy spawn limit
        if ((completeLvl1 == false) && (completeLvl2 == false) && (completeLvl3 == false))
        {
            if (enemyKillCount >= curWaveSpawnLimit)
            {
                //spawner.GetComponent<SpawnManager>().end = true;
                secondSpawner.GetComponent<SpawnManager>().activated = true;
                Debug.Log("LEVEL 1 CLEARED");
                //test.text = "LEVEL 1 CLEARED";
                //gameUI.GetComponent<PlayerUI>().gameState.text = "LEVEL 1 CLEARED!";
                completeLvl1 = true;
                curWaveComplete = true;
                enemyKillCount = 0;
                comboFalling = 0;
                comboShatter = 0;
                comboBoom = 0;
                curWaveSpawnLimit = secondSpawner.GetComponent<SpawnManager>().spawnLimit;
                //gameUI.GetComponent<PlayerUI>().waveNum.text = "Wave 2 of 3";
                StartCoroutine(ChangeWaveNumText("Wave 2 of 3", 3.1f));
                //Time.timeScale = 0;

                //Enable FreezeStation
                GameObject[] stations = GameObject.FindGameObjectsWithTag("Station");
                for (int i = 0; i < stations.Length; i++)
                {
                    if (stations[i].name.Contains("FreezeStation")) {
                        stations[i].GetComponent<StationStatus>().prepared = true;
                    }
                }
            }
        }
        if ((completeLvl1 == true) && (completeLvl2 == false) && (completeLvl3 == false))
        {
            if (enemyKillCount >= curWaveSpawnLimit)
            {
                //beesintro.Play();
                //secondSpawner.GetComponent<SpawnManager>().end = true;
                thirdSpawner.GetComponent<SpawnManager>().activated = true;
                Debug.Log("LEVEL 2 CLEARED");
                //gameUI.GetComponent<PlayerUI>().gameState.text = "LEVEL 2 CLEARED!";
                completeLvl2 = true;
                curWaveComplete = true;
                enemyKillCount = 0;
                comboFalling = 0;
                comboShatter = 0;
                comboBoom = 0;
                curWaveSpawnLimit = thirdSpawner.GetComponent<SpawnManager>().spawnLimit;
                //gameUI.GetComponent<PlayerUI>().waveNum.text = "Wave 3 of 3";
                StartCoroutine(ChangeWaveNumText("Wave 3 of 3", 3.1f));
                //Time.timeScale = 0;

                //Enable ElectricityStation
                GameObject[] stations = GameObject.FindGameObjectsWithTag("Station");
                for (int i = 0; i < stations.Length; i++)
                {
                    if (stations[i].name.Contains("ElectricityStation"))
                    {
                        stations[i].GetComponent<StationStatus>().prepared = true;
                    }
                }
            }
        }
        if ((completeLvl1 == true) && (completeLvl2 == true) && (completeLvl3 == false))
        {
            if (enemyKillCount >= curWaveSpawnLimit)
            {

                //thirdSpawner.GetComponent<SpawnManager>().end = true;
                //secondSpawner.GetComponent<SpawnManager>().activated = true;
                Debug.Log("LEVEL 3 CLEARED/YOU WIN");
                //gameUI.GetComponent<PlayerUI>().gameState.text = "LEVEL 3 CLEARED! (YOU WIN)";
                completeLvl3 = true;
                enemyKillCount = 0;
                comboFalling = 0;
                comboShatter = 0;
                comboBoom = 0;
                //Time.timeScale = 0;
            }
        }
        if ((completeLvl1 == true) && (completeLvl2 == true) && (completeLvl3 == true))
        {
            //    //game over give option to restart
            //    //gameOver = true;

            //    completeLvl1 = false;
            //    completeLvl2 = false;
            //    completeLvl3 = false;
            if (Input.GetKey(KeyCode.JoystickButton9))
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
                gameOver = false;
            }
            if ((Input.GetKey("e"))|| (Input.GetKey(KeyCode.JoystickButton8)))
            {
                gameOver = false;
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(0);


            }
        }


        // Win Condition 1): collect enough orbs and press button
        //if (!completeLvl && !gameOver && curOrbs >= maxOrbs &&
        //    ((GameObject.Find("P2(Clone)").transform.position.y > 700 && GameObject.Find("P2(Clone)").transform.position.z < -40 &&
        //      GameObject.Find("P2(Clone)").GetComponent<PickOrDrop>().hasCollector) ||
        //     (GameObject.Find("P1(Clone)").transform.position.y > 700 && GameObject.Find("P1(Clone)").transform.position.z < -40 &&
        //      GameObject.Find("P1(Clone)").GetComponent<PickOrDrop>().hasCollector)))
        //{
        //    // Enter WIN scene
        //    Debug.Log("YOU WIN");
        //    completeLvl = true;
        //}

    }

    public IEnumerator ChangeWaveNumText (string txt, float delay) {
        float i = 0f;
        while (i < delay) {
            i += Time.deltaTime;
            yield return null;
        }

        gameUI.GetComponent<PlayerUI>().waveNum.text = txt;
    }
}
