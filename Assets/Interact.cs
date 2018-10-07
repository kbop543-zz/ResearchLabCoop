using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{

    public float interactRadius = 16f;

    void FixedUpdate()
    {
        if ((Input.GetKey("x") && (gameObject.name == "P1" || gameObject.name == "P1(Clone)")) ||
            (Input.GetKey("o") && (gameObject.name == "P2" || gameObject.name == "P2(Clone)")))
        {
            Activate();

        }
    }

    private void Activate()
    {
        Collider[] items = Physics.OverlapSphere(transform.position, interactRadius);
        bool found = false;
        int size = items.Length;

        if (size != 0)
        {
            int i = 0;

            while ((i < size) && !found)
            {
                //If we have station
                if (items[i].gameObject.tag == "Station")
                {
                    if (items[i].GetComponent<StationStatus>().activated == false)
                    {
                        items[i].GetComponent<StationStatus>().activated = true;
                        found = true;

                        //Debug.Log("Interacted!!!");
                    }

                }
                //Elsif we have ...
                //else if (items[i].gameObject.tag == "Tool")
                //{
                //
                //}

                i++;
            }

        }

        //Else
        return;
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Tile")
    //    {
    //        Physics.IgnoreCollision(GetComponent<Collider>(), collision.transform.GetComponent<Collider>());
    //    }
    //}
}
