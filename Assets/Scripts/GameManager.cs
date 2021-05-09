using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerLap;
    public int playerPosition;
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
            PlayerRacer player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRacer>();
            playerLap = player.currentLap + 1;
            playerPosition = 1;
            GameObject[] AI = GameObject.FindGameObjectsWithTag("AI");
            for(int i = 0; i < AI.Length; i++)
            {
                AIRacer AIStats = AI[i].GetComponent<AIRacer>();
                if((player.checkpointNum + player.currentLap * player.checkpoints.Length) < (AIStats.checkpointNum + AIStats.currentLap * AIStats.checkpoints.Length))
                {
                    playerPosition++;
                }
                else if((player.checkpointNum + player.currentLap * player.checkpoints.Length) == (AIStats.checkpointNum + AIStats.currentLap * AIStats.checkpoints.Length))
                {
                    if(Vector3.Distance(player.checkpointPos, player.transform.position) > Vector3.Distance(AIStats.checkpointPos, AIStats.transform.position))
                    {
                        playerPosition++;
                    }
                }
            }
        }
        catch { }
    }
}
