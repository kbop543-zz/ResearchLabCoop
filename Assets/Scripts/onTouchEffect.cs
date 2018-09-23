using UnityEngine;

public class onTouchEffect : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject gm = GameObject.FindWithTag("GameManager");

            gm.GetComponent<gameConstants>().curOrbs += 1;
            Debug.Log("curOrbs: " + gm.GetComponent<gameConstants>().curOrbs.ToString());


            Destroy(this.gameObject);
        }
    }
}
