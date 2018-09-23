using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform LabTransform;
    public float forwardSpeed = 20f;
    public int damage = 10;
    //Transform _playerTransform;
    //NavMeshAgent _meshAgent;

    // Use this for initialization
    void Start()
    {
        LabTransform = GameObject.FindGameObjectWithTag("Lab").transform;
        //try
        //{
        //    _meshAgent = GetComponent<NavMeshAgent>();
        //    _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //}
        //catch (System.Exception)
        //{
        // TOOD: throw custom error
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (LabTransform)
        {
            Vector3 direction = Vector3.MoveTowards(transform.position,
                                                    LabTransform.position,
                                                    Time.deltaTime * forwardSpeed);

            transform.position = direction;
        }

    }

    private void OnCollisionEnter(Collision collision)     {         if (collision.gameObject.tag == "Lab") {             GameObject spawner = GameObject.FindWithTag("GameManager");

            if (!spawner.GetComponent<gameConstants>().gameOver) {
                spawner.GetComponent<gameConstants>().curLabHealth -= damage;
                Debug.Log("curLabHealth: " + spawner.GetComponent<gameConstants>().curLabHealth.ToString());
            } 
            Destroy(this.gameObject);         }     }



}
