using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public int EnemiesSpawned;
    // public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject[] enemies;
    public Vector3 spawnValues;
    public float spawnWait;
		public float spawnMostWait;
		public float spawnLeastWait;
		public int startWait;
		public bool stop;

		int randEnemy;

		void Start (){

			StartCoroutine(waitSpawner());

		}

		void Update(){

			spawnWait = Random.Range(spawnLeastWait,spawnMostWait);

		}

		IEnumerator waitSpawner(){

			yield return new WaitForSeconds (startWait);

			while (!stop){
				randEnemy = Random.Range(0,2);

				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x,spawnValues.x), 0, Random.Range(-spawnValues.z,spawnValues.z)) + transform.TransformPoint(0,0,0);
				spawnPosition.y = 0;
				Instantiate (enemies[randEnemy], spawnPosition,
				gameObject.transform.rotation);

				yield return new WaitForSeconds (spawnWait);
			}
		}
	}
