using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public float startHealth = 100;
	public GameObject deathEffect;
	float health;

	public Image healthBar;

	// Use this for initialization
	void Start () {
		health = startHealth;

	}

	// Update is called once per frame
	public void Die () {

		if(gameObject){
			Destroy(gameObject);
		}


	}

	public void TakeDamage(float amount){
		health-=amount;

		healthBar.fillAmount = health/startHealth;
		Debug.Log("fill amount" + healthBar.fillAmount );

		if(health <= 0){
			Die();
		}
	}

	private void OnCollisionEnter(Collision collision)
  {
	  if (collision.gameObject.tag == "Monster")
	  {
			Debug.Log("collide with monster");
			TakeDamage(10);
		}
	}



}
