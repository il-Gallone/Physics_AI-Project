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

    public float multiplier = 1;

    public Rigidbody2D rigid2D;

    // Start is called before the first frame update
    void Awake()
    {
        rigid2D.mass = weight;
        rigid2D.drag = 1 / weight;
        rigid2D.angularDrag = 2 * weight;
    }
}

