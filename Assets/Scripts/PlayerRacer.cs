using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerRacer : RacerController
{
    public static float multiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {

        rigid2D.AddForce(transform.up * acceleration * Input.GetAxis("Vertical") * multiplier * Time.deltaTime);
        if (rigid2D.velocity.magnitude > maxSpeed * multiplier)
        {
            rigid2D.AddForce(-rigid2D.velocity.normalized * acceleration * multiplier * Time.deltaTime);
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
