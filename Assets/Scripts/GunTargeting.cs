using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTargeting : MonoBehaviour {

    public Canvas gunTargetUI;
    public GameObject playerModel;

	// Use this for initialization
	void Start () {
        gunTargetUI.transform.rotation = playerModel.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        gunTargetUI.transform.rotation = playerModel.transform.rotation;
    }
}
