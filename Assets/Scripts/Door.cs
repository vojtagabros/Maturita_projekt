using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    public Transform TPto; //point thru wich plyaer will tp BACK to level
    public Transform TPcamera; //camera will move to this point
    public Camera mainCamera;
    public GameObject Player;
    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Move camera if it's fixed per room
            if (mainCamera != null)
                mainCamera.transform.position = TPcamera.position;
            
            // Move player to destination
            NavMeshAgent agent = Player.GetComponent<NavMeshAgent>();
            agent.Warp(TPto.position);


        }
    }
}