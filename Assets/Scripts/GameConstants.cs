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
    public bool completeLvl;
    public GameObject[] players;
    GameObject p1;
    GameObject p2;
    public GameObject gameUI;
    
    // For win condition, grab enemy death count and enemy spawn limit
    public int enemyKillCount = 0;
    public GameObject spawner;

    // Use this for initialization
    void Start () {
        curLabHealth = maxLabHealth;
        gameOver = false;
        curOrbs = 0;
        completeLvl = false;
        // Grab each gameobject for win condition
        players = GameObject.FindGameObjectsWithTag("Player");
        p1 = GameObject.Find("P1(Clone)");
        p2 = GameObject.Find("P2(Clone)");
        spawner = GameObject.Find("Spawner(Clone)");
        gameUI = GameObject.Find("HUDCanvas(Clone)");

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
        //if (gameOver) {
        //    Scene scene = SceneManager.GetActiveScene();
        //    SceneManager.LoadScene(scene.name);
        //    gameOver = false;
        //}
        // Win Condition 1): Kill same amount of enemy with enemy spawn limit
        if (enemyKillCount >= spawner.GetComponent<SpawnManager>().spawnLimit)
        {
            Debug.Log("YOU WIN");
            gameUI.GetComponent<PlayerUI>().gameState.text = "YOU WIN!";
            completeLvl = true;
            enemyKillCount = 0;
            Time.timeScale = 0;
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
