using UnityEngine;

public class gameConstants : MonoBehaviour {

    public int maxLabHealth = 100;
    public int curLabHealth;
    public bool gameOver;

	// Use this for initialization
	void Start () {
        curLabHealth = maxLabHealth;
        gameOver = false;

    }
	
	// Update is called once per frame
	void Update () {
        // Lose Condition 1): lab health drops to 0
        if (!gameOver && curLabHealth <= 0) {
            // Enter GAMEOVER scene
            Debug.Log("GAME OVER");
            gameOver = true;
        }
	}
}
