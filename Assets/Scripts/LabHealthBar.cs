using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabHealthBar : MonoBehaviour {

	public Image currentHealthbar;
	public Text ratioText;


	// Use this for initialization
	void Start () {
		UpdateHealthBar();

	}

	// Update is called once per frame
	public void UpdateHealthBar () {
		GameObject gm = GameObject.FindWithTag("GameManager");
		var currLabHealth = gm.GetComponent<gameConstants>().curLabHealth;
		var maxLabHealth = gm.GetComponent<gameConstants>().maxLabHealth;

		float ratio = currLabHealth / maxLabHealth;
		currentHealthbar.rectTransform.localScale = new Vector3(ratio,1,1);
		ratioText.text = (ratio*100).ToString() + '%';

	}
}
