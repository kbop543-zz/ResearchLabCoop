using UnityEngine;

public class BigShotShoot : MonoBehaviour {
    public Transform camTrans;     public GameObject bullet;     public float range = 1000f;     public float startDistance = 25f;     public float duration = 1.5f;     public float shootRate = 100f;
    private float curCooldown = 0f;
    private float cooldown = 10f;

    void FixedUpdate()     {
        if (gameObject.GetComponent<PickOrDrop>().hasGun == true) {
            if ((Input.GetKey("b") || Input.GetKey(KeyCode.Joystick1Button7)) && ((gameObject.name == "P1") || gameObject.name == "P1(Clone)"))
            {
                Shoot();
            }
            if ((Input.GetKey("m") || Input.GetKey(KeyCode.Joystick2Button7)) && ((gameObject.name == "P2") || (gameObject.name == "P2(Clone)")))
            {
                Shoot();
            }
        }      }      public void Shoot() {
        if (curCooldown < 0) {
            Debug.Log("");
            Vector3 pos = transform.position;
            //camTrans = transform.forward;
            Vector3 direction = (pos + transform.forward);
            direction.y = 0f;
            direction.Normalize();

            var b = (GameObject)Instantiate(bullet,
                                            pos + direction * startDistance,
                                            Quaternion.identity);
            b.GetComponent<Rigidbody>().velocity = direction * range;

            Destroy(b, duration);

            curCooldown = cooldown;
            Debug.Log("");
        }
        else {
            curCooldown -= shootRate * Time.deltaTime;
        }
     }  }