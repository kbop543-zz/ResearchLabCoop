using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transTerrainLab : MonoBehaviour {

    //public GameObject player;
    public Vector3 translocation;
	
	void OnCollisionEnter(Collision col) 
    {
        translocate(col.gameObject);
    }

    public void translocate(GameObject col)
    {
        col.transform.position += translocation;
    }
}
