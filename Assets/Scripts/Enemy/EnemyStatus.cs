using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{

    public bool frozen = false;
    public bool shrank = false;
    public float duraiton = 5f;

    float originalSpeed = 20f;

    void FixedUpdate()
    {
        if (shrank)
        {
            StartCoroutine(Unshrink());

            Debug.Log("Waiting for unshrink!");
        }
        else if (frozen)
        {
            StartCoroutine(Unfreeze());

            //Debug.Log("Waiting for unfreeze!");
        }

    }

    //IEnumerator waitForStatusEnd(bool status)
    //{
    //    yield return new WaitForSeconds(duraiton);
    //    status = false;
    //    gameObject.GetComponent<EnemyMovement>().forwardSpeed = originalSpeed;
    //    Debug.Log("Effect debuffed!!");
    //}

    public void Shrink(float ratio)
    {
        shrank = true;
        gameObject.GetComponent<EnemyMovement>().forwardSpeed = gameObject.GetComponent<EnemyMovement>().forwardSpeed * ratio;
    }

    IEnumerator Unshrink()
    {
        yield return new WaitForSeconds(duraiton);
        shrank = false;

        // restore original speed after being unshrink
        gameObject.GetComponent<EnemyMovement>().forwardSpeed = originalSpeed;
<<<<<<< HEAD
        //Debug.Log("Unfrozen!!!");
=======

        // restore original size after being unshrink
        if (transform.localScale.x < 15)
        {
            transform.localScale = transform.localScale + new Vector3(1f, 1f, 1f) * 3.0f * Time.deltaTime;
            transform.position = new Vector3(transform.position.x,
                                             transform.localScale.y / 2,
                                             transform.position.z);
        }

        Debug.Log("Unshrank!!");
>>>>>>> 5233dc78e91bcbee328cb5b8288ebb90f813f72d
    }

    public void Freeze()
    {
        frozen = true;
        gameObject.GetComponent<EnemyMovement>().forwardSpeed = 0;
    }

    IEnumerator Unfreeze()
    {
        yield return new WaitForSeconds(duraiton);
        frozen = false;

        // restore original speed after being unfrozen
        gameObject.GetComponent<EnemyMovement>().forwardSpeed = originalSpeed;

        Debug.Log("Unfrozen!!");
    }
}
