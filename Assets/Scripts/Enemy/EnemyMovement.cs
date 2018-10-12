using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float minDistance = 5f;
    Transform LabTransform;
    public float forwardSpeed = 20f;
    GameObject gm;
    private Vector3 DestinationPos;

    // Use this for initialization
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        LabTransform = GameObject.FindGameObjectWithTag("Lab").transform;
        gm = GameObject.FindWithTag("GameManager");


        //Fixing scorpman floating problem
        DestinationPos = new Vector3(LabTransform.position.x, transform.position.y, LabTransform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //}

        transform.LookAt(playerTransform);

        if (Vector3.Distance(transform.position, playerTransform.position) >= minDistance)
        {

            transform.position += transform.forward * forwardSpeed * Time.deltaTime;

        }
    }





}
