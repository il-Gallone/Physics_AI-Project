using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRacer : RacerController {


    public float playerCatchupModifier = 1;

    void Start() {

    }


    void Update()
    {
        if (GameManager.instance.countdown <= 0)
        {
            //Finding the path to travel
            CalculateNodeDistance();
            FindClosestToCheckpoint();



            //finding angle and positional distances
            FindDistances();

            //finding out whether to slow down or speed up to make the race more interesting
            CalculateCatchupModifier();

            //steering along the path

            //always trying to accelerate based on the racer's stats
            rigid2D.AddForce(transform.up * acceleration * multiplier * playerCatchupModifier * weight * Time.deltaTime);

            //try to turn away if we're gonna head offroad
            RaycastHit2D left = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, 30) * transform.up, 1, mask);
            RaycastHit2D right = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, -30) * transform.up, 1, mask);
            if (left)
            {
                rigid2D.AddTorque(-handling * weight * playerCatchupModifier * Time.deltaTime);
            }
            if (right)
            {
                rigid2D.AddTorque(handling * weight * playerCatchupModifier * Time.deltaTime);
            }


            //if the AI is at max speed slow down
            if (rigid2D.velocity.magnitude > maxSpeed * multiplier * playerCatchupModifier)
            {
                rigid2D.AddForce(-rigid2D.velocity.normalized * acceleration * multiplier * playerCatchupModifier * weight * Time.deltaTime);
            }
            //turn as much as necessary to make a corner
            if (Mathf.Abs(distAngle * Mathf.Rad2Deg) > Mathf.Abs(rigid2D.angularVelocity / handling))
                rigid2D.AddTorque(Mathf.Sign(distAngle) * Mathf.Min(handling, Mathf.Abs(distAngle * Mathf.Rad2Deg)) * weight * Time.deltaTime);
            else
                rigid2D.AddTorque(Mathf.Sign(-distAngle) * Mathf.Min(handling, rigid2D.angularVelocity) * weight * Time.deltaTime);
            //if turning too fast slow down
            if (rigid2D.angularVelocity > maxTorque * playerCatchupModifier)
            {
                rigid2D.AddTorque(-handling * weight * Time.deltaTime);
            }
            if (rigid2D.angularVelocity < -maxTorque * playerCatchupModifier)
            {
                rigid2D.AddTorque(handling * weight * Time.deltaTime);
            }

            //move the direction of velocity with turnspeed
            rigid2D.velocity = Quaternion.Euler(0, 0, rigid2D.angularVelocity * Time.deltaTime) * rigid2D.velocity / (Mathf.Abs(distAngle * distAngle / 500.0f) + (Mathf.Abs(rigid2D.angularVelocity * rigid2D.angularVelocity / 5000000.0f)) + 1);

            

            //track position along the path
            CheckForNearbyCheckpoint();
        }

    }

    void CalculateCatchupModifier()
    {
        //if the player is ahead speed up, or behind slow down
        PlayerRacer player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRacer>();
        int checkpointDifference = (checkpointNum + currentLap*checkpoints.Length) - (player.checkpointNum + player.currentLap*checkpoints.Length);
        if (checkpointDifference > 0)
        {
            playerCatchupModifier = Mathf.Pow(0.92f, checkpointDifference);
        }
        else if (checkpointDifference < 0)
        {
            playerCatchupModifier = Mathf.Pow(1.05f, -checkpointDifference);
        }
        else
        {
            playerCatchupModifier = 1;
        }
    }
}
