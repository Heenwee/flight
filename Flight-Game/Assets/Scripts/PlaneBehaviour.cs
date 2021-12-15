using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public Animator modelAnim;

    float currentSpeed;
    public float speed, dashSpeed, acceleration;
    public float afterDashDecelerate;
    public float turningSpeed, rotateSpeed;

    public Camera cam;
    public float fovIncrease;

    float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        z = Input.GetAxisRaw("Rotate");

        RotateModel();
        Dash();

        if (rb.velocity.magnitude >= speed) cam.fieldOfView = FOV(rb.velocity.magnitude);
        else cam.fieldOfView = 90;
    }

    float FOV(float x)
    {
        //return fovIncrease * x + 90 - fovIncrease * speed;
        //return Mathf.Pow(1.19f, x) + 60;
        //return -131.65f + 73.99f * Mathf.Log(x);
        return 125.7143f / (1 + 27.5573f * Mathf.Pow((float)System.Math.E, -0.212f * x));
    }

    void Dash()
    {
        if (Input.GetButton("Dash"))
        {
            currentSpeed = dashSpeed;
        }
        else if (currentSpeed > speed)
        {
            currentSpeed -= Time.deltaTime * afterDashDecelerate;
        }
        else currentSpeed = speed;
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
        Vector3 newVel = Vector3.ClampMagnitude(vel, currentSpeed);
        rb.velocity = newVel; 
        // HELLO, IS IT ME YOU'RE LOOKING FOR?
        // no ):<
        // I'VE BEEN WONDERING IF AFTER ALL THESE YEARS YOU'D LIKE TO MEET?
        // what? that's not even from the same song!
    }
}
