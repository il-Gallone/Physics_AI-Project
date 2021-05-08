using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRacer : RacerController {


    public float playerCatchupModifier;

    void Start() {

    }


    void Update()
    {
        CalculateNodeDistance();
        FindClosestToCheckpoint();

        Debug.DrawLine(closestTarget, transform.position);



        //steering
        FindDistances();

        CalculateCatchupModifier();

        rigid2D.AddForce(transform.up * acceleration * multiplier * playerCatchupModifier * weight * Time.deltaTime);

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


        //rigid2D.AddForce(transform.up * acceleration * 1 * Time.deltaTime);
        if (rigid2D.velocity.magnitude > maxSpeed * multiplier * playerCatchupModifier)
        {
            rigid2D.AddForce(-rigid2D.velocity.normalized * acceleration * multiplier * playerCatchupModifier * weight * Time.deltaTime);
        }

        if (Mathf.Abs(distAngle * Mathf.Rad2Deg) > Mathf.Abs(rigid2D.angularVelocity / handling))
            rigid2D.AddTorque(Mathf.Sign(distAngle) * Mathf.Min(handling, Mathf.Abs(distAngle * Mathf.Rad2Deg)) * weight * Time.deltaTime);
        else
            rigid2D.AddTorque(Mathf.Sign(-distAngle) * Mathf.Min(handling, rigid2D.angularVelocity) * weight * Time.deltaTime);
        if (rigid2D.angularVelocity > maxTorque * playerCatchupModifier)
        {
            rigid2D.AddTorque(-handling * weight * Time.deltaTime);
        }
        if (rigid2D.angularVelocity < -maxTorque * playerCatchupModifier)
        {
            rigid2D.AddTorque(handling * weight * Time.deltaTime);
        }

        rigid2D.velocity = Quaternion.Euler(0, 0, rigid2D.angularVelocity * Time.deltaTime) * rigid2D.velocity / (Mathf.Abs(distAngle * distAngle / 500.0f) + (Mathf.Abs(rigid2D.angularVelocity * rigid2D.angularVelocity / 5000000.0f)) + 1);

        //if (mag == 0) {
        //    rigid2D.velocity = (transform.up * -5);
        //}
        //float dot = Mathf.Abs(Vector3.Dot(Vector3.Normalize(transform.up), Vector3.Normalize(rigid2D.velocity)));
        //float mag = rigid2D.velocity.magnitude * dot;
        //rigid2D.AddForce(rigid2D.velocity * -1 * dot);
        //rigid2D.AddForce(transform.up * mag * 1 * dot );

        //this.transform.position = this.transform.position + new Vector3(speed * Time.deltaTime * Mathf.Cos(angle), speed * Time.deltaTime * Mathf.Sin(angle), 0);

        CheckForNearbyCheckpoint();

    }

    void CalculateCatchupModifier()
    {
        PlayerRacer player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRacer>();
        int checkpointDifference = (checkpointNum + currentLap*checkpoints.Length) - (player.checkpointNum + player.currentLap*checkpoints.Length);
        if (checkpointDifference > 0)
        {
            playerCatchupModifier = Mathf.Pow(0.98f, checkpointDifference);
        }
        else if (checkpointDifference < 0)
        {
            playerCatchupModifier = Mathf.Pow(1.005f, -checkpointDifference);
        }
        else
        {
            playerCatchupModifier = 1;
        }
    }
}
