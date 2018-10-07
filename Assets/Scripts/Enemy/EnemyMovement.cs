using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float minDistance = 5f;
    Transform LabTransform;
    public float forwardSpeed = 20f;
    public int damage = 10;
    public Image currentHealthbar;
  	public Text ratioText;
    GameObject healthBarImage;
    GameObject labRatioText;
    GameObject gm;
    private Vector3 DestinationPos;
    //Transform _playerTransform;
    //NavMeshAgent _meshAgent;

    // Use this for initialization
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        LabTransform = GameObject.FindGameObjectWithTag("Lab").transform;
        healthBarImage = GameObject.FindWithTag("HealthBarImage");
        labRatioText = GameObject.FindWithTag("LabRatio");
        gm = GameObject.FindWithTag("GameManager");
        //try
        //{
        //    _meshAgent = GetComponent<NavMeshAgent>();
        //    _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //}
        //catch (System.Exception)
        //{
        // TOOD: throw custom error
        //}

        //Fixing scorpman floating problem
        DestinationPos = new Vector3(LabTransform.position.x, transform.position.y, LabTransform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (LabTransform)
        //{
        //    Vector3 direction = Vector3.MoveTowards(transform.position,
        //                                            DestinationPos,
        //                                            Time.deltaTime * forwardSpeed);

        //    transform.position = direction;
        //}

        transform.LookAt(playerTransform);

        if (Vector3.Distance(transform.position, playerTransform.position) >= minDistance)
        {

            transform.position += transform.forward * forwardSpeed * Time.deltaTime;

        }
    }

    private void LateUpdate()
    {
        if(gm.GetComponent<GameConstants>().completeLvl) 
        {
          if(labRatioText){
            ratioText = labRatioText.GetComponent<Text>();
            ratioText.text = "Orbs collected.You win!";
            ratioText.color = Color.blue;
            }
            GameObject[] playerObjectArray = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in playerObjectArray) {
                    player.SetActive (false);
            }
        }
        if (gm.GetComponent<GameConstants>().gameOver)
        {
            if (labRatioText)
            {
                ratioText = labRatioText.GetComponent<Text>();
                ratioText.text = "Game Over";
                ratioText.color = Color.red;
            }
            GameObject[] playerObjectArray = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in playerObjectArray)
            {
                player.SetActive(false);
            }
        }
    }

    void UpdateHealthBar(){
      //GameObject gm = GameObject.FindWithTag("GameManager");
  		var currLabHealth = gm.GetComponent<GameConstants>().curLabHealth;
  		var maxLabHealth = gm.GetComponent<GameConstants>().maxLabHealth;

  		float ratio = (float) currLabHealth / 100;
      print("ratio" + ratio);
      print("currlabhealth" + currLabHealth);

      if(healthBarImage){
        currentHealthbar = healthBarImage.GetComponent<Image>();
  		  currentHealthbar.rectTransform.localScale = new Vector3(ratio,1,1);
        }
      if(labRatioText){
        ratioText = labRatioText.GetComponent<Text>();
  		  ratioText.text = (ratio*100).ToString() + '%';
        }
    }

    private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.tag == "Lab")
            {

                //GameObject gm = GameObject.FindWithTag("GameManager");

                if (!gm.GetComponent<GameConstants>().gameOver && !gm.GetComponent<GameConstants>().completeLvl)
                {
                    gm.GetComponent<GameConstants>().curLabHealth -= damage;


                    Debug.Log("curLabHealth: " + gm.GetComponent<GameConstants>().curLabHealth.ToString());
                    UpdateHealthBar();
                }

                // Moving the following code to LateUpdate; currently it's
                // triggered by OnCollisionEnter builtin function which causes the delay

                //if(gm.GetComponent<GameConstants>().completeLvl){
                //  if(labRatioText){
                //    ratioText = labRatioText.GetComponent<Text>();
                //	  ratioText.text = "Orbs collected.You win!";
                //    ratioText.color = Color.blue;
                //    }
                //    GameObject[] playerObjectArray = GameObject.FindGameObjectsWithTag("Player");
                //    foreach (GameObject player in playerObjectArray) {
                //            player.SetActive (false);
                //    }
                //}
                //if (gm.GetComponent<GameConstants>().gameOver) {
                //  if(labRatioText){
                //    ratioText = labRatioText.GetComponent<Text>();
                //	  ratioText.text = "Game Over";
                //    ratioText.color = Color.red;
                //    }
                //    GameObject[] playerObjectArray = GameObject.FindGameObjectsWithTag("Player");
                //    foreach (GameObject player in playerObjectArray) {
                //            player.SetActive (false);
                //    }
                //}


                Destroy(this.gameObject);

            }
        }
}
