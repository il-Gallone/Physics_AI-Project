using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public float baseMultiplier = 1;
    public float sandMultiplier = 0.5f;
    public float offTerrainMultiplier = 0.25f;

    RacerController parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.GetComponentInParent<RacerController>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
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
        parent.multiplier = baseMultiplier;
    }
}
