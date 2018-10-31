using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float minDistance;// = 20f;
    public float forwardSpeed = 75f;

    public float sightRange;// = 115f;
    public float idleDuration = 5.0f;
    public int damage = 10;
    public float attackCD = 1.5f;
    public float navCD = 0.1f;
    public NavMeshAgent navAgent;
    private GameObject[] players;
    //private Transform LabTransform;
    private GameObject gm;
    private Vector3 DestinationPos;
    private Vector3 curVelocity;
    private GameObject curTargetPlayer;
    private float curCD;
    private float curNavCD;
    public bool chasing;
    public bool idling;
    public bool activated;

    // Use this for initialization
    private void Start()
    {
        //LabTransform = GameObject.FindGameObjectWithTag("Lab").transform;
        sightRange = 500f;
        minDistance = transform.localScale.x + 25f;
        GetComponent<NavMeshAgent>().stoppingDistance = minDistance;
        gm = GameObject.FindWithTag("GameManager");
        players = gm.GetComponent<GameConstants>().players;
        chasing = false;
        idling = false;
        activated = false;
        curCD = attackCD;
        curNavCD = navCD;
        navAgent.speed = 0;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        SearchForTarget();

        if (activated) {
            if (GetComponent<EnemyStatus>().frozen || GetComponent<EnemyStatus>().blown) {
                return;
            }

            if (true) {
            //if (chasing) {
                Chase();
            }

            // Not chasing && not idling => start idling
            //else {
            //    if (!idling) {
            //        idling = true;
            //        StartCoroutine(Idle());
            //    }
            //}

            // Recharge attack
            if (curCD < attackCD) {
                curCD += Time.deltaTime;
            }

            // Count re-navigation CD
            if (curNavCD < navCD) {
                curNavCD += Time.deltaTime;
            }
        }
    }

    public void changeCurTarget (GameObject target) {
        curTargetPlayer = target;
    }

    public void SearchForTarget () {
        if (!chasing) {
            if (players[0].GetComponent<PlayerHealth>().playerIsDead &&
                players[1].GetComponent<PlayerHealth>().playerIsDead)
            {
                return;
            }

            if (!players[0].GetComponent<PlayerHealth>().playerIsDead) {
                //targetPos = players[0].transform.position;
                curTargetPlayer = players[0];
            }

            if (!players[1].GetComponent<PlayerHealth>().playerIsDead) {
                if (!players[0].GetComponent<PlayerHealth>().playerIsDead) {
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
        if(curTargetPlayer == null || curTargetPlayer.GetComponent<PlayerHealth>().playerIsDead) {
            chasing = false;
            navAgent.speed = 0;
            return;
        }

        DestinationPos = new Vector3(curTargetPlayer.transform.position.x,
                                     transform.position.y,
                                     curTargetPlayer.transform.position.z);

        //transform.LookAt(DestinationPos);

        if (Vector3.Distance(transform.position, DestinationPos) > 2 * sightRange) {
            chasing = false;
            navAgent.speed = 0;
            return;
        }
        else if (Vector3.Distance(transform.position, DestinationPos) >= minDistance)
        {
            //curVelocity = DestinationPos - transform.position;
            //curVelocity.Normalize();
            //curVelocity *= forwardSpeed;
            //GetComponent<Rigidbody>().velocity = curVelocity;

            if (curNavCD >= navCD) {
                navAgent.speed = forwardSpeed;
                navAgent.SetDestination(DestinationPos);
                curNavCD = 0f;
            }

        }
        else {
            // Attack target when in attack range
            if (curCD >= attackCD) {
                //Debug.Log("Attack!");
                curTargetPlayer.GetComponent<PlayerHealth>().TakeDamage(damage);
                curCD = 0f;
            }

            navAgent.speed = 0;
            navAgent.SetDestination(transform.position);

            //GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);

        }
    }

    //private IEnumerator Idle() {
    //    // Pick random direction

    //    Vector3 idleDir = new Vector3(Random.Range(-75, 75), 0, Random.Range(-75, 75));
    //    idleDir += curTargetPlayer.transform.position - transform.position;
    //    idleDir.y = 0;
    //    idleDir.Normalize();
    //    float stopDuration = Random.Range(1, 3) * idleDuration / 10;
    //    float walkSpeed = 3 * forwardSpeed / 5;
    //    // Vector3 lookTo;

    //    // Idle
    //    navAgent.speed = 0;
    //    float curTime = 0.0f;
    //    while (idling && (curTime < stopDuration)) {
    //        curTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    if (!idling)
    //    {
    //        //GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
    //        yield break;
    //    }

    //    // Walk
    //    Vector3 dest = transform.position + idleDir * forwardSpeed;
    //    navAgent.speed = walkSpeed;
    //    navAgent.SetDestination(dest);
    //    while (idling && curTime < idleDuration && Vector3.Distance(transform.position, dest) > 0) {
    //        //lookTo = transform.position + idleDir;
    //        //transform.LookAt(lookTo);
    //        //GetComponent<Rigidbody>().velocity = idleDir * walkSpeed;
    //        curTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    // End
    //    navAgent.speed = 0;
    //    navAgent.SetDestination(transform.position);
    //    //GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
    //    idling = false;
    //    yield return null;
    //}

}
