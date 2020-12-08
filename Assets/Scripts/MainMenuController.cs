using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
	[SerializeField]
    private Color32 menuItemDefaultColor;
    
    [SerializeField]
    private Color32 menuItemHighlightColor;

    public void PlayButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        StageData._data.Restart(); //sets game to stage 1
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        
    }
    
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is closing");
    }

    public void ToggleMute(AudioSource music)
    {
        music.mute = !music.mute;
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


}
