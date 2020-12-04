using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStatus : MonoBehaviour
{
    public bool _roomVisited;// indicates if the room has been visited before

    public bool _currentRoom = false;
    public int _enemiesRemaining = -1;

    EnemySpawner _roomSpawn;
    bool _isBoss = false; // indicates if a room is a boss room
    

    private void Start()
    {
        _roomSpawn = GetComponentInParent<EnemySpawner>();
    }

    private void Update()
    {
        if (_currentRoom) Debug.Log(_enemiesRemaining);
        if (_currentRoom && _enemiesRemaining <= 0)
        {
            _currentRoom = false;
            for (int a = 0; a < transform.childCount; a++)
            {
                if (transform.GetChild(a).tag == "Door")
                {
                    //transform.GetChild(a).gameObject.SetActive(true); // open doors after all enemies defeated
                    transform.GetChild(a).GetComponent<Animator>().SetTrigger("open"); // open doors after all enemies were defetead
                    Debug.Log("Abir porta");
                    transform.GetChild(a).gameObject.layer = 12;
                }
            }
        }        
    }

    // When room becomes visible, if it hasn't been visited yet, spawn enemies in it
    private void OnBecameVisible()
    {
        _currentRoom = true;
        if (_roomVisited == false)
        {
            _roomVisited = true;
            gameObject.layer = 12;
            

            if (_isBoss == false)
            {
                for (int a = 0; a < transform.childCount; a++)
                {
                    if (transform.GetChild(a).tag == "Door")
                    {
                        //transform.GetChild(a).gameObject.SetActive(false);  // close doors while enemies still alive
                        // could change later to another sprite of a closed door
                    }
                }
                _enemiesRemaining = Random.Range(StageData._data.GetStage(), (StageData._data.GetStage() + 1) * 3/2);
                _roomSpawn.SpawnEnemies(_enemiesRemaining);
            }
            else
            {
                //Spawn Boss
            }
        }
        
    }

    public void UpgradeRoom()   // turns room into a boss room
    {
        _isBoss = true;
        Debug.Log("sala boss");
    }

}
