using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundScript : MonoBehaviour {

    public AudioClip bg1;
    public AudioClip bg2;
    //public AudioClip bg3;
    public GameObject gmtest;
    public AudioSource bg;
    public bool play1,play2;
	// Use this for initialization
	void Awake(){
        //DontDestroyOnLoad(gameObject);
        gmtest = GameObject.Find("GameManagerTest");
        play1 = true;
        play2 = true;
	}
    void Update()
    {
        if(gmtest.GetComponent<GameConstants>().completeLvl1){
            if(play1){
                bg.clip = bg1;
                bg.Play();
                play1 = false;
            }

        }
        if (gmtest.GetComponent<GameConstants>().completeLvl2)
        {
            if (play2)
            {
                bg.clip = bg2;
                bg.Play();
                play2 = false;
            }
        }
    }
}
