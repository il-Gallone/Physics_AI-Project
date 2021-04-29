using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerController : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public float maxTorque;
    public float handling;
    public float weight;

    public Rigidbody2D rigid2D;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D.mass = weight;
        rigid2D.drag = 1 / weight;
        rigid2D.angularDrag = 2 * weight;
    }

    // Update is called once per frame
    void Update()
    {
        rigid2D.AddForce(transform.up * acceleration * Input.GetAxis("Vertical")*Time.deltaTime);
        if(rigid2D.velocity.magnitude > maxSpeed)
        {
            rigid2D.AddForce(-rigid2D.velocity.normalized * acceleration * Time.deltaTime);
        }
        rigid2D.AddTorque(handling * -Input.GetAxis("Horizontal")*Time.deltaTime);
        if(rigid2D.angularVelocity > maxTorque)
        {
            rigid2D.AddTorque(-handling * Time.deltaTime);
        }
        if (rigid2D.angularVelocity < -maxTorque)
        {
            rigid2D.AddTorque(handling * Time.deltaTime);
        }
    }
}
