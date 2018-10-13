using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

    private GameObject station;


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
                hitTarget.GetComponent<P1Status>().Freeze();
            }
            if (hitTarget.name == "P2(Clone)")
            {
                hitTarget.GetComponent<P2Status>().Freeze();
            }
            if (hitTarget.tag == "Monster")
            {
                hitTarget.GetComponent<EnemyStatus>().Freeze();
            }

        }
    }
}
