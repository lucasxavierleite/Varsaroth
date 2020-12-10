using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;

public class TransitionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _transitionPanel;
    
    [SerializeField]
    private GameObject _menuCanvas;
    
    [SerializeField]
    private GameObject _HUDCanvas;
    
    [SerializeField]
    private TextAsset _storyTextAsset;
    
    private List<List<string>> _storyList = new List<List<string>>();

    [SerializeField]
    private TextMeshProUGUI _storyText;

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
		_stageBackgroundMusic = Camera.main.GetComponent<AudioSource>();
		_storyBackgroundMusic = GetComponent<AudioSource>();
        ReadStory();
        Show();
    }
    
    void Update()
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

        // check if trapdoor SFX is finished before playing the backround music
        
        // if (!AudioManager.instance.isPlaying("Trapdoor"))
        // {
        //  	_storyBackgroundMusic.Play(); 
        // }
    }

    public void Show()
    {
        _menuCanvas.SetActive(false);
        _HUDCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        _transitionPanel.SetActive(true);

        if (_storyList[StageData._data.GetStage()].Count > 1) // if there's more than one page, show next button
        {
            ShowNextButton();
        }
        else // if there's only one page, show continue button
        {
            ShowContinueButton();
        }

        _stageBackgroundMusic.Stop();
		_storyBackgroundMusic.Play();

        // if it's not the first stage, play the trapdoor SFX first
        
        // if (StageData._data.GetStage() > 1)
        // {
        //     AudioManager.instance.Play("Trapdoor");
        // }

		_currentPage = 1;
        UpdateText();
    }

    public void Hide()
    {
        Time.timeScale = 1;
        _menuCanvas.SetActive(true);
        _HUDCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        _transitionPanel.SetActive(false);
		_storyBackgroundMusic.Stop();
        _stageBackgroundMusic.Play();
    }

    public void Next()
    {
        _currentPage++;
        UpdateText();
        
        if (_storyList[StageData._data.GetStage()].Count <= _currentPage) // if there's no more text to show
        {
            ShowContinueButton();
        }
    }

    public void Continue()
    {
        Hide();
    }

    private void ReadStory()
    {
        string[] stagesTexts = Regex.Split(_storyTextAsset.text, STAGE_TAG);

        foreach (string st in stagesTexts)
        {
            List<string> stp = new List<string>(Regex.Split(st, PAGE_TAG));
            stp.RemoveAt(0);
            _storyList.Add(stp);
        }

        // PrintStory();

    }

    private void UpdateText()
    {
        _storyText.text = _storyList[StageData._data.GetStage()][_currentPage - 1];
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
    
    private void PrintStory()
    {
        for (int i = 0; i < _storyList.Count; i++)
        {
            for (int j = 0; j < _storyList[i].Count; j++)
            {
                Debug.Log("Element " + i + ", " + j + ": " + _storyList[i][j]);
            }
        }
    }
}