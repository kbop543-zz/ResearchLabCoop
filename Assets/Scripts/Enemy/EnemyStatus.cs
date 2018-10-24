using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatus : MonoBehaviour
{

    public bool frozen = false;
    public bool shrank = false;
    public bool blown = false;
    public bool oiled = false;
    public bool willDie = false;
    public bool falling = false;
    public float duraiton = 5f;

    public float originalSpeed;
    private IEnumerator curUnshrink;
    private IEnumerator curUnfreeze;
    private IEnumerator curUnoiled;

    private void Start()
    {
        originalSpeed = gameObject.GetComponent<EnemyMovement>().forwardSpeed;
    }

    //void FixedUpdate()
    //{
    //    if (shrank)
    //    {
    //        StartCoroutine(Unshrink());

    //        // Debug.Log("Waiting for unshrink!");
    //    }

    //    if (frozen)
    //    {
    //        StartCoroutine(Unfreeze());

    //        //Debug.Log("Waiting for unfreeze!");
    //    }

    //}

    public void Fall() {
        GetComponent<EnemyMovement>().chasing = false;
        GetComponent<EnemyMovement>().idling = false;
        GetComponent<EnemyMovement>().activated = false;
        GetComponent<EnemyMovement>().forwardSpeed = 0;

        falling = true;
        willDie = true;
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

        if (curUnshrink != null)
        {
            StopCoroutine(curUnshrink);
        }
        curUnshrink = Unshrink();
        StartCoroutine(curUnshrink);
        // Debug.Log("Waiting for unshrink!");
    }

    IEnumerator Unshrink()
    {
        yield return new WaitForSeconds(duraiton);

        if (falling) {
            yield break;
        }

        // restore original size after being unshrink
        while (transform.localScale.x < 15)
        {
            transform.localScale = transform.localScale + new Vector3(1f, 1f, 1f) * 3f * Time.deltaTime;
            transform.position = new Vector3(transform.position.x,
                                             transform.localScale.y / 2,
                                             transform.position.z);
            yield return null;
        }

        // restore original speed after being unshrink
        if (!frozen)
        {
            gameObject.GetComponent<EnemyMovement>().forwardSpeed = originalSpeed;
        }

        shrank = false;
        // Debug.Log("Unshrank!!");
    }

    public void Freeze()
    {
        frozen = true;
        gameObject.GetComponent<EnemyMovement>().forwardSpeed = 0;
        gameObject.GetComponent<NavMeshAgent>().speed = 0;
        gameObject.GetComponent<NavMeshAgent>().SetDestination(transform.position);
        gameObject.GetComponent<EnemyMovement>().chasing = false;
        gameObject.GetComponent<EnemyMovement>().idling = false;

        if (curUnfreeze != null)
        {
            StopCoroutine(curUnfreeze);
        }
        curUnfreeze = Unfreeze();
        StartCoroutine(curUnfreeze);
        // Debug.Log("Waiting for unfreeze!");
    }

    IEnumerator Unfreeze()
    {
        yield return new WaitForSeconds(duraiton);

        // restore original speed after being unfrozen
        gameObject.GetComponent<EnemyMovement>().forwardSpeed = originalSpeed;

        frozen = false;
        //Debug.Log("Unfrozen!!");
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

        if (falling) {
            yield break;
        }

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

    public void Oiling()
    {
        oiled = true;

        gameObject.GetComponent<EnemyMovement>().forwardSpeed = gameObject.GetComponent<EnemyMovement>().forwardSpeed / 2;

        if (curUnoiled != null)
        {
            StopCoroutine(curUnoiled);
        }
        curUnoiled = Unoil();
        StartCoroutine(curUnoiled);
    }

    IEnumerator Unoil()
    {
        yield return new WaitForSeconds(duraiton);

        gameObject.GetComponent<EnemyMovement>().forwardSpeed = originalSpeed;

        oiled = false;
    }

    public void Shock()
    {
        if (gameObject.GetComponent<EnemyStatus>().oiled)
        {
            Destroy(gameObject);
            GameManager.instance.GetComponent<GameConstants>().enemyKillCount += 1;
        }
        else
        {
            gameObject.GetComponent<EnemyMovement>().forwardSpeed = gameObject.GetComponent<EnemyMovement>().forwardSpeed * 2
        }
    }
}
