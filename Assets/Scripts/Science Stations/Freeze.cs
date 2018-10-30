using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

    private GameObject station;
    public AudioSource freezeSound;
    public AudioClip clip1;
    public AudioClip clip2;


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
                if(freezeSound.clip == clip1){
                    freezeSound.Play();
                    freezeSound.clip = clip2;
                }
                else if (freezeSound.clip == clip2)
                {
                    freezeSound.Play();
                    freezeSound.clip = clip1;
                }
                freezeSound.Play();
                hitTarget.GetComponent<EnemyStatus>().Freeze();
            }

        }
    }
}
