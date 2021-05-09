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
        SceneManager.LoadScene(sceneName);
    }

    public void selectPlayer1()
    {
        GameManager.playerType = 1;
        startButton.GetComponent<Button>().interactable = true;
        text.GetComponent<TextMeshProUGUI>().text = "Red Wing";
        textBack.GetComponent<TextMeshProUGUI>().text = "Red Wing";
        text.GetComponent<TextMeshProUGUI>().color = Color.red;
        textBack.GetComponent<TextMeshProUGUI>().color = Color.black;
    }
    public void selectPlayer2()
    {
        GameManager.playerType = 2;
        startButton.GetComponent<Button>().interactable = true;
        text.GetComponent<TextMeshProUGUI>().text = "Green Spear";
        textBack.GetComponent<TextMeshProUGUI>().text = "Green Spear";
        text.GetComponent<TextMeshProUGUI>().color = Color.green;
        textBack.GetComponent<TextMeshProUGUI>().color = Color.black;
    }
    public void selectPlayer3()
    {
        GameManager.playerType = 3;
        startButton.GetComponent<Button>().interactable = true;
        text.GetComponent<TextMeshProUGUI>().text = "Blue Pointer";
        textBack.GetComponent<TextMeshProUGUI>().text = "Blue Pointer";
        text.GetComponent<TextMeshProUGUI>().color = Color.blue;
        textBack.GetComponent<TextMeshProUGUI>().color = Color.black;
    }
    public void selectPlayer4()
    {
        GameManager.playerType = 4;
        startButton.GetComponent<Button>().interactable = true;
        text.GetComponent<TextMeshProUGUI>().text = "Black Bruiser";
        textBack.GetComponent<TextMeshProUGUI>().text = "Black Bruiser";
        text.GetComponent<TextMeshProUGUI>().color = Color.black;
        textBack.GetComponent<TextMeshProUGUI>().color = Color.white;
    }
}
