using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrink : MonoBehaviour {

    public float thrinkRate = 3.0f;
    public int speedDecreaseRate = 5;
    //public float thrinkMin = 5.0f;
    public float maxIntensity = 20f;
    private GameObject station;
    private Light myLight;
    private bool flashing;
    public AudioSource shrinkSound;
    public AudioClip clip3, clip4;
    private void Start()
    {
        //thrinkMin = GameObject.Find("P1(Clone)").transform.localScale.x / 3;
        station = GameObject.Find("ShrinkStation(Clone)");
        myLight = GetComponentInChildren<Light>();
        myLight.intensity = 0f;
        flashing = false;
        shrinkSound.clip = clip3;
    }

    private void OnTriggerStay(Collider other)
    {
        if (station.GetComponent<StationStatus>().activated) {
            GameObject hitTarget = other.transform.root.gameObject;

            float thrinkMin = 0;

            if (hitTarget.name == "P1(Clone)") {
                thrinkMin = hitTarget.GetComponent<P1Status>().originalScale / 2;
            }else if (hitTarget.name == "P2(Clone)") {
                thrinkMin = hitTarget.GetComponent<P2Status>().originalScale / 2;
            }
            else if (hitTarget.tag == "Monster") {
                thrinkMin = hitTarget.GetComponent<EnemyStatus>().originalScale / 2;
            }

            if (hitTarget.transform.localScale.x > thrinkMin)
            {
                // Change size
                hitTarget.transform.localScale = hitTarget.transform.localScale -
                                                 new Vector3(1f, 1f, 1f) * thrinkRate * Time.deltaTime;

                // Change position y
                hitTarget.transform.position = new Vector3(hitTarget.transform.position.x,
                                                           hitTarget.transform.localScale.y / 2,
                                                           hitTarget.transform.position.z);

                // Change speed
                float ratio = Mathf.Pow((1.0f - speedDecreaseRate * thrinkRate * Time.deltaTime / hitTarget.transform.position.x), speedDecreaseRate);

                if (hitTarget.name == "P1(Clone)")
                {
                    hitTarget.GetComponent<P1Status>().Shrink(1/ratio);
                }
                else if (hitTarget.name == "P2(Clone)")
                {
                    hitTarget.GetComponent<P2Status>().Shrink(1/ratio);
                }
                else if (hitTarget.tag == "Monster")
                {
                    if(shrinkSound.clip == clip3){
                        //shrinkSound.Play();
                        shrinkSound.clip = clip4;
                    }
                    else if(shrinkSound.clip == clip4){
                        //shrinkSound.Play();
                        shrinkSound.clip = clip3;
                    }
                    shrinkSound.Play();
                    hitTarget.GetComponent<EnemyStatus>().Shrink(1/ratio);

                }
            }
        }

    }

    private void FixedUpdate()
    {
        if (station.GetComponent<StationStatus>().activated && !flashing) {
            myLight.intensity = maxIntensity;
            flashing = true;
        }
        else if (!station.GetComponent<StationStatus>().activated && flashing)
        {
            myLight.intensity = 0f;
            flashing = false;
        }

    }

}
