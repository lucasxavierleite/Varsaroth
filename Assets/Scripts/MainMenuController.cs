using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    public void PlayButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        StageData._data.Restart(); //sets game to stage 1
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        
    }


}
