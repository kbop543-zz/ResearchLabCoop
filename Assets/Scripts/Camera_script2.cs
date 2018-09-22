using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_script2 : MonoBehaviour
{
    private const float y_angle_min = 0.0f;
    private const float y_angle_max = 50.0f;
    public GameObject Player;
    public Transform lookAt;
    public Transform camTransform;
    private Camera cam;
    private float distance = 100.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        camTransform = transform;
        cam = Camera.main;
        //offset = transform.position - Player.transform.position;
    }
    private void Update()
    {
        currentX += Input.GetAxis("P2 Horizontall camera");
        currentY += Input.GetAxis("P2 Vertical camera");
        currentY = Mathf.Clamp(currentY, y_angle_min, y_angle_max);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * direction;
        camTransform.LookAt(lookAt.position);
        //transform.position = Player.transform.position + offset;
    }
}
