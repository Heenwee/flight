using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public Animator modelAnim;

    public float speed, acceleration;
    public float turningSpeed, rotateSpeed;

    float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        z = Input.GetAxisRaw("Rotate");

        RotateModel();
    }

    void FixedUpdate()
    {
        Move();
        Clamp();
    }
    void Move()
    {
        rb.AddForce(transform.forward * acceleration);

        rb.AddTorque(transform.up * turningSpeed * x);
        rb.AddTorque(transform.right * turningSpeed * y);
        rb.AddTorque(transform.forward * rotateSpeed * -z);
    }
    void RotateModel()
    {
        modelAnim.SetFloat("x", Input.GetAxisRaw("Horizontal"));
        modelAnim.SetFloat("y", Input.GetAxisRaw("Vertical"));
    }
    void Clamp()
    {
        Vector3 vel = rb.velocity;
        Vector3 newVel = Vector3.ClampMagnitude(vel, speed);
        rb.velocity = newVel; 
        // HELLO, IS IT ME YOU'RE LOOKING FOR?
        // no ):<
        // I'VE BEEN WONDERING IF AFTER ALL THESE YEARS YOU'D LIKE TO MEET?
        // what? that's not even from the same song!
    }
}
