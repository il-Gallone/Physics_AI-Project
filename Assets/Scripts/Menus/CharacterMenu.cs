using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{

    public GameObject startButton;
    public int playerSelector;

    public GameObject text;
    public GameObject textBack;

    public void StartGame()
    {

    }

    public void selectPlayer1()
    {
        playerSelector = 1;
        startButton.GetComponent<Button>().interactable = true;
        text.GetComponent<TextMeshProUGUI>().text = "Red Wing";
        textBack.GetComponent<TextMeshProUGUI>().text = "Red Wing";
        text.GetComponent<TextMeshProUGUI>().color = Color.red;
        textBack.GetComponent<TextMeshProUGUI>().color = Color.black;
    }
    public void selectPlayer2()
    {
        playerSelector = 2;
        startButton.GetComponent<Button>().interactable = true;
        text.GetComponent<TextMeshProUGUI>().text = "Green Spear";
        textBack.GetComponent<TextMeshProUGUI>().text = "Green Spear";
        text.GetComponent<TextMeshProUGUI>().color = Color.green;
        textBack.GetComponent<TextMeshProUGUI>().color = Color.black;
    }
    public void selectPlayer3()
    {
        playerSelector = 3;
        startButton.GetComponent<Button>().interactable = true;
        text.GetComponent<TextMeshProUGUI>().text = "Blue Pointer";
        textBack.GetComponent<TextMeshProUGUI>().text = "Blue Pointer";
        text.GetComponent<TextMeshProUGUI>().color = Color.blue;
        textBack.GetComponent<TextMeshProUGUI>().color = Color.black;
    }
    public void selectPlayer4()
    {
        playerSelector = 4;
        startButton.GetComponent<Button>().interactable = true;
        text.GetComponent<TextMeshProUGUI>().text = "Black Bruiser";
        textBack.GetComponent<TextMeshProUGUI>().text = "Black Bruiser";
        text.GetComponent<TextMeshProUGUI>().color = Color.black;
        textBack.GetComponent<TextMeshProUGUI>().color = Color.white;
    }
}
