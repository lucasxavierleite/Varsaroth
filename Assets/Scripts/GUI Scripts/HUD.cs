using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField]
    GameObject _HUD = null;
    
    void Start()
    {
        _HUD.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Quote))
		{
			ToggleHUDButton();
		}
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
}
