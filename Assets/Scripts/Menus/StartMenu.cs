using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject CharacterMenu;
    public GameObject InstructionMenu;

    public Button SelectHowToPlay;
    public Button SelectPlayGame;

    public void PlayGame()
    {
        //When Play is clicked, open the character selector and close the start screen
        CharacterMenu.SetActive(true);
        SelectPlayGame.Select();
        this.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        //If quit is clicked then close the game
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void HowToPlay()
    {
        //When How to Play is clicked open the instruction menu and close the start screen
        InstructionMenu.SetActive(true);
        SelectHowToPlay.Select();
        this.gameObject.SetActive(false);
    }
}