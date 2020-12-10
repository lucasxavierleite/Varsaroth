using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenuController : MonoBehaviour
{
	[SerializeField]
    private Color32 _menuItemDefaultColor;
    
    [SerializeField]
    private Color32 _menuItemHighlightColor;

    [SerializeField]
    private GameObject _popUpPanel;
    
    [SerializeField]
    private GameObject _volume;

    [SerializeField]
    private GameObject[] _buttons;

    [SerializeField]
    private TextAsset _creditsTextAsset;

    [SerializeField]
    private TextMeshProUGUI _creditsText;

    void Start()
    {
        _creditsText.text = _creditsTextAsset.text;
    }
    
    void Update()
    {
        if (_popUpPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            _popUpPanel.SetActive(false);
            _volume.SetActive(false);
            foreach (GameObject b in _buttons)
            {
                b.GetComponent<EventTrigger>().enabled = true;
            }
        }
    }

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
        text.color = _menuItemHighlightColor;
    }
    
    public void MenuButtonPointerExit(TextMeshProUGUI text)
    {
        text.color = _menuItemDefaultColor;
    }

	public void MenuButtonPointerEnter(Image image)
    {
        image.color = _menuItemHighlightColor;
    }
    
    public void MenuButtonPointerExit(Image image)
    {
        image.color = _menuItemDefaultColor;
    }
}
