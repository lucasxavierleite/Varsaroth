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
        if (_roomVisited == false)
        {
            transform.Find("Torch").gameObject.SetActive(false);
            transform.Find("Torch (1)").gameObject.SetActive(false);
            transform.Find("Torch (2)").gameObject.SetActive(false);
            transform.Find("Torch (3)").gameObject.SetActive(false);
            transform.Find("Torch (4)").gameObject.SetActive(false);
            transform.Find("Torch (5)").gameObject.SetActive(false);
            transform.Find("Torch (6)").gameObject.SetActive(false);
            transform.Find("Torch (7)").gameObject.SetActive(false);
			transform.Find("Light").gameObject.SetActive(false);
        }
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
                    transform.GetChild(a).GetComponent<Animator>().SetTrigger("open");
                    transform.GetChild(a).GetComponent<BoxCollider2D>().isTrigger = true; // open doors after all enemies were defetead
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

            transform.Find("Torch").gameObject.SetActive(true);
            transform.Find("Torch (1)").gameObject.SetActive(true);
            transform.Find("Torch (2)").gameObject.SetActive(true);
            transform.Find("Torch (3)").gameObject.SetActive(true);
            transform.Find("Torch (4)").gameObject.SetActive(true);
            transform.Find("Torch (5)").gameObject.SetActive(true);
            transform.Find("Torch (6)").gameObject.SetActive(true);
            transform.Find("Torch (7)").gameObject.SetActive(true);
			transform.Find("Light").gameObject.SetActive(true);

            if (_isBoss == false)
            {
                for (int a = 0; a < transform.childCount; a++)
                {
                    if (transform.GetChild(a).tag == "Door")
                    {
                        transform.GetChild(a).GetComponent<BoxCollider2D>().isTrigger = false; // close doors while enemies are still alive
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
