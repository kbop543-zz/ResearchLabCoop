﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public float startHealth = 100;
	public GameObject deathEffect;
	float health;
	public bool playerIsDead = false;
	public Image healthBar;
	public Transform respawnTransform;
    public AudioSource playerhurt;
    public AudioClip clip1, clip2, clip3;
    public GameObject gmtest;
    Color t_green;
    Color t_yellow;
    Color t_red;

    // Use this for initialization
    void Start () {
		health = startHealth;
        t_green = new Color(0.04f, 0.9f, 0.0f, 0.5f);
        t_yellow = new Color(1.0f, 1.0f, 0.0f, 0.5f);
        t_red = new Color(1.0f, 0.0f, 0.0f, 0.5f);

		healthBar.color = t_green;
        gmtest = GameObject.Find("GameManagerTest");

        // Respawn position is subject to change in future
        //respawnTransform = transform;

    }


	public void Die () {
        // Drop items before Die
        if (!GetComponent<PickOrDrop>().emptyHand)
        {
            GetComponent<PickOrDrop>().Dropdown();
        }

        if (gameObject){
			gameObject.SetActive(false);
			playerIsDead = true;

            Invoke("Respawn", 3);
                
		}
	}

	public void Respawn () {
		print("respawn");
        if (!gmtest.GetComponent<GameConstants>().gameOver)
        {
            transform.position = respawnTransform.position;
            transform.rotation = respawnTransform.rotation;
            transform.localScale = respawnTransform.localScale;

            health = startHealth;

            // Reset status
            if (gameObject.name == "P1(Clone)")
            {
                GetComponent<P1Status>().RestoreStatus();
            }
            else
            {
                GetComponent<P2Status>().RestoreStatus();
            }

            if (gameObject)
            {
                gameObject.SetActive(true);
                playerIsDead = false;
                healthBar.fillAmount = health;
                healthBar.color = t_green;
            }

            Debug.Log("Player has respawned");
        }
	}

	public void TakeDamage(float amount){

        health-=amount;

		healthBar.fillAmount = health/startHealth;
		// Debug.Log("fill amount" + healthBar.fillAmount );


		if(healthBar.fillAmount <= .80 & healthBar.fillAmount > .30){
			healthBar.color = t_yellow;
            // Debug.Log("fill should be yellow" );
            playerhurt.clip = clip2;
            playerhurt.Play();
        }

		else if(healthBar.fillAmount <= .30 ){
			healthBar.color = t_red;
            playerhurt.clip = clip3;
            playerhurt.Play();
            // Debug.Log("fill should be red" );
        }
		else{
			healthBar.color = t_green;
            playerhurt.clip = clip1;
            playerhurt.Play();
            // Debug.Log("fill should be red" );

        }

		if(health <= 0){
            // Drop items before Die
            //if (!GetComponent<PickOrDrop>().emptyHand)
            //{
            //    GetComponent<PickOrDrop>().Dropdown();
            //}

            Die();
		}
	}

	//private void OnCollisionEnter(Collision collision)
    //{
	//  if (collision.gameObject.tag == "Monster")
	//  {
	//		Debug.Log("collide with monster");
	//		TakeDamage(10);
	//	}
	//}
}
