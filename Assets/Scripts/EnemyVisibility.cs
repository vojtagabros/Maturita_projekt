using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisibility : MonoBehaviour
{
    private Renderer rend;
    private AIMovement ai;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        ai = GetComponent<AIMovement>();

        rend.enabled = false;
    }

    public void SetVisible(bool value)
    {
        rend.enabled = value;

        if (value)
        {
            ai.OnPlayerSeen(); // Start chasing forever
        }
    }
}
