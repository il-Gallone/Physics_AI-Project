using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public string IDSpawn1;
    public GameObject spawn1;
    public string IDSpawn2;
    public GameObject spawn2;
    public string IDSpawn3;
    public GameObject spawn3;
    public string IDSpawn4;
    public GameObject spawn4;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player;
        if(IDSpawn1 == GameManager.instance.playerRacer)
        {
            player = Instantiate(spawn1, transform.position, transform.rotation);
        }
        else if (IDSpawn2 == GameManager.instance.playerRacer)
        {
            player = Instantiate(spawn2, transform.position, transform.rotation);
        }
        else if (IDSpawn3 == GameManager.instance.playerRacer)
        {
            player = Instantiate(spawn3, transform.position, transform.rotation);
        }
        else if (IDSpawn4 == GameManager.instance.playerRacer)
        {
            player = Instantiate(spawn4, transform.position, transform.rotation);
        }
        else
        {
            GameManager.instance.playerRacer = IDSpawn1;
            player = Instantiate(spawn1, transform.position, transform.rotation);
        }
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SetPos>().player = player.transform;
        GameObject.FindGameObjectWithTag("Backdrop").GetComponent<MoveSky>().player = player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
