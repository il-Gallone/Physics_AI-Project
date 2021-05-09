using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerRacer : RacerController
{

    public bool facingWrong = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        //Only Run if the race has started
        if (GameManager.instance.countdown <= 0)
        {
            //Accelerate and Reverse based on racer's stats
            rigid2D.AddForce(transform.up * acceleration * Input.GetAxis("Vertical") * weight * multiplier * Time.deltaTime);
            //If velocity if above max speed then slow down
            if (rigid2D.velocity.magnitude > maxSpeed * multiplier)
            {
                rigid2D.AddForce(-rigid2D.velocity.normalized * acceleration * weight * multiplier * Time.deltaTime);
            }
            //Turn based on racer's stats
            rigid2D.AddTorque(handling * -Input.GetAxis("Horizontal") * weight * Time.deltaTime);
            //Change direction of movement based on turn speed
            rigid2D.velocity = Quaternion.Euler(0, 0, rigid2D.angularVelocity * Time.deltaTime) * rigid2D.velocity / (Mathf.Abs(0.15f * 0.15f * Input.GetAxis("Horizontal") / 500.0f) + (Mathf.Abs(rigid2D.angularVelocity * rigid2D.angularVelocity / 5000000.0f)) + 1);
            //If turnspeed above max rotation then slow down
            if (rigid2D.angularVelocity > maxTorque)
            {
                rigid2D.AddTorque(-handling * weight * Time.deltaTime);
            }
            if (rigid2D.angularVelocity < -maxTorque)
            {
                rigid2D.AddTorque(handling * weight * Time.deltaTime);
            }

            //used for placement tracking
            CalculateNodeDistance();
            FindClosestToCheckpoint();
            FindDistances();

            //goes unused but it's there for future
            if (Mathf.Abs(distAngle) > Mathf.PI / 2)
            {
                facingWrong = true;
            }
            else
            {
                facingWrong = false;
            }

            //Also used for placement tracking
            CheckForNearbyCheckpoint();
        }
    }
}
