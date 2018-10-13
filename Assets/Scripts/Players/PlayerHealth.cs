using System.Collections;
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


	// Use this for initialization
	void Start () {
		health = startHealth;
		healthBar.color = Color.green;

        // Respawn position is subject to change in future
        //respawnTransform = transform;

    }


	public void Die () {

		if(gameObject){
			gameObject.SetActive(false);
			playerIsDead = true;

			Invoke("Respawn",3);
		}
	}

	public void Respawn () {
		transform.position = respawnTransform.position;
		transform.rotation = respawnTransform.rotation;

		health = startHealth;

		if(gameObject){
			gameObject.SetActive(true);
			playerIsDead = false;
			healthBar.fillAmount = health;
			healthBar.color = Color.green;
		}

		Debug.Log("Player has respawned");
	}

	public void TakeDamage(float amount){
		health-=amount;

		healthBar.fillAmount = health/startHealth;
		// Debug.Log("fill amount" + healthBar.fillAmount );


		if(healthBar.fillAmount <= .80 & healthBar.fillAmount > .30){
			healthBar.color = Color.yellow;
			// Debug.Log("fill should be yellow" );
		}

		else if(healthBar.fillAmount <= .30 ){
			healthBar.color = Color.red;
			// Debug.Log("fill should be red" );
		}
		else{
			healthBar.color = Color.green;
			// Debug.Log("fill should be red" );

		}

		if(health <= 0){
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
