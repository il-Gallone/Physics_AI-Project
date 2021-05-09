using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public string primarySpawnID;
    public GameObject primarySpawn;
    public GameObject secondarySpawn;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.playerRacer != primarySpawnID)
        {
            Instantiate(primarySpawn, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(secondarySpawn, transform.position, transform.rotation);
        }
    }
}
