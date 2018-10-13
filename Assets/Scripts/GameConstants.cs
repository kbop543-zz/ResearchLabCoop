using UnityEngine;

public class GameConstants : MonoBehaviour {

    public int maxLabHealth = 100;
    public int maxOrbs = 5;
    public int curLabHealth;
    public int curOrbs;
    public bool gameOver;
    public bool completeLvl;
    public GameObject[] players;

    // Use this for initialization
    void Start () {
        curLabHealth = maxLabHealth;
        gameOver = false;
        curOrbs = 0;
        completeLvl = false;
        players = GameObject.FindGameObjectsWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {
        // Lose Condition 1): lab health drops to 0
        if (!gameOver && curLabHealth <= 0) {
            // Enter GAMEOVER scene
            Debug.Log("GAME OVER");
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
