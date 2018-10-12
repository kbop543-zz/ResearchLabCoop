using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Status : MonoBehaviour {

    public bool frozen = false;
    public float duraiton = 5f;

    float originalSpeed = 100f;

    void FixedUpdate()
    {
        if (frozen)
        {
            StartCoroutine(waitForStatusEnd());

            Debug.Log("Waiting for unfreeze!");
        }

    }

    IEnumerator waitForStatusEnd()
    {
        yield return new WaitForSeconds(duraiton);
        frozen = false;
        gameObject.GetComponent<p2_movement>().speed = originalSpeed;
        Debug.Log("Unfrozen!!!");
    }

    public void Freeze()
    {
        frozen = true;
        gameObject.GetComponent<p2_movement>().speed = 0;
    }
}
