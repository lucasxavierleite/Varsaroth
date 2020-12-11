using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField]
    GameObject _HUD = null;

    [SerializeField]
    private TextMeshProUGUI _currentFloorText;
    
    void Start()
    {
        _HUD.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Quote) && Time.timeScale > 0)
		{
			ToggleHUDButton();
		}
        
        _currentFloorText.text = StageData._data.GetStage() + "<sup>" + Ordinal(StageData._data.GetStage()) + "</sup> floor";
    }

    public void ToggleHUDButton()
	{
		if (!_HUD.activeSelf) // if hidden, show
        {
            _HUD.SetActive(true);
        }
        else // if not hidden, hide
        {
            _HUD.SetActive(false);
        }
	}
    
    private string Ordinal(int n)
    {
        if (n <= 0)
        {
            return n.ToString();
        }

        switch (n % 100)
        {
            case 11:
            case 12:
            case 13:
                return "th";
        }
    
        switch (n % 10)
        {
            case 1:
                return "st";
            case 2:
                return "nd";
            case 3:
                return "rd";
            default:
                return "th";
        }
    }

}
