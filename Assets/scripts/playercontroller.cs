using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    bool leftrotate;
    bool rightrotate;
    bool forward;
    bool backwards;
    bool boost;

    public bool istriggered = false;

    [SerializeField] float pickupspeed;
    [SerializeField] float torque;

    SurfaceEffector2D surfaceEffector;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        surfaceEffector = FindObjectOfType<SurfaceEffector2D>();    
    }
    private void FixedUpdate()
    {
        playerRotation();
        playerMovement();
    }
    private void Update()
    {
        ProcessInput();   
    }
    private void playerRotation()
    {
        if (rightrotate && !istriggered)
        {
            rb.AddTorque(-torque * Time.deltaTime);
        }
        else if (leftrotate && !istriggered) 
        { 
            rb.AddTorque(torque * Time.deltaTime);
        }
    }
    private void playerMovement()
    {
        if (forward && !istriggered)
        {
            if(surfaceEffector.speed < 20) 
            {
                surfaceEffector.speed += 1 * pickupspeed * Time.deltaTime ;
            }
            else {return;}
        }
        else if (backwards && !istriggered)
        {
            if (surfaceEffector.speed > 0)
            {
                surfaceEffector.speed -= 1 * pickupspeed * Time.deltaTime;
            }
            else { return; }
        }
        else
        {
            if (surfaceEffector.speed >1)
            {
                surfaceEffector.speed -= 1 * pickupspeed * Time.deltaTime;
            }
            else { return; }
        }
        if (boost && !istriggered)
        {
            if (surfaceEffector.speed < 20)
            {
                float speed = surfaceEffector.speed;
                float remainingspeed = 20 - speed;
                if (remainingspeed <= 3)
                {
                    surfaceEffector.speed += remainingspeed;
                }
                else if (remainingspeed >= 3)
                {
                    surfaceEffector.speed += 3;
                }
            }
        }
    }
    private void ProcessInput()
    {
        leftrotate = Input.GetKey(KeyCode.LeftArrow);
        rightrotate = Input.GetKey(KeyCode.RightArrow);
        forward = Input.GetKey(KeyCode.UpArrow);
        backwards = Input.GetKey(KeyCode.DownArrow);
        boost = Input.GetKey(KeyCode.Space);
    }
    public void move()
    {
        istriggered = true;
    }
}
