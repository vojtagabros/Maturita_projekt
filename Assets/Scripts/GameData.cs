using UnityEngine;

public class GameData : MonoBehaviour
{
    public static float HelpCalled = 0;
    public static float ExitFound = 0;
    public static bool Died = false;
    public static bool Escaped = false;
    public static bool PlayerSeen = false;
    public static bool PhoneFound = false;
    public static float GameStartTime = 0;
    public static float SurvivalTime = 0;

    void Awake()
    {
        HelpCalled = 0;
        ExitFound = 0;
        Died = false;
        Escaped = false;
        PlayerSeen = false;
        PhoneFound = false;
        SurvivalTime = 0;
        GameStartTime = Time.time;
    }
}
