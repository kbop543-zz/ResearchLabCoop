using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p1_movement : MonoBehaviour
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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
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
            //camTransform = Camera.main.transform;
            camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }

        //rb.AddForce(motionVector * speed);
        rb.transform.Translate(speed * motionVector.x * Time.deltaTime, 0f, speed * motionVector.z * Time.deltaTime);

        // Change rotation
        if (!GetComponent<P1Status>().frozen && movement.magnitude > 0) {
            transform.GetChild(1).LookAt(rb.transform.position + motionVector); // since model is reversed, we will reverse directions
        }
    }
}
