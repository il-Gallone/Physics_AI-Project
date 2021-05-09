using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapCount : MonoBehaviour
{
    public GameObject LapText;
    public GameObject LapText_Front;

    public GameObject EndScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int lap = GameManager.instance.playerLap;

        if (lap > 3)
        {
            LapText.GetComponent<TextMeshPro>().text = "Race Complete";
            LapText_Front.GetComponent<TextMeshPro>().text = "Race Complete";
            EndScreen.SetActive(true);
        }
        else
        {
        LapText.GetComponent<TextMeshPro>().text = "Lap " + lap;
        LapText_Front.GetComponent<TextMeshPro>().text = "Lap " + lap;
        }

        
    }
}
