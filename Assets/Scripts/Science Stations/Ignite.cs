using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignite : MonoBehaviour
{

    private GameObject station;


    void Start()
    {

        station = GameObject.Find("ElectricityStation(Clone)");

    }

    public void OnTriggerStay(Collider other)
    {
        if (station.GetComponent<StationStatus>().activated)
        {
            GameObject hitTarget = other.transform.root.gameObject;

            //if (hitTarget.name == "P1(Clone)")
            //{
            //    hitTarget.GetComponent<P1Status>().Shock();
            //}
            //if (hitTarget.name == "P2(Clone)")
            //{
            //    hitTarget.GetComponent<P2Status>().Shock();
            //}
            if (hitTarget.tag == "Monster")
            {
                Debug.Log("ignite monster");
                hitTarget.GetComponent<EnemyStatus>().Shock();
            }

        }
    }
}
