using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenus : MonoBehaviour
{
    [SerializeField]
    GameObject pauseScreen = null;
    [SerializeField]
    GameObject deathScreen = null;
    [SerializeField]
    GameObject HUD = null;

    // Start is called before the first frame update
    void Start()
    {
        HUD.SetActive(true);
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
		if (Input.GetKeyDown(KeyCode.Quote))
		{
			ToggleHUDButton();
		}
    }

    public void PauseButton()   //pause or unpause game
    {
        if (!pauseScreen.activeSelf) // if not paused, pause
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else // if paused, unpause
        {
            ContinueButton();
        }
    }

    public void ContinueButton()    // unpause game
    {
        pauseScreen.SetActive(false);
        deathScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void DeathButton()
    {
        HUD.SetActive(false);
        deathScreen.SetActive(true);
    }

    public void ReturnToMenu()
    {
        // zero run score and return to menu
        deathScreen.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

	public void ToggleHUDButton()
	{
		if (!HUD.activeSelf) // if hidden, show
        {
            HUD.SetActive(true);
        }
        else // if not hidden, hide
        {
            HUD.SetActive(false);
        }
	}
}
