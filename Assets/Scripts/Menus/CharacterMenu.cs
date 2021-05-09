using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    public GameObject startButton;

    public Button characterButton1;
    public Button characterButton2;
    public Button characterButton3;
    public Button characterButton4;
    public Button lastSelectedButton;

    public GameObject text;
    public GameObject textBack;


    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;

    private void Start()
    {
    }

    public void StartGame(string sceneName)
    {
        //When start is clicked set the countdown, and load the track
        GameManager.instance.countdown = 3.5f;
        SceneManager.LoadScene(sceneName);
    }

    //these 4 functions allow for character selection and enable the start button
    public void selectPlayer1()
    {
        GameManager.instance.playerRacer = "RedWing";
        startButton.GetComponent<Button>().interactable = true;
        text.GetComponent<TextMeshProUGUI>().text = "Red Wing";
        textBack.GetComponent<TextMeshProUGUI>().text = "Red Wing";
        text.GetComponent<TextMeshProUGUI>().color = Color.red;
        textBack.GetComponent<TextMeshProUGUI>().color = Color.black;
    }
    public void selectPlayer2()
    {
        GameManager.instance.playerRacer = "GreenSpear";
        startButton.GetComponent<Button>().interactable = true;
        text.GetComponent<TextMeshProUGUI>().text = "Green Spear";
        textBack.GetComponent<TextMeshProUGUI>().text = "Green Spear";
        text.GetComponent<TextMeshProUGUI>().color = Color.green;
        textBack.GetComponent<TextMeshProUGUI>().color = Color.black;
    }
    public void selectPlayer3()
    {
        GameManager.instance.playerRacer = "BluePointer";
        startButton.GetComponent<Button>().interactable = true;
        text.GetComponent<TextMeshProUGUI>().text = "Blue Pointer";
        textBack.GetComponent<TextMeshProUGUI>().text = "Blue Pointer";
        text.GetComponent<TextMeshProUGUI>().color = Color.blue;
        textBack.GetComponent<TextMeshProUGUI>().color = Color.black;
    }
    public void selectPlayer4()
    {
        GameManager.instance.playerRacer = "BlackBruiser";
        startButton.GetComponent<Button>().interactable = true;
        text.GetComponent<TextMeshProUGUI>().text = "Black Bruiser";
        textBack.GetComponent<TextMeshProUGUI>().text = "Black Bruiser";
        text.GetComponent<TextMeshProUGUI>().color = Color.black;
        textBack.GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    private void Update()
    {
        //due to some finicky menu controls, this allows a controller to select the start menu button
        if(startButton.GetComponent<Button>().interactable)
        {
            if(Input.GetAxis("MenuVertical") < 0)
            {
                startButton.GetComponent<Button>().Select();
            }
            if (Input.GetAxis("MenuVertical") > 0)
            {
                lastSelectedButton.Select();
            }
        }
        //this is how we determine where to return if we don't want to start the game
        if(EventSystem.current.currentSelectedGameObject == characterButton1.gameObject)
        {
            lastSelectedButton = characterButton1;
        }
        if (EventSystem.current.currentSelectedGameObject == characterButton2.gameObject)
        {
            lastSelectedButton = characterButton2;
        }
        if (EventSystem.current.currentSelectedGameObject == characterButton3.gameObject)
        {
            lastSelectedButton = characterButton3;
        }
        if (EventSystem.current.currentSelectedGameObject == characterButton4.gameObject)
        {
            lastSelectedButton = characterButton4;
        }
    }

}
