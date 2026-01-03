using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public float throwForce = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ThrowObjects();
        }
    }

    void ThrowObjects()
    {
        foreach (var grabbed in PlayerDrag.GrabbedObjects)
        {
            Rigidbody rb = grabbed.GetComponent<Rigidbody>();
            if (rb == null)
                continue;

            grabbed.DisconnectJoint();

            Vector3 direction = rb.position - transform.position;
            direction.Normalize();

            rb.AddForce(direction * throwForce, ForceMode.Impulse);
        }

        PlayerDrag.GrabbedObjects.Clear();
    }
}