using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    // This class allows us to manage any data that has to be transitioned from a scene to another 
    public static StageData _data;
    public int _stage;

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
    }

    public int GetStage()// returns current stage
    {
        return _stage;
    }
}
