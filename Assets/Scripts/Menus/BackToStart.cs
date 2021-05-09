using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToStart : MonoBehaviour
{
    public GameObject StartMenu;
    public Button SelectStart;

    public void backToStartMenu()
    {
        //if back is selected close the instruction screen and go back to the main menu
        StartMenu.SetActive(true);
        SelectStart.Select();
        this.gameObject.SetActive(false);
    }
}
