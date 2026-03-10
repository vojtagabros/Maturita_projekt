using UnityEngine;

public class PhonePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameData.WeaponFound = true;
            Destroy(gameObject);
        }
    }
}
