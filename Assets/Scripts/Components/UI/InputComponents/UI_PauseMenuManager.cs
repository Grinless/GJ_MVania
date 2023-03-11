using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum MenuState
{
    INACTIVE,
    PAUSE,
    SETTINGS
}

public class UI_PauseMenuManager : MonoBehaviour
{
    public static UI_PauseMenuManager instance;
    public MenuState lastState;
    public MenuState menuState = MenuState.INACTIVE;

    public GameObject pauseMenuObject;
    public GameObject settingsMenuObject;
    public int pauseMenuIndex = 0;
    public int settingsMenuIndex = 0;
    public TextMeshProUGUI[] pauseMenuTextOptions;
    public TextMeshProUGUI[] settingsMenuTextOptions;

    private void Start()
    {
        //Create singleton for script access. 
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        pauseMenuObject.SetActive(false);
        settingsMenuObject.SetActive(false);
    }

    private void Update()
    {
        lastState = menuState; //Store the last state for comparisons. 

        if (menuState == MenuState.INACTIVE)
        {
            State_Update_Inactive();
            return;
        }
        if (menuState == MenuState.PAUSE)
        {
            State_Update_Pause();
            return;
        }
    }

    private void State_Update_Inactive()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && CheckIfInactive()) //Open the menu. 
        {
            UpdateMenuDisplay(MenuState.PAUSE);
        }
    }

    private void State_Update_Pause()
    {
        IMenuAction action;

        if (Input.GetKeyUp(KeyCode.Escape)) //Close the menu. 
        {
            UpdateMenuDisplay(MenuState.INACTIVE);
            return;
        }

        UpdateActiveMenu(pauseMenuTextOptions, ref pauseMenuIndex);

        if (Input.GetKeyDown(PlayerController.instance.Input.jumpKey.keyA))
        {
            action = pauseMenuTextOptions[pauseMenuIndex].GetComponent<IMenuAction>();

            if (action != null)
            {
                action.ExecuteAction();
            }
        }
    }

    private void UpdateMenuDisplay(MenuState nextState)
    {
        menuState = nextState;

        switch (nextState) //Update the current menu display. 
        {
            case MenuState.INACTIVE: //Hide everything. 
                Time.timeScale = 1;
                pauseMenuObject.SetActive(false);
                settingsMenuObject.SetActive(false);
                break;
            case MenuState.PAUSE: //Show pause menu. 
                Time.timeScale = 0;
                pauseMenuObject.SetActive(true);
                settingsMenuObject.SetActive(false);
                break;
            case MenuState.SETTINGS: //Show Settings menu. 
                Time.timeScale = 0;
                pauseMenuObject.SetActive(false);
                settingsMenuObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void UpdateActiveMenu(TextMeshProUGUI[] options, ref int currentIndex)
    {
        int localIndex = currentIndex;
        int maxIndex = options.Length - 1;

        //Get new menu index. 
        if (Input.GetKeyUp(PlayerController.instance.Input.leftKey.keyA))
            localIndex--;

        if (Input.GetKeyUp(PlayerController.instance.Input.rightKey.keyA))
            localIndex++;

        //Wrap the index if overflowing past array bounds. 
        if (localIndex < 0)
            localIndex = maxIndex;
        else if (localIndex > maxIndex)
            localIndex = 0;

        //Adjust the menu selection colors. 
        foreach (TextMeshProUGUI item in pauseMenuTextOptions)
        {
            item.color = Color.white;
        }

        options[localIndex].color = Color.red;

        currentIndex = localIndex;
    }

    private bool CheckIfInactive() => (lastState == MenuState.INACTIVE && menuState == MenuState.INACTIVE);

    public void ClosePauseMenu() => UpdateMenuDisplay(MenuState.INACTIVE);

    public void OpenSettingsMenu() => UpdateMenuDisplay(MenuState.SETTINGS);

    public void CloseSettingsMenu() => UpdateMenuDisplay(MenuState.PAUSE);
}
