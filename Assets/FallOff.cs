using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOff : MonoBehaviour {

    public float fallSpeed = 10.0f;
    public float fallThreshold = 10.0f;
    public float destroyDelay = 5.0f;
    private GameObject station;
    private Renderer myRend;
    private bool opened;

    private void Start()
    {
        station = GameObject.Find("HoleStation(Clone)");
        myRend = GetComponent<Renderer>();
        myRend.enabled = false;
        opened = false;
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
                    hitTarget.GetComponent<p1_movement>().speed = 0;

                    // Drop item after short delay
                }
                else if (hitTarget.name == "P2(Clone)")
                {
                    hitTarget.GetComponent<p2_movement>().speed = 0;

                    // Drop item after short delay
                }
                else if (hitTarget.tag == "monster")
                {
                    hitTarget.GetComponent<EnemyMovement>().forwardSpeed = 0;

                }
                else{
                    return;
                }

                // Fall
                Vector3 tarDest = transform.position + new Vector3(0f, -10f, 0f);
                Vector3 direction = tarDest - hitTarget.transform.position;
                direction.Normalize();
                hitTarget.transform.Translate(direction * fallSpeed * Time.deltaTime);

                // Destroy
                Destroy(hitTarget, destroyDelay);

            }
        }

    }

    private void FixedUpdate()
    {
        if (station.GetComponent<StationStatus>().activated && !opened) {
            myRend.enabled = true;
            opened = true;
        }
        else if (!station.GetComponent<StationStatus>().activated && opened) {
            myRend.enabled = false;
            opened = false;
        }
    }
}
