using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class audio : MonoBehaviour {

    public AudioSource clickSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.JoystickButton0)){
            clickSound.Play();
        }
	}
}
