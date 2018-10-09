using UnityEngine;

public class bulletHit : MonoBehaviour {

    public GameObject orb;

	private void OnCollisionEnter(Collision collision)     {
        if (collision.gameObject.tag == "Monster") {
            Vector3 pos = collision.transform.position;
             //Deals damage
            Destroy(collision.gameObject);

            //Spawn Orb
            Instantiate(orb, pos,
                        Quaternion.identity);
         }

        //Self destruct
        Destroy(this.gameObject);     }
}
