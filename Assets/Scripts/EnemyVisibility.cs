using UnityEngine;

public class EnemyVisibility : MonoBehaviour
{
    private Renderer rend;
    private AIMovement ai;
    private bool _alreadySeen = false;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        ai = GetComponent<AIMovement>();
        rend.enabled = false;
        GameData.PlayerSeen = false;
    }

    public void SetVisible(bool value)
    {
        rend.enabled = value;

        if (value && !_alreadySeen)
        {
            _alreadySeen = true;
            GameData.PlayerSeen = true;
            ai.OnPlayerSeen();
        }
    }
}
