using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    public GameObject startButton;

    public GameObject text;
    public GameObject textBack;


    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;

    private void Start()
    {
    }

    public void StartGame(string sceneName)
    {
        GameManager.instance.countdown = 5;
        SceneManager.LoadScene(sceneName);
    }

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
}
