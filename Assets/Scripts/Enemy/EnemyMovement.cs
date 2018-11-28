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
    public float chgTarCD = 3f;
    public float chgTarDis = 200f;
    public NavMeshAgent navAgent;
    private GameObject[] players;
    //private Transform LabTransform;
    private GameObject gm;
    private Vector3 DestinationPos;
    private Vector3 curVelocity;
    private GameObject curTargetPlayer;
    private float curCD;
    private float curNavCD;
    private Animator anim;
    private float curChgTarCD;
    public bool chasing;
    public bool idling;
    public bool activated;

    // Use this for initialization
    private void Start()
    {
        //LabTransform = GameObject.FindGameObjectWithTag("Lab").transform;
        sightRange = 500f;
        chgTarCD = 3f;
        chgTarDis = 200f;
        curChgTarCD = 0;
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
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("attack", false);

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        SearchForTarget();

        if (activated) {
            if (GetComponent<EnemyStatus>().frozen || GetComponent<EnemyStatus>().blown) {
                if (GetComponent<EnemyStatus>().frozen) {
                    anim.speed = 0;
                }
                return;
            }
            else if (!GetComponent<EnemyStatus>().frozen && anim.speed.Equals(0)) {
                anim.speed = 1;
            }

            if (!anim.GetBool("attack")) {
                Chase();
            }

            if (anim.GetBool("idle") && curTargetPlayer != null)
            {
                var targetRotation = Quaternion.LookRotation(DestinationPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime);
            }

            // Not chasing && not idling => start idling
            //else {
            //    if (!idling) {
            //        idling = true;
            //        StartCoroutine(Idle());
            //    }
            //}

            // Recharge attack
            if (curCD < attackCD && !anim.GetBool("attack")) {
                curCD += Time.deltaTime;
            }

            // Count re-navigation CD
            if (curNavCD < navCD) {
                curNavCD += Time.deltaTime;
            }

            // Count switch target CD
            if (curChgTarCD < chgTarCD) {
                curChgTarCD += Time.deltaTime;
            }
        }
    }

    public void changeCurTarget (GameObject target) {
        curTargetPlayer = target;
    }

    public void SearchForTarget () {
        if (!chasing || (curChgTarCD >= chgTarCD)) {
            if (curTargetPlayer == null || curTargetPlayer.GetComponent<PlayerHealth>().playerIsDead)
            {
                if (players[0].GetComponent<PlayerHealth>().playerIsDead &&
                    players[1].GetComponent<PlayerHealth>().playerIsDead)
                {
                    return;
                }

                if (!players[0].GetComponent<PlayerHealth>().playerIsDead)
                {
                    //targetPos = players[0].transform.position;
                    curTargetPlayer = players[0];
                }

                if (!players[1].GetComponent<PlayerHealth>().playerIsDead)
                {
                    if (!players[0].GetComponent<PlayerHealth>().playerIsDead)
                    {
                        float p1Dis = Vector3.Distance(players[0].transform.position, transform.position);
                        float p2Dis = Vector3.Distance(players[1].transform.position, transform.position);
                        if (p2Dis < p1Dis)
                        {
                            //targetPos = players[1].transform.position;
                            curTargetPlayer = players[1];
                        }
                    }
                    else
                    {
                        //targetPos = players[1].transform.position;
                        curTargetPlayer = players[1];
                    }

                }

                // Verify range
                if (Vector3.Distance(curTargetPlayer.transform.position, transform.position) < sightRange)
                {
                    chasing = true;
                    idling = false;

                    //if (!activated) {
                    //    activated = true;
                    //}
                }
            }
            else
            {
                // Switch target iff both players are alive
                if (players[0].GetComponent<PlayerHealth>().playerIsDead ||
                    players[1].GetComponent<PlayerHealth>().playerIsDead)
                {
                    return;
                }

                int otherPlayerIdx = 0;
                if (curTargetPlayer.gameObject.name.Contains("P1")) {
                    otherPlayerIdx = 1;
                }
                else {
                    otherPlayerIdx = 0;
                }

                float otherPlayerDist = Vector3.Distance(players[otherPlayerIdx].transform.position, transform.position);
                if (Vector3.Distance(DestinationPos, transform.position) - otherPlayerDist >= chgTarDis)
                {
                    curTargetPlayer = players[otherPlayerIdx];
                    curChgTarCD = 0f;
                }
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

        //if (Vector3.Distance(transform.position, DestinationPos) > 2 * sightRange) {
        //    chasing = false;
        //    navAgent.speed = 0;
        //    return;
        //}
        if (Vector3.Distance(transform.position, DestinationPos) >= minDistance)
        {
            //curVelocity = DestinationPos - transform.position;
            //curVelocity.Normalize();
            //curVelocity *= forwardSpeed;
            //GetComponent<Rigidbody>().velocity = curVelocity;

            // Set idle boolean
            anim.SetBool("idle", false);

            if (curNavCD >= navCD) {
                navAgent.speed = forwardSpeed;
                if (navAgent.isOnNavMesh)
                {
                    navAgent.SetDestination(DestinationPos);
                }
                curNavCD = 0f;
            }

        }
        else {
            // Attack target when in attack range
            if (curCD >= attackCD && !GetComponent<EnemyStatus>().willDie) {
                //Debug.Log("Attack!");
                //curTargetPlayer.GetComponent<PlayerHealth>().TakeDamage(damage);
                StartCoroutine(Attack());
                curCD = 0f;
            }

            navAgent.speed = 0;
            if (navAgent.isOnNavMesh)
            {
                navAgent.SetDestination(transform.position);
            }
            //GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);

            // Set idle boolean
            anim.SetBool("idle", true);
        }
    }

    private IEnumerator Attack() {
        Vector3 TargetPos;
        float time = AnimationLength("attack");
        GameObject tempTarget = curTargetPlayer.gameObject;

        // Set attack boolean
        anim.SetBool("attack", true);
        bool attacked = false;
        float j = 0f;
        while (j < time) {
            if (j > time / 4 && !attacked) {
                TargetPos = new Vector3(tempTarget.transform.position.x,
                                         transform.position.y,
                                         tempTarget.transform.position.z);

                if (!GetComponent<EnemyStatus>().willDie && !GetComponent<EnemyStatus>().frozen &&
                    tempTarget != null && !tempTarget.GetComponent<PlayerHealth>().playerIsDead &&
                    Vector3.Distance(transform.position, TargetPos) <= minDistance * 3f)
                {
                    tempTarget.GetComponent<PlayerHealth>().TakeDamage(damage);
                    attacked = true;
                }
            }

            j += Time.deltaTime;
            yield return null;
        }

        //if (!GetComponent<EnemyStatus>().willDie && !GetComponent<EnemyStatus>().frozen &&
        //    curTargetPlayer != null && !curTargetPlayer.GetComponent<PlayerHealth>().playerIsDead) {
        //    curTargetPlayer.GetComponent<PlayerHealth>().TakeDamage(damage);
        //}

        anim.SetBool("attack", false);
    }

    public float AnimationLength(string name)
    {
        float time = 0;
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;

        for (int i = 0; i < ac.animationClips.Length; i++)
            if (ac.animationClips[i].name.Contains(name))
                time = ac.animationClips[i].length;

        return time;
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

    public void SetCurChgTarCD (float t) {
        curChgTarCD = t;
    }
}
