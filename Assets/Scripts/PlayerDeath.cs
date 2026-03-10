using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameData.FoughtAttacker = true;
            GameData.SurvivalTime = Time.time - GameData.GameStartTime;

            // Šance na výhru: 1/3 s telefonem, 1/5 bez telefonu
            int chance = GameData.PhoneFound ? 3 : 5;
            bool won = Random.Range(0, chance) == 0;

            if (won)
            {
                GameData.FightWon = true;
                GameData.Escaped = true;
            }
            else
            {
                GameData.Died = true;
            }

            SceneManager.LoadScene("ResultScene");
        }
    }
}
