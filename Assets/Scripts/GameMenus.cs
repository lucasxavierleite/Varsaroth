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
    GameObject playerUI = null;

    // Start is called before the first frame update
    void Start()
    {
        playerUI.SetActive(true);
        ContinueButton();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
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
        playerUI.SetActive(false);
        deathScreen.SetActive(true);
    }

    public void ReturnToMenu()
    {
        // zero run score and return to menu
        deathScreen.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
