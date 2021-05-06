using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    bool isTouchingRoad = false;
    bool isTouchingSand = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouchingRoad)
        {
            PlayerRacer.multiplier = 1;
        }
        else if (isTouchingSand)
        {
            PlayerRacer.multiplier = 0.5f;
        }
        else
        {
            PlayerRacer.multiplier = 0.2f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Road")
        {
            isTouchingRoad = true;
            print("On Road");
        }
        if (collision.tag == "SandTrap")
        {
            isTouchingSand = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Road")
        {
            isTouchingRoad = false;
            print("Off Road");
        }
        if (collision.tag == "SandTrap")
        {
            isTouchingSand = false;
        }
    }
}
