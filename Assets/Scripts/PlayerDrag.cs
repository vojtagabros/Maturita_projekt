using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDrag : MonoBehaviour
{
    public Rigidbody playerRigidbody; // Drag your Player (Rigidbody) here


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetJoint();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            DisconnectJoint();
        }
    }



    void SetJoint()
    {
        float maxDistance = 1.2f; // player reach distance
        float distance = Vector3.Distance(playerRigidbody.position, transform.position);

        if (distance > maxDistance)
        {
            Debug.Log("Object too far to grab");
            return; // don't attach
        }
        // Add SpringJoint if not already present
        SpringJoint joint = gameObject.GetComponent<SpringJoint>();
        if (joint == null)
            joint = gameObject.AddComponent<SpringJoint>();

        // Assign connected body (Player)
        joint.connectedBody = playerRigidbody;

        // Set values from your screenshot
        joint.autoConfigureConnectedAnchor = true;
        joint.anchor = Vector3.zero;
        joint.connectedAnchor = new Vector3(0.319999f, -0.16f, -2.62f);
        joint.spring = 10f;
        joint.damper = 1f;
        joint.minDistance = 0.01f;
        joint.maxDistance = 0.02f;
        joint.tolerance = 0.2f;
        joint.breakForce = 50f;
        joint.breakTorque = 50f;
        joint.enableCollision = true;
        joint.enablePreprocessing = true;
        joint.massScale = 50f;
        joint.connectedMassScale = 1f;
    }
    
    void DisconnectJoint()
    {
        SpringJoint joint = gameObject.GetComponent<SpringJoint>();
        if (joint != null)
        {
            Destroy(joint);
            Debug.Log("SpringJoint disconnected");
        }
    }
}