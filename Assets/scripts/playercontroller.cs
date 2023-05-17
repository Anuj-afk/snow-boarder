using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    [SerializeField] float pickupspeed;
    [SerializeField] float torque;
    SurfaceEffector2D surfaceEffector;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        surfaceEffector = collision.gameObject.GetComponent<SurfaceEffector2D>();
    }
    private void FixedUpdate()
    {
        playerRotation();
        playerMovement();
    }
    private void Update()
    {

    }
    private void playerRotation()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(-torque * 10 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) 
        { 
            rb.AddTorque(torque * 10 * Time.deltaTime);
        }
    }
    private void playerMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(surfaceEffector.speed < 20) 
            {
                surfaceEffector.speed += 1 * pickupspeed * Time.deltaTime ;
            }
            else {return;}
        }
        else if (Input.GetKey(KeyCode.DownArrow))
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
}
