using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RayVisualizer : MonoBehaviour
{
    public string targetLayerName = "Ground";
    private NavMeshAgent agent;
    public Transform[] spawnPoints;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

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
    }
}
