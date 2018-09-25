using UnityEngine;

public class onTouchEffect : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" &&
            collision.gameObject.GetComponent<PickOrDrop>().hasCollector == true)
        {
            GameObject gm = GameObject.FindWithTag("GameManager");

            gm.GetComponent<GameConstants>().curOrbs += 1;
            Debug.Log("curOrbs: " + gm.GetComponent<GameConstants>().curOrbs.ToString());


            Destroy(this.gameObject);
        }
    }
}
