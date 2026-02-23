using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisibility : MonoBehaviour
{
    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false; // Start hidden
    }

    public void SetVisible(bool value)
    {
        rend.enabled = value;
    }
}
