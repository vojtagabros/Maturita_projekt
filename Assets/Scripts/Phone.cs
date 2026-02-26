using UnityEngine;

public class PhonePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("phone found");

            // Find all phones
            GameObject[] phones = GameObject.FindGameObjectsWithTag("phone");

            foreach (GameObject phone in phones)
            {
                Destroy(phone);
            }
        }
    }
}