using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilHit : MonoBehaviour
{

    //public float thrust = 150f;
    public float extendRate = 550f;
    private GameObject shooter;
    private Rigidbody rb;
    //private bool isColliding;

    private void Start()
    {
        StartCoroutine(increaseXscale());
    }

    public void updateHolder(GameObject holder)
    {
        shooter = holder;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject hitTarget = other.transform.root.gameObject;
        //float force = thrust;
        //if (hitTarget.tag == "Monster" || hitTarget.tag == "Player")
        if (hitTarget.tag == "Monster")
        {
            //if (hitTarget.name == "P1(Clone)")
            //{
            //    if (!hitTarget.GetComponent<P1Status>().blown)
            //    {
            //        hitTarget.GetComponent<P1Status>().BlowAway(0.5f);

            // If Frozen
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
            //else if (hitTarget.name == "P2(Clone)")
            //{
            //    if (!hitTarget.GetComponent<P2Status>().blown)
            //    {
            //        hitTarget.GetComponent<P2Status>().BlowAway(0.5f);

            // If Frozen
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
            //else
            //{
            if (!hitTarget.GetComponent<EnemyStatus>().oiled)
            {
                hitTarget.GetComponent<EnemyStatus>().Oiling();

                hitTarget.GetComponent<EnemyMovement>().changeCurTarget(shooter);

            }
            else
            {
                return;
            }
            //force *= 2f;
            //}

            //hitTarget.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.normalized * force, ForceMode.Impulse);
        }

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

    IEnumerator increaseXZscales()
    {
        while (true)
        {
            transform.localScale = transform.localScale +
                                   new Vector3(extendRate * Time.deltaTime,
                                               0,
                                               extendRate * Time.deltaTime);
            yield return null;
        }
    }
}
