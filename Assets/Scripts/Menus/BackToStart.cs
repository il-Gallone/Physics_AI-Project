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

        StartMenu.SetActive(true);
        SelectStart.Select();
        this.gameObject.SetActive(false);
    }
}
