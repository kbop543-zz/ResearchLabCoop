using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{

    public bool frozen = false;
    public bool shrank = false;
    public bool blown = false;
    public bool willDie = false;
    public float duraiton = 5f;

    public float originalSpeed;

    private void Start()
    {
        originalSpeed = gameObject.GetComponent<EnemyMovement>().forwardSpeed;
    }

    void FixedUpdate()
    {
        if (shrank)
        {
            StartCoroutine(Unshrink());

            // Debug.Log("Waiting for unshrink!");
        }

        if (frozen)
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
        if (!frozen)
        {
            gameObject.GetComponent<EnemyMovement>().forwardSpeed = originalSpeed;
        }

        // restore original size after being unshrink
        if (transform.localScale.x < 15)
        {
            transform.localScale = transform.localScale + new Vector3(1f, 1f, 1f) * 3.0f * Time.deltaTime;
            transform.position = new Vector3(transform.position.x,
                                             transform.localScale.y / 2,
                                             transform.position.z);
        }

        // Debug.Log("Unshrank!!");
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

    public void BlowAway(float seconds)
    {
        blown = true;
        float curSpeed = gameObject.GetComponent<EnemyMovement>().forwardSpeed;
        gameObject.GetComponent<EnemyMovement>().forwardSpeed = 0;

        StartCoroutine(UnBlown(seconds, curSpeed));
    }

    IEnumerator UnBlown(float seconds, float curSpeed)
    {
        yield return new WaitForSeconds(seconds);
        blown = false;

        // restore original speed after being unfrozen
        if (frozen)
        {
        }
        else if (shrank)
        {
            gameObject.GetComponent<EnemyMovement>().forwardSpeed = curSpeed;
        }
        else
        {
            gameObject.GetComponent<EnemyMovement>().forwardSpeed = originalSpeed;
        }

    }
}
