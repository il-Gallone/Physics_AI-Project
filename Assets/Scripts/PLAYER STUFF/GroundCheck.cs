using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //Multiplier base values
    public float baseMultiplier = 1;
    public float sandMultiplier = 0.8f;
    public float offTerrainMultiplier = 0.7f;

    RacerController parent;

    // Start is called before the first frame update
    void Start()
    {
        //find the controller of the object this script is attached to
        parent = gameObject.GetComponentInParent<RacerController>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        //while on a terrain hazard, lower the multiplier
        if (collision.tag == "SandTrap" && parent.multiplier > sandMultiplier)
        {
            parent.multiplier = sandMultiplier;
        }
        if (collision.tag == "Grass" && parent.multiplier > offTerrainMultiplier)
        {
            parent.multiplier = offTerrainMultiplier;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //upon exit of a terrain hazard, reset the multiplier
        parent.multiplier = baseMultiplier;
    }
}
