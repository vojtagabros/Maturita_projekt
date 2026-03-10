using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static float HelpCalled = 0;
    public static float ExitFound = 0;
    public static bool Died = false;
    public static bool Escaped = false;
    public static bool PlayerSeen = false;
    public static bool WeaponFound = false;
    public static float GameStartTime = 0;
    public static float SurvivalTime = 0;
    public static bool FoughtAttacker = false;
    public static bool FightWon = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
            Reset();
    }

    static void Reset()
    {
        HelpCalled = 0;
        ExitFound = 0;
        Died = false;
        Escaped = false;
        PlayerSeen = false;
        WeaponFound = false;
        SurvivalTime = 0;
        GameStartTime = Time.time;
        FoughtAttacker = false;
        FightWon = false;
    }

    void Awake()
    {
        Reset();
    }
}
