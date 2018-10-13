using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public int EnemiesSpawned;
    // public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject[] enemies;
    public Vector3 spawnDeviation = new Vector3 (100f, 0f, 0f);
    public Vector3 spawnPoint = new Vector3 (170f, 0f, 560f);
    public int spawnLimit = 13;
    public float spawnWait = 4f;
	public float startWait = 5f;
	public bool stop = false;
    public float zBoundary = 505f;
    private int randEnemy;
    private Vector3 spawnPosition;

    private void Start () {
        EnemiesSpawned = 0;
        StartCoroutine(waitSpawner());

	}

	IEnumerator waitSpawner()
    {
	    yield return new WaitForSeconds (startWait);

		while(!stop && (EnemiesSpawned < spawnLimit)) {
		    randEnemy = Random.Range(0, enemies.Length);
            spawnPosition = new Vector3 (spawnPoint.x + Random.Range(-spawnDeviation.x, spawnDeviation.x),
                                         enemies[randEnemy].transform.localScale.y / 2,
                                         spawnPoint.z + Random.Range(-spawnDeviation.z, spawnDeviation.z));

			GameObject monster = Instantiate (enemies[randEnemy], spawnPosition,
                                              Quaternion.Euler(new Vector3(0, 0, 90)));
            // Send monster into experimental ground
            StartCoroutine(sendMinion(monster));
            EnemiesSpawned += 1;

            Debug.Log("spawned: " + EnemiesSpawned.ToString());
            yield return new WaitForSeconds (spawnWait);
		}
	}

    IEnumerator sendMinion(GameObject monster)
    {
        float speed = monster.GetComponent<EnemyMovement>().forwardSpeed;
        monster.transform.LookAt(monster.transform.position + new Vector3(0f, 0f, -7f));

        // While out of boundary -> keep moving
        while(monster.transform.position.z > (zBoundary - monster.transform.localScale.z)) {
            monster.transform.position = new Vector3(monster.transform.position.x,
                                                     monster.transform.position.y,
                                                     monster.transform.position.z - speed * Time.deltaTime);
            yield return null;
        }

        // activate monster
        monster.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        monster.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        monster.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        monster.GetComponent<EnemyMovement>().activated = true;
    }
}
