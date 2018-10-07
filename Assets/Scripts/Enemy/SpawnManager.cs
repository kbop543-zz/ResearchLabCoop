using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public int EnemiesSpawned;
    // public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject[] enemies;
    public Vector3 spawnValues;
    public float spawnWait = 3f;
    public float initHeight = 0.7f;
		public float spawnMostWait;
		public float spawnLeastWait;
		public int startWait;
		public bool stop;

		int randEnemy;

		void Start (){

			StartCoroutine(waitSpawner());

		}

		IEnumerator waitSpawner()
        {
			yield return new WaitForSeconds (startWait);

			while (!stop){
				randEnemy = Random.Range(0,2);

            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0, 500f);
            //+ transform.TransformPoint(0,0,0);
				spawnPosition.y = initHeight;
				Instantiate (enemies[randEnemy], spawnPosition,
                             Quaternion.Euler(new Vector3(0, 0, 90)));

				yield return new WaitForSeconds (spawnWait);
			}
		}
	}
