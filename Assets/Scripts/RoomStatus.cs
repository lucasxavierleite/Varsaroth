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
        if (_enemiesRemaining <= 0)
        {
            for (int a = 0; a < transform.childCount; a++)
            {
                if (transform.GetChild(a).tag == "Door")
                {
                    Debug.Log(transform.GetChild(a).name);
                    transform.GetChild(a).gameObject.SetActive(true); // open doors after all enemies defeated
                }
            }
            _enemiesRemaining--;
        }        
    }

    // When room becomes visible, if it hasn't been visited yet, spawn enemies in it
    private void OnBecameVisible()
    {
        if (_roomVisited == false)
        {
            _roomVisited = true;
            _currentRoom = true;

            if (_isBoss == false)
            {
                for (int a = 0; a < transform.childCount; a++)
                {
                    if (transform.GetChild(a).tag == "Door")
                    {
                        transform.GetChild(a).gameObject.SetActive(false);  // close doors while enemies still alive
                        // could change later to another sprite of a closed door
                    }
                }
                _enemiesRemaining = Random.Range(1, 3);
                _roomSpawn.SpawnEnemies(_enemiesRemaining);
            }
            else
            {
                //Spawn Boss
            }
        }
        
    }

    private void OnBecameInvisible()
    {
        _currentRoom = false;
    }

    public void UpgradeRoom()   // turns room into a boss room
    {
        _isBoss = true;
        Debug.Log("sala boss");
    }

}
