using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameData.Died = true;
            GameData.SurvivalTime = Time.time - GameData.GameStartTime;
            SceneManager.LoadScene("ResultScene");
        }
    }
}
