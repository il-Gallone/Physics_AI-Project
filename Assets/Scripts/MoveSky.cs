using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSky : MonoBehaviour
{
    public GameObject player;
    public float multiplier;
    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //used for fancy background effects
        transform.position = new Vector3(player.transform.position.x * multiplier - offset, transform.position.y);
    }
}
