using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p2_movement : MonoBehaviour
{

    public float speed;

    public Vector3 motionVector;
    private Rigidbody rb;
    private Transform camTransform;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
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

        if (camTransform != null)
        {
            motionVector = camTransform.TransformDirection(motionVector);
            motionVector.Set(motionVector.x, 0, motionVector.z);
        }
        else
        {
            camTransform = GameObject.FindGameObjectWithTag("p2_cam").transform;
        }

        //rb.AddForce(motionVector * speed);
        rb.transform.Translate(speed * motionVector.x * Time.deltaTime, 0f, speed * motionVector.z * Time.deltaTime);
    }
}
