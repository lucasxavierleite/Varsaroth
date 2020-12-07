using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    // This class allows us to manage any data that has to be transitioned from a scene to another 
    public static StageData _data;
    public int _stage;
    private int _playerHP;
    private int _maxHP = 100;

    private void Start()
    {
        _data = gameObject.GetComponent<StageData>();
    }
    public void NextLevel()//goes to next level
    {
        _stage++;
    }

    public void Restart()//go back to level 1
    {
        _stage = 1;
        _playerHP = _maxHP;
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
