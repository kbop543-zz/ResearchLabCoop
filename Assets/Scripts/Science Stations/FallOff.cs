using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOff : MonoBehaviour {

    public float fallSpeed = 10.0f;
    public float fallThreshold = 26.0f;
    public float destroyDelay = 5.0f;
    public float rotateRate = 270f;
    public float expandDuration = 0.75f;
    private GameObject station;
    private Renderer myRend;
    private bool opened;
    private float scaleX;
    private float scaleY;
    private float scaleZ;
    public AudioSource dropsound;

    private void Start()
    {
        station = GameObject.Find("HoleStation(Clone)");
        myRend = GetComponent<Renderer>();
        myRend.enabled = false;
        opened = false;

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }

    private void OnTriggerStay(Collider other)
    {
        if (station.GetComponent<StationStatus>().activated)
        {
            GameObject hitTarget = other.transform.root.gameObject;

            if (hitTarget.transform.localScale.x <= fallThreshold)
            {
                // Freeze movement by setting parameters
                if (hitTarget.name == "P1(Clone)")
                {
                    hitTarget.GetComponent<P1Status>().Fall();

                    // Drop item after short delay
                }
                else if (hitTarget.name == "P2(Clone)")
                {
                    hitTarget.GetComponent<P2Status>().Fall();

                    // Drop item after short delay
                }
                else if (hitTarget.tag == "Monster" && hitTarget.GetComponent<EnemyStatus>().willDie == false)
                {
                    // Enemy Fall
                    hitTarget.GetComponent<EnemyStatus>().Fall();
                    Destroy(hitTarget, destroyDelay);
                    GameManager.instance.GetComponent<GameConstants>().enemyKillCount += 1;

                    // Start falling animation
                    StartCoroutine(StartFalling(hitTarget));
                }
                else{
                    return;
                }

                // Fall
                //dropsound.Play();
                //Vector3 tarDest = transform.position + new Vector3(0f, -100f, 0f);
                //Vector3 direction = tarDest - hitTarget.transform.position;
                //direction.Normalize();
                //hitTarget.transform.Translate(direction * fallSpeed * Time.deltaTime);

                // Destroy
                //Destroy(hitTarget, destroyDelay);
                // update the enemy death count
                //GameManager.instance.GetComponent<GameConstants>().enemyKillCount += 1;

            }
        }

    }

    private IEnumerator StartFalling (GameObject target) {
        //dropsound.Play();
        Vector3 tarDest = transform.position + new Vector3(0f, -75f, 0f);
        Vector3 direction = tarDest - target.transform.position;
        direction.Normalize();

        while (true) {
            target.transform.Translate(direction * fallSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        IEnumerator holeAnimation = holeOpening();
        if (station.GetComponent<StationStatus>().activated && !opened) {
            myRend.enabled = true;
            opened = true;
            StartCoroutine(holeAnimation);
        }
        else if (!station.GetComponent<StationStatus>().activated && opened) {
            myRend.enabled = false;
            opened = false;
            StopCoroutine(holeAnimation);
        }
    }

    IEnumerator holeOpening()
    {
        transform.localScale = new Vector3(0f, 0f, 0f);
        float curRotation = 0f;

        // Reach original size
        float i = 0f;
        while (i < expandDuration) {
            transform.localScale = transform.localScale + new Vector3(scaleX * Time.deltaTime / expandDuration,
                                                                      scaleY * Time.deltaTime / expandDuration,
                                                                      scaleZ * Time.deltaTime / expandDuration);
            curRotation += rotateRate * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0, curRotation, 0));
            i += Time.deltaTime;
            yield return null;
        }
        // Keep rotating
        while (true) {
            curRotation += rotateRate * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0, curRotation, 0));
            yield return null;
        }
    }
}
