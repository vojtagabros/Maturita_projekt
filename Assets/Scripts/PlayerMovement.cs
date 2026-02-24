using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Jobs;
using Random = UnityEngine.Random;

public class RayVisualizer : MonoBehaviour
{
    public string targetLayerName = "Ground";
    private Vector3 targetPosition;
    private NavMeshAgent agent;
    public Transform[] spawnPoints;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        //spawn
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawn = spawnPoints[randomIndex];
        agent.Warp(chosenSpawn.position);   
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Vykresli červený Ray pro orientaci
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 0.1f);

            if (Physics.Raycast(ray, out hit))
            {
                // Získání názvu vrstvy objektu, na který Raycast narazil
                string hitLayerName = LayerMask.LayerToName(hit.collider.gameObject.layer);

                if (hitLayerName == targetLayerName)
                {
                    // Pokud je vrstva "Ground", vykresli zelenou čáru k bodu zásahu
                    Debug.DrawLine(ray.origin, hit.point, Color.green, 0.5f);
                    targetPosition = hit.point;
                    agent.SetDestination(targetPosition);
                }
            }
        }
    }
}