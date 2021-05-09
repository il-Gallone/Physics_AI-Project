using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToStart : MonoBehaviour
{
     public GameObject StartMenu;

    public void backToStartMenu()
    {

        StartMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
