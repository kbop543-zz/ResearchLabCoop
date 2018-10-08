using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject terrainTiles;
    public GameObject player1;
    public GameObject player2;
    public GameObject researchLab;
    public GameObject enemySpawner;
    public GameObject orb;
    public GameObject eventSystem;
    public GameObject labHealth;
    public GameObject crossBow;
    public GameObject collector;
    public GameObject thrinkStation;
    public GameObject thrinkTile;
    public GameObject holeStation;
    public GameObject holeTile;
    public GameObject OutsideWallBig;
    public GameObject OutsideWallSmall1;
    public GameObject OutsideWallSmall2;
    public GameObject lightBulbs;
    //public GameObject lighting;

    public void SetupScene() {
        //Instantiate(terrainTiles, new Vector3(-514f, 0, -472f), Quaternion.identity);
        //Instantiate(player1, new Vector3(144.4f, 8.1f, 330.3f), Quaternion.identity);
        //Instantiate(player2, new Vector3(167.68f, 9.5f, 330.3f), Quaternion.identity);
        //Instantiate(researchLab, new Vector3(0f, 25.7f, -6f), Quaternion.identity);
        //Instantiate(enemySpawner, new Vector3(98f, 34.42082f, 215.97f), Quaternion.identity);
        //Instantiate(orb, new Vector3(0f, 0f, 0f), Quaternion.identity);
        //Instantiate(eventSystem, new Vector3(0f, 0f, 0f), Quaternion.identity);
        //Instantiate(labHealth, new Vector3(333f, 196.5f, 0f), Quaternion.identity);

        Instantiate(terrainTiles);
        Instantiate(player1);
        Instantiate(player2);
        Instantiate(researchLab);
        Instantiate(enemySpawner);
        Instantiate(orb);
        Instantiate(eventSystem);
        Instantiate(labHealth);
        Instantiate(crossBow);
        Instantiate(collector);
        Instantiate(thrinkStation);
        Instantiate(thrinkTile);
        Instantiate(holeStation);
        Instantiate(holeTile);
        //Instantiate(lighting);
        Instantiate(OutsideWallBig);
        Instantiate(OutsideWallSmall1);
        Instantiate(OutsideWallSmall2);
        Instantiate(lightBulbs);

    }
}
