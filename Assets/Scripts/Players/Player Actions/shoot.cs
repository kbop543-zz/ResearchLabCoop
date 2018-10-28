using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject shockwave;
    public GameObject oil;
    public AudioSource LaserSound;
    // Following projectile properties will be updated according to weapon
    private GameObject projectile;
    private float range;// = 1000f;
    private float startDistance;// = 25f;
    private float duration;// = 1.5f;
    private float cooldown;// = 10f;
    private string[] guns = new string[] { "shock", "oil" };
    private int curGun = 0;

    // Current gun cooldown
    private float curCooldown;

    private void Start()
    {
        projectile = shockwave;
        range = 350f;
        startDistance = 30f;
        duration = 0.45f;
        //cooldown = 0.5f;
        cooldown = 2.0f;
        curCooldown = cooldown;
    }

    void FixedUpdate()
    {
        if ((Input.GetKeyUp("v") || Input.GetKey(KeyCode.Joystick1Button4)) && ((gameObject.name == "P1") || gameObject.name == "P1(Clone)"))
        {
            //Debug.Log("switch bullet");
            Switch();
        }
        if ((Input.GetKeyUp("n") || Input.GetKey(KeyCode.Joystick2Button4)) && ((gameObject.name == "P2") || (gameObject.name == "P2(Clone)")))
        {
            //Debug.Log("switch bullet2");
            Switch();
        }

        if ((Input.GetKeyUp("b") || Input.GetKey(KeyCode.Joystick1Button7)) && ((gameObject.name == "P1") || gameObject.name == "P1(Clone)"))
        {
            Shoot();
        }
        if ((Input.GetKeyUp("m") || Input.GetKey(KeyCode.Joystick2Button7)) && ((gameObject.name == "P2") || (gameObject.name == "P2(Clone)")))
        {
            Shoot();
        }

        if (curCooldown < cooldown)
        {
            curCooldown += Time.deltaTime;
        }

    }

    public void Switch()
    {
        if (curGun == (guns.Length - 1))
        {
            curGun = 0;
        }
        else
        {
            curGun += 1;
        }
        GunStatsUpdate(guns[curGun]);

    }

    public void Shoot()
    {
        if (gameObject.name == "P1(Clone)" && (GetComponent<P1Status>().frozen || GetComponent<P1Status>().blown))
        {
            return;
        }
        else if (gameObject.name == "P2(Clone)" && (GetComponent<P2Status>().frozen || GetComponent<P2Status>().blown))
        {
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
            if (guns[curGun] == "shock")
            {
                b.GetComponent<BigShotHit>().updateHolder(gameObject);

            }
            else if (guns[curGun] == "oil")
            {
                b.GetComponent<OilHit>().updateHolder(gameObject);
            }
            Destroy(b, duration);

            curCooldown = 0f;
        }

    }

    public void GunStatsUpdate(string gunName)
    {
        switch (gunName)
        {
            case "shock":
                projectile = shockwave;
                range = 350f;
                startDistance = 30f;
                duration = 0.45f;
                //cooldown = 0.5f;
                //cooldown = 2.0f;
                //curCooldown = cooldown;
                break;
            case "oil":
                projectile = oil;
                range = 350f;
                startDistance = 30f;
                duration = 0.45f;
                //cooldown = 2.0f;
                //curCooldown = cooldown;
                break;
            default:
                Debug.Log("Unregistered weapon");
                projectile = shockwave;
                range = 350f;
                startDistance = 30f;
                duration = 0.45f;
                //cooldown = 2.0f;
                //curCooldown = cooldown;
                break;
        }
    }

}