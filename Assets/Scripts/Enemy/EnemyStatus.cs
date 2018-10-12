using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{

    public bool frozen = false;
    public float duraiton = 5f;

    float originalSpeed = 20f;

    void FixedUpdate()
    {
        if (frozen)
        {
            StartCoroutine(waitForStatusEnd());

            //Debug.Log("Waiting for unfreeze!");
        }

    }

    IEnumerator waitForStatusEnd()
    {
        yield return new WaitForSeconds(duraiton);
        frozen = false;
        gameObject.GetComponent<EnemyMovement>().forwardSpeed = originalSpeed;
        //Debug.Log("Unfrozen!!!");
    }

    public void Freeze()
    {
        frozen = true;
        gameObject.GetComponent<EnemyMovement>().forwardSpeed = 0;
    }
}
