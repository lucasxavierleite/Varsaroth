using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;

public class TransitionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _transitionCanvas;
    
    [SerializeField]
    private GameObject _menuCanvas;
    
    [SerializeField]
    private GameObject _HUDCanvas;
    
    [SerializeField]
    private TextAsset _storyTextAsset;
    
    private List<List<string>> _transitionsList = new List<List<string>>();

    [SerializeField]
    private TextMeshProUGUI _storyText;

    [SerializeField]
    private int _currentStage;

    [SerializeField]
    private int _currentPage;

    [SerializeField]
    private GameObject _continueButton;
    
    [SerializeField]
    private GameObject _nextButton;
    
    [SerializeField]
    private Color32 menuItemDefaultColor;
    
    [SerializeField]
    private Color32 menuItemHighlightColor;

    private AudioSource _stageBackgroundMusic;
    private AudioSource _storyBackgroundMusic;
    
    private const string STAGE_TAG = @"\[STAGE \d\]";
    private const string PAGE_TAG = @"\(PAGE \d\)";

    void Start()
    {
        AudioManager.instance.EnablePlayInTransition("Trapdoor");
        AudioManager.instance.EnablePlayInTransition("MenuButtonHover");
        AudioManager.instance.EnablePlayInTransition("MenuButtonClick");
		_stageBackgroundMusic = Camera.main.GetComponent<AudioSource>();
		_storyBackgroundMusic = GetComponent<AudioSource>();
        _currentStage = StageData._data.GetStage();
        ReadStory();
        Show();
    }

    void Update()
    {
        if (_transitionCanvas.activeSelf)
        {
            if (Input.GetKeyDown("space") || Input.GetKeyDown("return"))
            {
                if (_continueButton.activeSelf)
                {
                    Continue();
                }
                else if (_nextButton.activeSelf)
                {
                    Next();
                }
            }

            AudioManager.instance.TransitionSounds();
        }
    }

    public void Show()
    {
        Cursor.visible = true;
        _menuCanvas.SetActive(false);
        _HUDCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        _transitionCanvas.SetActive(true);

        if (_transitionsList[_currentStage].Count > 1) // if there's more than one page, show next button
        {
            ShowNextButton();
        }
        else // if there's only one page, show continue button
        {
            ShowContinueButton();
        }

        _stageBackgroundMusic.Stop();

        // if it's not the first or last stage, play the trapdoor SFX
        
        if (_currentStage > 1 && _currentStage < _transitionsList.Count - 1)
        {
            AudioManager.instance.Play("Trapdoor");
        }
        
        _storyBackgroundMusic.Play();

		_currentPage = 1;
        UpdateText();
    }

    public void Hide()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        _menuCanvas.SetActive(true);
        _HUDCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        _transitionCanvas.SetActive(false);
    }

    public void Next()
    {
        _currentPage++;
        UpdateText();
        
        if (_transitionsList[_currentStage].Count <= _currentPage) // if there's no more text to show
        {
            ShowContinueButton();
        }
    }

    public void Continue()
    {
        if (_currentStage < _transitionsList.Count - 1) // if it's not the final transition
        {
            Hide();
            _storyBackgroundMusic.Stop();
            _stageBackgroundMusic.Play();
            AudioManager.instance.Play("GateSound");
        }
        else // if it is, load credits scene
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
        }

    }

    private void ReadStory()
    {
        string[] stagesTexts = Regex.Split(_storyTextAsset.text, STAGE_TAG);

        foreach (string st in stagesTexts)
        {
            List<string> stp = new List<string>(Regex.Split(st, PAGE_TAG));
            stp.RemoveAt(0);
            _transitionsList.Add(stp);
        }

        // PrintStory();

    }

    private void UpdateText()
    {
        _storyText.text = _transitionsList[_currentStage][_currentPage - 1];
    }

    private void ShowNextButton()
    {
        _continueButton.SetActive(false);
        _nextButton.SetActive(true);
    }
    
    private void ShowContinueButton()
    {
        _continueButton.SetActive(true);
        _nextButton.SetActive(false);
    }
    
    public void MenuButtonPointerEnter(TextMeshProUGUI text)
    {
        text.color = menuItemHighlightColor;
    }
    
    public void MenuButtonPointerExit(TextMeshProUGUI text)
    {
        text.color = menuItemDefaultColor;
    }

    public void ShowFinalTransition()
    {
        _currentStage = _transitionsList.Count - 1;
        Show();
    }
    
    private void PrintStory()
    {
        for (int i = 0; i < _transitionsList.Count; i++)
        {
            for (int j = 0; j < _transitionsList[i].Count; j++)
            {
                Debug.Log("Element " + i + ", " + j + ": " + _transitionsList[i][j]);
            }
        }
    }
}