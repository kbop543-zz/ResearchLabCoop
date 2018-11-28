using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignite : MonoBehaviour
{

    private GameObject station;
    public AudioSource FireSound;
    public AudioSource oildialogue;
    public bool play;

    void Start()
    {
        play = true;
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
                if (play)
                {  


                    if (hitTarget.GetComponent<EnemyStatus>().oiled)
                    {
                        FireSound.Play();
                        oildialogue.Stop();
                        play = false;
                        Invoke("Whatever", 10);
                    }
                    else
                    {
                        oildialogue.Play();
                    }
                }
            }
            else if (hitTarget.name.Contains("P1"))
            {
                Debug.Log("ignite P1");
                hitTarget.GetComponent<P1Status>().Shock();
                if (hitTarget.GetComponent<P1Status>().oiled)
                {
                    FireSound.Play();
                }
            }
            else if (hitTarget.name.Contains("P2"))
            {
                Debug.Log("ignite P2");
                hitTarget.GetComponent<P2Status>().Shock();
                if (hitTarget.GetComponent<P2Status>().oiled)
                {
                    FireSound.Play();
                }
            }

        }
    }
    void Whatever(){
        play = true;
    }
}
