using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public GameObject player;
    public Transform[] spawnPoints;
    public Transform[] destinations;
    public float timeToWait = 5f;

    private NavMeshAgent agent;
    private bool hasDetectedPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Spawn
        int randomIndex = Random.Range(0, spawnPoints.Length);
        agent.Warp(spawnPoints[randomIndex].position);

        StartCoroutine(WanderRoutine());
    }

    void Update()
    {
        if (hasDetectedPlayer)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    IEnumerator WanderRoutine()
    {
        while (!hasDetectedPlayer)
        {
            agent.SetDestination(destinations[Random.Range(0, destinations.Length)].position);
            yield return new WaitForSeconds(timeToWait);
        }
    }

    // Call this when player becomes visible
    public void OnPlayerSeen()
    {
        hasDetectedPlayer = true;
        StopCoroutine(WanderRoutine());
    }
    
}
