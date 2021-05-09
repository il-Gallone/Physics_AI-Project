using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PositionTracker : MonoBehaviour
{
    public GameObject PositionText;
    public GameObject PositionText_Front;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Do various text changes based on position
        int position = GameManager.instance.playerPosition;

        if (position == 1)
        {
            PositionText.GetComponent<TextMeshPro>().text = "1st";
            PositionText_Front.GetComponent<TextMeshPro>().text = "1st";
            PositionText_Front.GetComponent<TextMeshPro>().color = Color.yellow;
        }
        else if (position == 2)
        {
            PositionText.GetComponent<TextMeshPro>().text = "2nd";
            PositionText_Front.GetComponent<TextMeshPro>().text = "2nd";
            PositionText_Front.GetComponent<TextMeshPro>().color = new Color(0.7f, 0.7f, 0.7f);
        }
        else if (position == 3)
        {
            PositionText.GetComponent<TextMeshPro>().text = "3rd";
            PositionText_Front.GetComponent<TextMeshPro>().text = "3rd";
            PositionText_Front.GetComponent<TextMeshPro>().color = new Color(0.6f, 0.45f, 0.25f);
        }
        else
        {
            PositionText.GetComponent<TextMeshPro>().text = position.ToString() + "th";
            PositionText_Front.GetComponent<TextMeshPro>().text = position.ToString() + "th";
            PositionText_Front.GetComponent<TextMeshPro>().color = Color.white;
        }
    }
}
