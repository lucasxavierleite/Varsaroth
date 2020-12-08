﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMenus : MonoBehaviour
{
    [SerializeField]
    GameObject pauseScreen = null;
    
    [SerializeField]
    GameObject deathScreen = null;

    [SerializeField]
    private Color32 menuItemDefaultColor;
    
    [SerializeField]
    private Color32 menuItemHighlightColor;

    [SerializeField]
    private GameObject volume;

    // Start is called before the first frame update
    void Start()
    {
        ContinueButton();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape") && PlayerMovement._currentAnimationState != 4)
        {
            PauseButton();
        } 
        if (PlayerMovement._currentAnimationState == 4)
        {
            DeathButton();
        }
    }

    public void PauseButton()   //pause or unpause game
    {
        if (!pauseScreen.activeSelf) // if not paused, pause
        {
            Time.timeScale = 0;
            volume.SetActive(true);
            pauseScreen.SetActive(true);
        }
        else // if paused, unpause
        {
            ContinueButton();
            volume.SetActive(false);
        }
    }

    public void ContinueButton()    // unpause game
    {
        pauseScreen.SetActive(false);
        deathScreen.SetActive(false);
        volume.SetActive(false);
        Time.timeScale = 1;
    }

    public void DeathButton()
    {
        volume.SetActive(true);
        deathScreen.SetActive(true);
    }

    public void RestartGame()
    {
        StageData._data.Restart(); //sets game to stage 1
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void ReturnToMenu()
    {
        // zero run score and return to menu
        deathScreen.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    public void MenuButtonPointerEnter(TextMeshProUGUI text)
    {
        text.color = menuItemHighlightColor;
    }
    
    public void MenuButtonPointerExit(TextMeshProUGUI text)
    {
        text.color = menuItemDefaultColor;
    }
}
