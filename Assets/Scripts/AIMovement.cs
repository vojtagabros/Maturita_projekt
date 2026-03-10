using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public GameObject player;
    public Transform[] spawnPoints;
    public Transform[] destinations;
    public float timeToWait = 5f;

    [Header("Movement")]
    public float moveSpeed = 5.5f;
    public float acceleration = 20f;
    public float angularSpeed = 720f;
    public float stoppingDistance = 0.2f;

    private NavMeshAgent agent;
    private bool hasDetectedPlayer = false;
    private Coroutine _wanderCoroutine;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = moveSpeed;
        agent.acceleration = acceleration;
        agent.angularSpeed = angularSpeed;
        agent.stoppingDistance = stoppingDistance;
        agent.autoBraking = true;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        agent.Warp(spawnPoints[randomIndex].position);

        _wanderCoroutine = StartCoroutine(WanderRoutine());
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

    public void OnPlayerSeen()
    {
        if (hasDetectedPlayer) return;
        hasDetectedPlayer = true;
        if (_wanderCoroutine != null)
            StopCoroutine(_wanderCoroutine);
    }
}
