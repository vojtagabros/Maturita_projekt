using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameData.Escaped = true;
            GameData.SurvivalTime = Time.time - GameData.GameStartTime;
            SceneManager.LoadScene("ResultScene");
        }
    }
}
