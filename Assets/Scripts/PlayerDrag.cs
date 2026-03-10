using UnityEngine;
using System.Collections.Generic;

public class PlayerDrag : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    [SerializeField] private float maxDistance = 2.5f;
    [SerializeField] private Vector3 connectedAnchor = new Vector3(0.32f, -0.16f, -2.62f);

    public static HashSet<PlayerDrag> GrabbedObjects = new HashSet<PlayerDrag>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SetJoint();

        if (Input.GetKeyUp(KeyCode.Space))
            DisconnectJoint();
    }

    void SetJoint()
    {
        float distance = Vector3.Distance(playerRigidbody.position, transform.position);
        if (distance > maxDistance)
            return;

        SpringJoint joint = gameObject.GetComponent<SpringJoint>();
        if (joint == null)
            joint = gameObject.AddComponent<SpringJoint>();

        joint.connectedBody = playerRigidbody;
        joint.autoConfigureConnectedAnchor = true;
        joint.anchor = Vector3.zero;
        joint.connectedAnchor = connectedAnchor;
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

        GrabbedObjects.Add(this);
    }

    public void DisconnectJoint()
    {
        SpringJoint joint = gameObject.GetComponent<SpringJoint>();
        if (joint != null)
        {
            Destroy(joint);
            GrabbedObjects.Remove(this);
        }
    }
}
