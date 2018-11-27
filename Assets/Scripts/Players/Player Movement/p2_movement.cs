using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p2_movement : MonoBehaviour
{

    public float speed;

    public Vector3 motionVector;
    private Rigidbody rb;
    private Transform camTransform;
    private Animator anim;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("P2 Horizontal");
        float moveVertical = Input.GetAxis("P2 Vertical");
        Vector3 movement = Vector3.zero;

        movement.x = moveHorizontal;
        movement.z = moveVertical;

        if (movement.magnitude > 1) movement.Normalize();

        motionVector = movement;

        //if (camTransform != null)
        //{
        //    motionVector = camTransform.TransformDirection(motionVector);
        //    motionVector.Set(motionVector.x, 0, motionVector.z);
        //}
        //else
        //{
        //    camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        //}

        //rb.AddForce(motionVector * speed);
        rb.transform.Translate(speed * motionVector.x * Time.deltaTime, 0f, speed * motionVector.z * Time.deltaTime);

        // Change rotation
        if (!GetComponent<P2Status>().frozen)
        {
            // Rebind animation
            if (anim.speed.Equals(0))
            {
                anim.speed = 1;
            }

            if (movement.magnitude > 0)
            {
                transform.GetChild(1).LookAt(rb.transform.position + motionVector); // since model is reversed, we will reverse directions
                anim.SetBool("running", true);

                //if (GetComponent<P2Status>().isInvincible())
                //{
                //    GetComponent<P2Status>().SetInvincibility(false);
                //}
            }
            else
            {
                anim.SetBool("running", false);
            }
        }
        else
        {
            // Stop animation
            anim.speed = 0;
        }
    }
}
