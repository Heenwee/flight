using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueToRot : MonoBehaviour
{
    Rigidbody rb;
    public Transform desiredRot;

    public float dampenFactor = 0.8f;
    public float adjustFactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate(rb.transform.forward, desiredRot.forward);
        Rotate(rb.transform.up, desiredRot.up);
        Rotate(rb.transform.right, desiredRot.right);
    }
    void Rotate(Vector3 dir, Vector3 desiredDir)
    {
        Quaternion deltaQuad = Quaternion.FromToRotation(dir, desiredDir);

        Vector3 axis;
        float angle;

        deltaQuad.ToAngleAxis(out angle, out axis);
        rb.AddTorque(-rb.angularVelocity * dampenFactor, ForceMode.Acceleration);
        rb.AddTorque(axis.normalized * angle * adjustFactor, ForceMode.Acceleration);
    }
}
