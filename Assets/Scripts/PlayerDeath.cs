using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private bool _fightResolved = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (_fightResolved) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            _fightResolved = true;

            GameData.FoughtAttacker = true;
            GameData.SurvivalTime = Time.time - GameData.GameStartTime;

            // Šance na výhru: 1/3 se zbraní, 1/5 bez zbraně
            int chance = GameData.WeaponFound ? 3 : 5;
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
