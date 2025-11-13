using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform destination; // where player and camera should move
    public Camera mainCamera;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Move player to destination
            other.transform.position = destination.position;

            // Move camera if it's fixed per room
            if (mainCamera != null)
                mainCamera.transform.position = destination.position;
        }
    }
}