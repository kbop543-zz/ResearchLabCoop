using System.Collections;
using UnityEngine;

public class BigShotHit : MonoBehaviour {

    public float thrust = 150f;
    public float extendRate = 550f;
    private GameObject shooter;
    private Rigidbody rb;
    //private bool isColliding;
    public AudioSource shatterSound;

    private void Start()
    {
        StartCoroutine(increaseXscale());
    }

    public void updateHolder(GameObject holder) {
        shooter = holder;
    }

    private void OnTriggerEnter(Collider other)     {
        GameObject hitTarget = other.transform.root.gameObject;
        float force = thrust;
        if (hitTarget.tag == "Monster" || hitTarget.tag == "Player") {
            // Check if it's the shooter himself
            if (shooter.name.Equals(hitTarget.name)) {
                return;
            }

            // Else
            if (hitTarget.name == "P1(Clone)") {
                if (!hitTarget.GetComponent<P1Status>().blown)
                {
                    hitTarget.GetComponent<P1Status>().BlowAway(0.5f);

                    // If Frozen
                    if (hitTarget.GetComponent<P1Status>().frozen)
                    {
                        shatterSound.Play();
                        hitTarget.GetComponent<P1Status>().Shattered();
                        return;
                    }
                }
                else {
                    return;
                }
            }
            else if (hitTarget.name == "P2(Clone)") {
                if (!hitTarget.GetComponent<P2Status>().blown)
                {
                    hitTarget.GetComponent<P2Status>().BlowAway(0.5f);

                    // If Frozen
                    if (hitTarget.GetComponent<P2Status>().frozen)
                    {
                        shatterSound.Play();
                        hitTarget.GetComponent<P2Status>().Shattered();
                        return;
                    }
                }
                else {
                    return;
                }
            }
            else {
                if (!hitTarget.GetComponent<EnemyStatus>().blown)
                {
                    hitTarget.GetComponent<EnemyStatus>().BlowAway(0.5f);

                    // If Frozen
                    if (hitTarget.GetComponent<EnemyStatus>().frozen) {
                        shatterSound.Play();
                        hitTarget.GetComponent<EnemyStatus>().ShatterMonster();
                        GameManager.instance.GetComponent<GameConstants>().enemyKillCount += 1;
                        GameManager.instance.GetComponent<GameConstants>().comboShatter += 1;
                        GameManager.instance.GetComponent<LevelManager>().myStats.GetComponent<StatsCounter>().incFreezeShockKill();
                        return;
                    }
                    // If normal state
                    else {
                        hitTarget.GetComponent<EnemyMovement>().changeCurTarget(shooter);
                    }
                    hitTarget.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.normalized * force, ForceMode.Impulse);

                }
                else {
                    return;
                }
                //force *= 2f;
            }

         
          } 
    }

    IEnumerator increaseXscale()
    {
        while (true)
        {
            transform.localScale = transform.localScale +
                                   new Vector3(extendRate * Time.deltaTime,
                                               0,
                                               0);
            yield return null;
        }
    }

    IEnumerator increaseXZscales(){
        while(true){
            transform.localScale = transform.localScale +
                                   new Vector3(extendRate * Time.deltaTime,
                                               0,
                                               extendRate * Time.deltaTime);
            yield return null;
        }
    }
}
