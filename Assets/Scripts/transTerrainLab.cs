using UnityEngine;

public class transTerrainLab : MonoBehaviour {

    //public GameObject player
    public Vector3 translocation;
    public bool doorOpen;
    GameObject gm;

    private void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        doorOpen = false;
        gm = GameObject.FindWithTag("GameManager");
    }

    private void LateUpdate()
    {
        if (!doorOpen)
        {
            if (gm.GetComponent<GameConstants>().curOrbs >= gm.GetComponent<GameConstants>().maxOrbs)
            {
                doorOpen = true;
                gameObject.GetComponent<Renderer>().enabled = true;
            }
        }

    }

    void OnCollisionEnter(Collision col) 
    {
        if (doorOpen) {
            translocate(col.gameObject);
        }

    }

    public void translocate(GameObject col)
    {
        if (col.tag == "Player")
        {
            col.transform.position += translocation;
        }
    }


}
