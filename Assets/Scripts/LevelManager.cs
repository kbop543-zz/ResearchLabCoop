using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject terrainTile;
    //public GameObject bigtile;
    //public GameObject smalltile;
    public GameObject player1;
    public GameObject player2;
    public GameObject HUDCanvas;
    public GameObject enemySpawner;
    public GameObject orb;
    public GameObject eventSystem;
    public GameObject crossBow;

    //public GameObject collector;
    public GameObject wall;
    public GameObject light;
    //public GameObject shrinkpipe;
    public GameObject thrinkStation;
    public GameObject thrinkTile;
    public GameObject holeStation;
    public GameObject holes;
    //public GameObject holeTile0;
    //public GameObject holeTile1;
    //public GameObject holeTile2;
    public GameObject lightBulbs;
    //public GameObject lighting;
    public GameObject freezeStation;
    public GameObject freezeTile;
    //public GameObject freezePipe;
    public GameObject electricityStation;
    public GameObject electricityTile;
    //public GameObject electricityPipe;
    //public GameObject ShockWaveGun1;
    //public GameObject ShockWaveGun2;
    public GameObject BackgroundSound;
    public GameObject navmesh;
    public GameObject enemySpawner2, enemySpawner3;

    public GameObject p1;
    public GameObject p2;
    public GameObject hud;

    public void SetupScene() {
        //Instantiate(terrainTiles, new Vector3(-514f, 0, -472f), Quaternion.identity);
        //Instantiate(player1, new Vector3(144.4f, 8.1f, 330.3f), Quaternion.identity);
        //Instantiate(player2, new Vector3(167.68f, 9.5f, 330.3f), Quaternion.identity);
        //Instantiate(researchLab, new Vector3(0f, 25.7f, -6f), Quaternion.identity);
        //Instantiate(enemySpawner, new Vector3(98f, 34.42082f, 215.97f), Quaternion.identity);
        //Instantiate(orb, new Vector3(0f, 0f, 0f), Quaternion.identity);
        //Instantiate(eventSystem, new Vector3(0f, 0f, 0f), Quaternion.identity);
        //Instantiate(labHealth, new Vector3(333f, 196.5f, 0f), Quaternion.identity);

        Instantiate(terrainTile);
        //Instantiate(bigtile);
        Instantiate(wall);
        Instantiate(light);
        //Instantiate(smalltile);
        p1 = Instantiate(player1) as GameObject;
        p2 = Instantiate(player2) as GameObject;
        hud = Instantiate(HUDCanvas) as GameObject;
        //Instantiate(researchLab);
        Instantiate(enemySpawner);
        Instantiate(orb);
        Instantiate(eventSystem);

        //Instantiate(crossBow);
        //Instantiate(collector);

        Instantiate(thrinkStation);
        Instantiate(thrinkTile);
        //Instantiate(shrinkpipe);
        Instantiate(holeStation);
        //Instantiate(holeTile0);
        //Instantiate(holeTile1);
        //Instantiate(holeTile2);
        Instantiate(holes);
        //Instantiate(lighting);
        Instantiate(lightBulbs);
        Instantiate(freezeStation);
        //Instantiate(freezePipe);
        Instantiate(freezeTile);
        Instantiate(electricityStation);
        Instantiate(electricityTile);
        //Instantiate(electricityPipe);
        //Instantiate(ShockWaveGun1);
        //Instantiate(ShockWaveGun2, new Vector3(135f, 2.62f, 400f), Quaternion.identity);
        Instantiate(enemySpawner2);
        Instantiate(enemySpawner3);
        Instantiate(BackgroundSound);
        Instantiate(navmesh);

    }
}
