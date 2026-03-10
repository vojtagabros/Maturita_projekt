using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RayVisualizer : MonoBehaviour
{
    public string targetLayerName = "Ground";
    public Transform[] spawnPoints;

    [Header("Movement")]
    public float moveSpeed = 4f;
    public float acceleration = 20f;
    public float angularSpeed = 720f;
    public float stoppingDistance = 0.2f;

    private NavMeshAgent agent;

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
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                string hitLayerName = LayerMask.LayerToName(hit.collider.gameObject.layer);
                if (hitLayerName == targetLayerName)
                {
                    agent.SetDestination(hit.point);
                }
            }
        }

        // Zastav agenta jakmile dosáhne cíle
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.velocity = Vector3.zero;
        }
    }
}
