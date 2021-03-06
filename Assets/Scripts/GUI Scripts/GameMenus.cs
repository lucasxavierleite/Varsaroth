﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenus : MonoBehaviour
{
    [SerializeField]
    GameObject pauseScreen = null;
    
    [SerializeField]
    GameObject deathScreen = null;

	[SerializeField]
    GameObject controlsScreen = null;

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
		if (Input.GetKeyDown("escape") && controlsScreen.activeSelf)
		{
			foreach (Transform child in pauseScreen.transform)
			{
				child.gameObject.SetActive(true);
			}
			controlsScreen.SetActive(false);
		}

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
        if (deathScreen.activeSelf)
        {
            return;
        }

        if (!pauseScreen.activeSelf) // if not paused and game over screen is not showing, pause
        {
            ShowCursor();
            Time.timeScale = 0;
            volume.SetActive(true);
            pauseScreen.SetActive(true);
			AudioManager.instance.PauseAll();
        }
        else // if paused, unpause
        {
            ContinueButton();
        }
    }

    public void ContinueButton()    // unpause game
    {
        HideCursor();
        pauseScreen.SetActive(false);
        deathScreen.SetActive(false);
        volume.SetActive(false);
        Time.timeScale = 1;
		AudioManager.instance.UnPauseAll();
    }

    public void DeathButton()
    {
        ShowCursor();
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

	public void MenuButtonPointerEnter(Image image)
    {
        image.color = menuItemHighlightColor;
    }
    
    public void MenuButtonPointerExit(Image image)
    {
        image.color = menuItemDefaultColor;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
