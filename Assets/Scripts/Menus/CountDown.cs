using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public GameObject CountDownText;
    public GameObject CountDownText_Front;

    public GameObject LapCounter;


    int tick = 0;

    private void Update()
    {        
        //used to signal the start of the race when the program starts
        if (GameManager.instance.countdown <= 0)
        {
            CountDownText.GetComponent<TextMeshPro>().text = "GO!";
            CountDownText_Front.GetComponent<TextMeshPro>().text = "GO!";

            CountDownText_Front.GetComponent<TextMeshPro>().color = Color.white;
            CountDownText.GetComponent<TextMeshPro>().color = new Color(0, 0.5f, 0);

            tick++;

            if (tick > 20)
            {

                CountDownText.GetComponent<TextMeshPro>().color = Color.Lerp(CountDownText.GetComponent<TextMeshPro>().color, new Color(0, 0, 0, 0), (tick - 25) / 30.0f);
                CountDownText_Front.GetComponent<TextMeshPro>().color = Color.Lerp(CountDownText_Front.GetComponent<TextMeshPro>().color, new Color(0, 0, 0, 0), (tick - 25) / 30.0f);
            }
            if (tick > 100)
            {
                LapCounter.SetActive(true);
            }

        }
        else if (GameManager.instance.countdown < 2.5)
        {
            int countDown = (int)(GameManager.instance.countdown * 2) + 1;
            CountDownText.GetComponent<TextMeshPro>().text = countDown.ToString();
            CountDownText_Front.GetComponent<TextMeshPro>().text = countDown.ToString();
        }
        
    }
}
