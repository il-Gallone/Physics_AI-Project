using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPos : MonoBehaviour
{
    public Transform player;
    public bool transX = true;
    public bool transY = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //This allows objects to follow the player, either only in 1 axis or both.
        float newX, newY;
        
        if (transX == true)
            newX = player.position.x;
        else
            newX = transform.position.x;

        if (transY == true)
            newY = player.position.y;
        else
            newY = transform.position.y;

        // Only change values if the values are true
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
