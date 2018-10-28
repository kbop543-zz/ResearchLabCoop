using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

    private GameObject station;
    //public AudioSource freezeSound;


	void Start () {

        station = GameObject.Find("FreezeStation(Clone)");

    }
    
    public void OnTriggerStay(Collider other)
    {
        if (station.GetComponent<StationStatus>().activated)
        {
            GameObject hitTarget = other.transform.root.gameObject;

            if (hitTarget.name == "P1(Clone)")
            {
                //freezeSound.Play();
                hitTarget.GetComponent<P1Status>().Freeze();
            }
            if (hitTarget.name == "P2(Clone)")
            {
                //freezeSound.Play();
                hitTarget.GetComponent<P2Status>().Freeze();
            }
            if (hitTarget.tag == "Monster")
            {
                //freezeSound.Play();
                hitTarget.GetComponent<EnemyStatus>().Freeze();
            }

        }
    }
}
