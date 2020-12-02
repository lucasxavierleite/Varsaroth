using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawn : MonoBehaviour
{

    [SerializeField]
    GameObject Tutorial;

    // spawns the tutorial message in the first stage of the game
    void Start()
    {
        if (StageData._data.GetStage() == 1)
        {
            Tutorial.SetActive(true);
        }
        else
        {
            Tutorial.SetActive(false);
        }
    }

}
