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

    [SerializeField] float pickupspeed;
    [SerializeField] float torque;

    SurfaceEffector2D surfaceEffector;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }
    private void FixedUpdate()
    {
        playerRotation();
        playerMovement();
    }
    private void Update()
    {;
        ProcessInput();   
    }
    private void playerRotation()
    {
        if (rightrotate)
        {
            rb.AddTorque(-torque * 10 * Time.deltaTime);
        }
        else if (leftrotate) 
        { 
            rb.AddTorque(torque * 10 * Time.deltaTime);
        }
    }
    private void playerMovement()
    {
        if (forward)
        {
            if(surfaceEffector.speed < 20) 
            {
                surfaceEffector.speed += 1 * pickupspeed * Time.deltaTime ;
            }
            else {return;}
        }
        else if (backwards)
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
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        surfaceEffector = collision.gameObject.GetComponent<SurfaceEffector2D>();
    }
    private void ProcessInput()
    {
        leftrotate = Input.GetKey(KeyCode.LeftArrow);
        rightrotate = Input.GetKey(KeyCode.RightArrow);
        forward = Input.GetKey(KeyCode.UpArrow);
        backwards = Input.GetKey(KeyCode.DownArrow);    
    }
}
