using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerLap;
    public string playerRacer;
    public float countdown = 5;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown > 0)
        {
            countdown -= Time.deltaTime/2;
        }
        try
        {
            playerLap = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRacer>().currentLap + 1;
        }
        catch { }
    }
}
