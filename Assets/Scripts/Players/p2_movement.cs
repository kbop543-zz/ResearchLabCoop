using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p2_movement : MonoBehaviour {

    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("P2 Horizontal");
        float moveVertical = Input.GetAxis("P2 Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }
}
