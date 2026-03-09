using UnityEngine;
using UnityEngine.SceneManagement; // Důležitá knihovna

public class SceneSwitcher : MonoBehaviour
{
    public void LoadGameScene()
    {
        // Načte scénu podle indexu v Build Settings (tvoje ID 1)
        SceneManager.LoadScene(1);
    }
}