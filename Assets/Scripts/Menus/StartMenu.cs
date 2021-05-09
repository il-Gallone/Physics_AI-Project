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
        CharacterMenu.SetActive(true);
        SelectPlayGame.Select();
        this.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void HowToPlay()
    {
        InstructionMenu.SetActive(true);
        SelectHowToPlay.Select();
        this.gameObject.SetActive(false);
    }
}