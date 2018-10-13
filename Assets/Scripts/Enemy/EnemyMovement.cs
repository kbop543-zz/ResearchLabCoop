using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float minDistance = 20f;
    public float forwardSpeed = 45f;
    public float sightRange = 115f;
    public float idleDuration = 5.0f;
    public int damage = 10;
    public float attackCD = 1.5f;
    private GameObject[] players;
    //private Transform LabTransform;
    private GameObject gm;
    private Vector3 DestinationPos;
    private Vector3 curVelocity;
    private GameObject curTargetPlayer;
    private float curCD;
    public bool chasing;
    public bool idling;
    public bool activated;

    // Use this for initialization
    private void Start()
    {
        //LabTransform = GameObject.FindGameObjectWithTag("Lab").transform;
        gm = GameObject.FindWithTag("GameManager");
        players = gm.GetComponent<GameConstants>().players;
        chasing = false;
        idling = false;
        activated = false;
        curCD = attackCD;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        SearchForTarget();

        if (activated) {
            if (GetComponent<EnemyStatus>().frozen) {
                return;
            }

            if (chasing) {
                Chase();
            }
            // Not chasing && not idling => start idling
            else {
                if (!idling) {
                    idling = true;
                    StartCoroutine(Idle());
                }
            }

            // Recharge attack
            if (curCD < attackCD) {
                curCD += Time.deltaTime;
            }
        }
    }

    private void SearchForTarget () {
        if (!chasing) {
            if (players[0] == null && players[1] == null)
            {
                return;
            }

            if (players[0] != null) {
                //targetPos = players[0].transform.position;
                curTargetPlayer = players[0];
            }

            if (players[1] != null) {
                if (players[0] != null) {
                    float p1Dis = Vector3.Distance(players[0].transform.position, transform.position);
                    float p2Dis = Vector3.Distance(players[1].transform.position, transform.position);
                    if (p2Dis < p1Dis) {
                        //targetPos = players[1].transform.position;
                        curTargetPlayer = players[1];
                    }
                }
                else {
                    //targetPos = players[1].transform.position;
                    curTargetPlayer = players[1];
                }

            }

            // Verify range
            if (Vector3.Distance(curTargetPlayer.transform.position, transform.position) < sightRange) {
                chasing = true;
                idling = false;

                //if (!activated) {
                //    activated = true;
                //}
            }
        }
    }

    private void Chase() {
        //DestinationPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);
        if(curTargetPlayer == null) {
            chasing = false;
            return;
        }

        DestinationPos = new Vector3(curTargetPlayer.transform.position.x,
                                     transform.position.y,
                                     curTargetPlayer.transform.position.z);

        transform.LookAt(DestinationPos);

        if (Vector3.Distance(transform.position, DestinationPos) > 2 * sightRange) {
            chasing = false;
            return;
        }
        else if (Vector3.Distance(transform.position, DestinationPos) >= minDistance)
        {
            curVelocity = DestinationPos - transform.position;
            curVelocity.Normalize();
            curVelocity *= forwardSpeed;
            GetComponent<Rigidbody>().velocity = curVelocity;

        }
        else {
            // Attack target when in attack range
            if (curCD >= attackCD) {
                Debug.Log("Attack!");
                curTargetPlayer.GetComponent<PlayerHealth>().TakeDamage(damage);
                curCD = 0f;
            }

            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);

        }
    }

    private IEnumerator Idle() {
        // Pick random direction
        Vector3 idleDir = new Vector3(Random.Range(-3, 6), 0, Random.Range(-10, 4));
        idleDir.Normalize();
        float stopDuration = Random.Range(1, 3) * idleDuration / 10;
        float walkSpeed = 3 * forwardSpeed / 5;
        Vector3 lookTo;

        // Idle
        float curTime = 0.0f;
        while (idling && (curTime < stopDuration)) {
            curTime += Time.deltaTime;
            yield return null;
        }

        if (!idling)
        {
            yield break;
        }

        // Walk
        while (idling && (curTime < idleDuration)) {
            lookTo = transform.position + idleDir;
            transform.LookAt(lookTo);
            GetComponent<Rigidbody>().velocity = idleDir * walkSpeed;
            curTime += Time.deltaTime;
            yield return null;
        }

        if (!idling)
        {
            yield break;
        }

        // End
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f); 
        idling = false;
        yield return null;
    }

}
