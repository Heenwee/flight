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
    public bool mouseControl;
    public float turningSpeed, rotateSpeed;

    float midX, midY;

    public Camera cam;
    public float fovIncrease;

    float x, y, z;

    public Transform[] bulletPos;
    int posNr;
    public float fireRate;
    float rateTime;
    public float bulletSpeed;
    public int dmg;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = speed;
        posNr = 0;

        midX = Screen.width / 2;
        midY = Screen.height / 2;
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

        Attack();
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

        if (!mouseControl) RotateWASD();
        else RotateMouse();
    }
    void RotateWASD()
    {
        rb.AddTorque(transform.up * turningSpeed * x);
        rb.AddTorque(transform.right * turningSpeed * y);
        rb.AddTorque(transform.forward * rotateSpeed * -z);
    }
    void RotateMouse()
    {
        Debug.Log(Input.mousePosition.x);
        Debug.Log(Input.mousePosition.y);

        float xInput = (Input.mousePosition.x - midX) / (Screen.width / 2);
        float yInput = (Input.mousePosition.y - midY) / (Screen.height / 2);

        rb.AddTorque(transform.up * turningSpeed * xInput);
        rb.AddTorque(transform.right * turningSpeed * -yInput);
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

    void Attack()
    {
        if(Input.GetButton("Fire1"))
        {
            if (rateTime <= 0) Shoot();
        }
        rateTime -= Time.deltaTime;
    }
    void Shoot()
    {
        rateTime = fireRate;

        var b = Instantiate(bullet, bulletPos[posNr].position, bulletPos[posNr].rotation);
        Rigidbody bRb = b.GetComponent<Rigidbody>();
        bRb.velocity = b.transform.forward * bulletSpeed;

        if (posNr < bulletPos.Length-1) posNr++;
        else posNr = 0;

        //Collider col = b.GetComponent<Collider>();
        //col.enabled = false;
        //yield return new WaitForSeconds(0.5f);
        //col.enabled = true;
    }
}
