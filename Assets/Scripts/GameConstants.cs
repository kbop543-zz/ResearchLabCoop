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
    public GameObject[] players;
    GameObject p1;
    GameObject p2;
    public GameObject gameUI;

    // For win condition, grab enemy death count and enemy spawn limit
    public int enemyKillCount = 0;
    public GameObject spawner, secondSpawner, thirdSpawner;

    // Use this for initialization
    void Start () {
        curLabHealth = maxLabHealth;
        gameOver = false;
        curOrbs = 0;
        completeLvl1 = false;
        completeLvl2 = false;
        completeLvl3 = false;
        // Grab each gameobject for win condition
        players = GameObject.FindGameObjectsWithTag("Player");
        p1 = GameObject.Find("P1(Clone)");
        p2 = GameObject.Find("P2(Clone)");
        spawner = GameObject.Find("Spawner(Clone)");
        secondSpawner = GameObject.Find("SecondSpawnManager(Clone)");
        thirdSpawner = GameObject.Find("ThirdSpawnManager(Clone)");
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
            gameUI.GetComponent<PlayerUI>().gameState.text = "GAME OVER";
            gameOver = true;
            Time.timeScale = 0;
        }
        if (gameOver) {
            Debug.Log("PRESS R TO RESTART WAVE");
            if (Input.GetKey("r")){

                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
                gameOver = false;
            }
           
        }
        // Win Condition 1): Kill same amount of enemy with enemy spawn limit
        if ((completeLvl1 == false) && (completeLvl2 == false) && (completeLvl3 == false))
        {
            if (enemyKillCount >= spawner.GetComponent<SpawnManager>().spawnLimit)
            {
                //spawner.GetComponent<SpawnManager>().end = true;
                secondSpawner.GetComponent<SpawnManager>().activated = true;
                Debug.Log("LEVEL 1 CLEARED");
                //.GetComponent<PlayerUI>().gameState.text = "LEVEL 1 CLEARED!";
                completeLvl1 = true;
                enemyKillCount = 0;
                //Time.timeScale = 0;
            }
        }
        if ((completeLvl1 == true) && (completeLvl2 == false) && (completeLvl3 == false))
        {
            if (enemyKillCount >= secondSpawner.GetComponent<SpawnManager>().spawnLimit)
            {
                //secondSpawner.GetComponent<SpawnManager>().end = true;
                thirdSpawner.GetComponent<SpawnManager>().activated = true;
                Debug.Log("LEVEL 2 CLEARED");
                //gameUI.GetComponent<PlayerUI>().gameState.text = "LEVEL 2 CLEARED!";
                completeLvl2 = true;
                enemyKillCount = 0;
                //Time.timeScale = 0;
            }
        }
        if ((completeLvl1 == true) && (completeLvl2 == true) && (completeLvl3 == false))
        {
            if (enemyKillCount >= thirdSpawner.GetComponent<SpawnManager>().spawnLimit)
            {
                //thirdSpawner.GetComponent<SpawnManager>().end = true;
                //secondSpawner.GetComponent<SpawnManager>().activated = true;
                Debug.Log("LEVEL 3 CLEARED/YOU WIN");
                //gameUI.GetComponent<PlayerUI>().gameState.text = "LEVEL 3 CLEARED! (YOU WIN)";
                completeLvl3 = true;
                enemyKillCount = 0;
                Time.timeScale = 0;
            }
        }
        if ((completeLvl1 == true) && (completeLvl2 == true) && (completeLvl3 == true)){
            //game over give option to restart
            gameOver = true;
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
}
