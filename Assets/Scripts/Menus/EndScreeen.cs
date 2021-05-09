using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreeen : MonoBehaviour
{
    //The text game objects
    public GameObject Heading;
    public GameObject HeadingFront;
    public GameObject Placement;
    public GameObject PlacementFront;

    //Time before returning to the main screen
    public float waitTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Find the player's final position at the end of the last lap
        int placement = GameManager.instance.playerPosition;

        //Various colour and word choices based on postion
        if (placement == 1)
        {
            Heading.GetComponent<TextMeshProUGUI>().text = "Victory!!!";
            HeadingFront.GetComponent<TextMeshProUGUI>().text = "Victory!!!";


            Heading.GetComponent<TextMeshProUGUI>().color = Color.yellow;
            HeadingFront.GetComponent<TextMeshProUGUI>().color = new Color(1, 0.66f, 0);

            Placement.GetComponent<TextMeshProUGUI>().text = "1st Place";
            PlacementFront.GetComponent<TextMeshProUGUI>().text = "1st Place";


            Placement.GetComponent<TextMeshProUGUI>().color = Color.white;
            PlacementFront.GetComponent<TextMeshProUGUI>().color = Color.yellow;
        }
        else
        {
            Heading.GetComponent<TextMeshProUGUI>().text = "Defeat...";
            HeadingFront.GetComponent<TextMeshProUGUI>().text = "Defeat...";


            Heading.GetComponent<TextMeshProUGUI>().color = Color.black;
            HeadingFront.GetComponent<TextMeshProUGUI>().color = Color.red;


            if (placement == 2)
            {
                Placement.GetComponent<TextMeshProUGUI>().text = "2nd Place";
                PlacementFront.GetComponent<TextMeshProUGUI>().text = "2nd Place";


                Placement.GetComponent<TextMeshProUGUI>().color = Color.black;
                PlacementFront.GetComponent<TextMeshProUGUI>().color = Color.gray;
            }
            if(placement == 3)
            {
                Placement.GetComponent<TextMeshProUGUI>().text = "3rd Place";
                PlacementFront.GetComponent<TextMeshProUGUI>().text = "3rd Place";


                Placement.GetComponent<TextMeshProUGUI>().color = Color.black;
                PlacementFront.GetComponent<TextMeshProUGUI>().color = new Color(0.6f, 0.45f, 0.25f);
            }
            if(placement > 3)
            {
                Placement.GetComponent<TextMeshProUGUI>().text = placement.ToString()+"th Place";
                PlacementFront.GetComponent<TextMeshProUGUI>().text = placement.ToString() + "th Place";


                Placement.GetComponent<TextMeshProUGUI>().color = Color.black;
                PlacementFront.GetComponent<TextMeshProUGUI>().color = Color.white;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        waitTime += Time.deltaTime;
        if (waitTime >= 4)
        {
            SceneManager.LoadScene("StartingMenu");
        }
    }
}
