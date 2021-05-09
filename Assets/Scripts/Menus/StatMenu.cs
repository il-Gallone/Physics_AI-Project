using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatMenu : MonoBehaviour
{
    public GameObject CharacterMenu;
    public GameObject InstructionMenu;

    public void PlayGame()
    {
        CharacterMenu.SetActive(true);
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
        this.gameObject.SetActive(false);
    }
}