using UnityEngine;
using UnityEngine.SceneManagement; // Důležitá knihovna

public class SceneSwitcher : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}