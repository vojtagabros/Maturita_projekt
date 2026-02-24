using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public GameObject destinationOfPlayer;
    public Transform[] spawnPoints;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        //spawn
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawn = spawnPoints[randomIndex];
        agent.Warp(chosenSpawn.position);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(destinationOfPlayer.transform.position);
    }
}
