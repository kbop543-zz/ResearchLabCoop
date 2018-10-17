using System.Collections;
using UnityEngine;

public class BigShotHit : MonoBehaviour {

    public float thrust = 150f;
    public float extendRate = 550f;
    private GameObject shooter;
    private Rigidbody rb;
    //private bool isColliding;

    private void Start()
    {
        StartCoroutine(increaseXscale());
    }

    public void updateHolder(GameObject holder) {
        shooter = holder;
    }

    private void OnTriggerEnter(Collider other)     {
        GameObject hitTarget = other.transform.root.gameObject;
        float force = thrust;
        if (hitTarget.tag == "Monster" || hitTarget.tag == "Player") {
            if (hitTarget.name == "P1(Clone)") {
                if (!hitTarget.GetComponent<P1Status>().blown)
                {
                    hitTarget.GetComponent<P1Status>().BlowAway(0.5f);

                    // If Frozen
                }
                else {
                    return;
                }
            }
            else if (hitTarget.name == "P2(Clone)") {
                if (!hitTarget.GetComponent<P2Status>().blown)
                {
                    hitTarget.GetComponent<P2Status>().BlowAway(0.5f);

                    // If Frozen
                }
                else {
                    return;
                }
            }
            else {
                if (!hitTarget.GetComponent<EnemyStatus>().blown)
                {
                    hitTarget.GetComponent<EnemyStatus>().BlowAway(0.5f);

                    // If Frozen
                    if (hitTarget.GetComponent<EnemyStatus>().frozen) {
                        Destroy(hitTarget);
                        GameManager.instance.GetComponent<GameConstants>().enemyKillCount += 1;
                    }
                    // If normal state
                    else {
                        hitTarget.GetComponent<EnemyMovement>().changeCurTarget(shooter);
                    }

                }
                else {
                    return;
                }
                //force *= 2f;
            }

            hitTarget.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);         }   
    }

    IEnumerator increaseXscale()
    {
        while (true)
        {
            transform.localScale = transform.localScale +
                                   new Vector3(extendRate * Time.deltaTime,
                                               0,
                                               0);
            yield return null;
        }
    }

    IEnumerator increaseXZscales(){
        while(true){
            transform.localScale = transform.localScale +
                                   new Vector3(extendRate * Time.deltaTime,
                                               0,
                                               extendRate * Time.deltaTime);
            yield return null;
        }
    }
}
