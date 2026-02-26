using System.Collections.Generic;
using UnityEngine;

public class PhoneSpawner : MonoBehaviour
{
    public GameObject phonePrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        SpawnPhones(3);
    }

    void SpawnPhones(int amount)
    {
        // Convert array to list (so we can modify it)
        List<Transform> availableSpawns = new List<Transform>(spawnPoints);

        for (int i = 0; i < amount; i++)
        {
            if (availableSpawns.Count == 0)
                break;

            int randomIndex = Random.Range(0, availableSpawns.Count);

            Transform chosenSpawn = availableSpawns[randomIndex];

            Instantiate(phonePrefab, chosenSpawn.position, chosenSpawn.rotation);

            // Remove used spawn so it can't be picked again
            availableSpawns.RemoveAt(randomIndex);
        }
    }
}