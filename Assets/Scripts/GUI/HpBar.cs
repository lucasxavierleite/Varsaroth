using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
	
	[SerializeField]
	private Slider _slider;
	
	[SerializeField]

	private Text _text;

	public void SetMaxHp(int maxHp) {
		_slider.maxValue = maxHp;
		SetHp(maxHp);
	}

	public void SetHp(int hp) {
		_slider.value = Mathf.Clamp(hp, _slider.minValue, _slider.maxValue);
		_text.text = _slider.value.ToString() + "/" + _slider.maxValue;
	}
}
