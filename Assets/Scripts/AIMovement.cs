using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public GameObject destinationOfPlayer;
    public Transform[] spawnPoints;
    public Transform[] destinations;
    public float timeToWait = 5f;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // spawn
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawn = spawnPoints[randomIndex];
        agent.Warp(chosenSpawn.position);

        StartCoroutine(UpdateDestinationRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(destinationOfPlayer.transform.position);
        //agent.SetDestination(destinations[Random.Range(0, destinations.Length)].position);
        //WaitUntil waitUntil = new WaitUntil(() => !agent.pathPending);
        //yield return new WaitForSeconds(2);
    }
    
    IEnumerator UpdateDestinationRoutine()
    {
        while (true)
        {
            agent.SetDestination(destinations[Random.Range(0, destinations.Length)].transform.position);
            yield return new WaitForSeconds(timeToWait);
        }
    }
    
}
