using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_PauseMenuManager : MonoBehaviour
{
    public GameObject menuObject; 
    public bool menuOpen = false;
    public int index = 0;
    public int maxIndex;
    public TextMeshProUGUI[] textOptions; 

    private void Start()
    {
        menuObject.SetActive(false);
        maxIndex = textOptions.Length -1;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            menuOpen = !menuOpen;
            UpdateMenuState();
        }

        if (menuOpen)
        {
            //Get new menu index. 
            if (Input.GetKeyUp(PlayerController.instance.Input.leftKey.keyA))
                index--; 

            if (Input.GetKeyUp(PlayerController.instance.Input.rightKey.keyA))
                index++;

            //Wrap the index if overflowing past array bounds. 
            if (index < 0)
                index = maxIndex; 
            else if(index > maxIndex)
                index = 0;

            //Adjust the menu selection colors. 
            foreach (TextMeshProUGUI item in textOptions)
            {
                item.color = Color.white; 
            }

            textOptions[index].color = Color.red;

            if (Input.GetKey(PlayerController.instance.Input.jumpKey.keyA))
            {
                //Do some activation logic. 
                if(index == 1)
                    Application.Quit();
                if(index == 0)
                {
                    //Open the settings menu. 
                }
            }
        }
        
    }

    private void UpdateMenuState()
    {
        if (menuOpen)
        {
            Time.timeScale = 0;
            menuObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            menuObject.SetActive(false);
        }
    }

    private void CheckInputKeys()
    {

    }
}
