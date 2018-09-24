using UnityEngine;

public class transTerrainLab : MonoBehaviour {

    //public GameObject player;
    public Vector3 translocation;
	
	void OnCollisionEnter(Collision col) 
    {
        translocate(col.gameObject);
    }

    public void translocate(GameObject col)
    {
        if (col.tag == "Player")
        {
            col.transform.position += translocation;
        }
    }
}
