using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapCount : MonoBehaviour
{
    public GameObject CountDownText;
    public GameObject CountDownText_Front;

    public 

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
            CountDownText.GetComponent<TextMeshPro>().text = "Race Complete";
            CountDownText_Front.GetComponent<TextMeshPro>().text = "Race Complete";
        }
        else
        {
        CountDownText.GetComponent<TextMeshPro>().text = "Lap " + lap;
        CountDownText_Front.GetComponent<TextMeshPro>().text = "Lap " + lap;
        }

        
    }
}
