using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public PlayerDrag playerDrag;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerDrag.DisconnectJoint();
            //Throw();
        }
    }
    
    
    //void Throw()
    //{
        
    //}
}
