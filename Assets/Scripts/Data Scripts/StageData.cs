using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    // This class allows us to manage any data that has to be transitioned from a scene to another 
    public static StageData _data;
    public int _stage;
    private const int _baseHP = 100;
    private int _playerHP;
    private int _maxHP = _baseHP;

    private void Start()
    {
        _data = gameObject.GetComponent<StageData>();
    }
    public void NextLevel()//goes to next level
    {
        _stage++;
        _maxHP += 25;  // increases player max hp by 25
        _playerHP += 25; // heals player for 25hp
    }

    public void Restart()//go back to level 1
    {
        _stage = 1;
        _playerHP = _baseHP;
        _maxHP = _baseHP;
    }

    public int GetStage()// returns current stage
    {
        return _stage;
    }

    public int GetHP()
    {
        return _playerHP;
    }

    public int GetMAXHP()
    {
        return _maxHP;
    }

    public void SetHP(int hp)
    {
        _playerHP = hp;
    }
}
