using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform _playerTransform;
    NavMeshAgent _meshAgent;

    // Use this for initialization
    void Start()
    {
        try
        {
            _meshAgent = GetComponent<NavMeshAgent>();
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch (System.Exception)
        {
            // TOOD: throw custom error
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerTransform)
        {

            var playerPosition = _playerTransform.position;
						playerPosition.y = 0;
            _meshAgent.SetDestination(playerPosition);
        }
    }
}
