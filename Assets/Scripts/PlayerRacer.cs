using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRacer : RacerController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        rigid2D.AddForce(transform.up * acceleration * Input.GetAxis("Vertical") * Time.deltaTime);
        if (rigid2D.velocity.magnitude > maxSpeed)
        {
            rigid2D.AddForce(-rigid2D.velocity.normalized * acceleration * Time.deltaTime);
        }
        rigid2D.AddTorque(handling * -Input.GetAxis("Horizontal") * Time.deltaTime);
        if (rigid2D.angularVelocity > maxTorque)
        {
            rigid2D.AddTorque(-handling * Time.deltaTime);
        }
        if (rigid2D.angularVelocity < -maxTorque)
        {
            rigid2D.AddTorque(handling * Time.deltaTime);
        }
    }
}