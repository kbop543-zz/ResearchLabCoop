using System.Collections;
using UnityEngine;

public class BigShotHit : MonoBehaviour {

    public float thrust;
    public float duration;
    public float extendRate;
    private Rigidbody rb;

	private void OnTriggerEnter(Collider collision)     {
        if (collision.gameObject.tag == "Monster") {
            Vector3 pos = collision.transform.position;
            rb = collision.GetComponent<Rigidbody>();
            rb.AddForce(-transform.forward * thrust);
            StartCoroutine(increaseXscale());

            
            
         }

        //Self destruct
        Destroy(this.gameObject);   
    }

    IEnumerator increaseXscale(){
        int i = 0;
        while(i < duration){
            yield return new WaitForSeconds(duration);
             i += (int) Time.deltaTime;
            transform.localScale += new Vector3(extendRate*Time.deltaTime, 0 , 0);
        }  // increase i by extendRate every second
    }
}
