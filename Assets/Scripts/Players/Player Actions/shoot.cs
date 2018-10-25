using UnityEngine;

public class shoot : MonoBehaviour {
    public GameObject shockwave;
    public AudioSource LaserSound;
    // Following projectile properties will be updated according to weapon     private GameObject projectile;     private float range = 1000f;     private float startDistance = 25f;     private float duration = 1.5f;
    private float cooldown = 10f;

    // Current gun cooldown
    private float curCooldown;

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

            if (curCooldown < cooldown) {
                curCooldown += Time.deltaTime;
            }
        }     }      public void Shoot() {
        if (gameObject.name == "P1(Clone)" && (GetComponent<P1Status>().frozen || GetComponent<P1Status>().blown)) {
            return;
        }
        else if (gameObject.name == "P2(Clone)" && (GetComponent<P2Status>().frozen || GetComponent<P2Status>().blown)) {
            return;
        }

        if (curCooldown >= cooldown)
        {
            Vector3 pos = transform.position;
            Vector3 direction = transform.GetChild(1).forward;
            direction.y = 0f;
            direction.Normalize();

            //Debug.Log("Preparing Bullet");

            var b = (GameObject)Instantiate(projectile,
                                            pos + direction * startDistance,
                                            transform.rotation);
            LaserSound.Play();
            b.GetComponent<Rigidbody>().velocity = direction * range;
            b.GetComponent<BigShotHit>().updateHolder(gameObject);

            Destroy(b, duration);

            curCooldown = 0f;
        }
     }

    public void GunStatsUpdate(string gunName) {
        switch (gunName)
        {
            case "ShockwaveGun(Clone)":
                projectile = shockwave;
                range = 350f;
                startDistance = 30f;
                duration = 0.45f;
                //cooldown = 0.5f;
                cooldown = 2.0f;
                curCooldown = cooldown;
                break;
            default:
                Debug.Log("Unregistered weapon");
                projectile = shockwave;
                range = 1000f;
                startDistance = 25f;
                duration = 1.5f;
                cooldown = 10f;
                curCooldown = cooldown;
                break;
        }
    }  }