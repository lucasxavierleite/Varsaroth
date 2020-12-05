using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField]
    int _openingDirection;
    // OpeningDirection indicates what kind of room needs to spawn in the location
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door

    [SerializeField]
    GameObject _door;

    RoomTemplates _roomTemplates;

    int _rand;
    public bool _spawned = false;
    public bool _connected = false;
    float _destroyAfter = 5f;

    void Start()
    {
        Destroy(gameObject, _destroyAfter); // clears up memory by removing the spawn points
        _roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        
        Invoke("SpawnRooms", 0.1f);
        Invoke("CheckMissingRoom", 1f);

    }

    /// <summary>
    /// This Method Spawns rooms where the spawn points are located, following the spawn rules
    /// </summary>
    void SpawnRooms()
    {
        if (_spawned == false)
        {
            // if spawn point needs to have a room with a bottom door
            if (_openingDirection == 1)
            {
                // Makes sure levels have minimum ammount of rooms
                if (_roomTemplates._minimumSize > 1)
                {
                    _rand = Random.Range(0, _roomTemplates.Down.Length - 1);
                }
                // Spawn room with bottom door
                else if (_roomTemplates._leftToSpawn > 1)
                {
                    _rand = Random.Range(0, _roomTemplates.Down.Length);
                }
                // After maximum ammount of rooms has been reached, spawn a dead end
                else
                {
                    _rand = _roomTemplates.Down.Length - 1;
                }
                Instantiate(_roomTemplates.Down[_rand], transform.position, Quaternion.identity);
            }
            // if spawn point needs to have a room with a bottoptom door
            else if (_openingDirection == 2)
            {
                // Makes sure levels have minimum ammount of rooms
                if (_roomTemplates._minimumSize > 1)
                {
                    _rand = Random.Range(0, _roomTemplates.Up.Length - 1);
                }
                // Spawn room with top door
                else if (_roomTemplates._leftToSpawn > 1)
                {
                    _rand = Random.Range(0, _roomTemplates.Up.Length);
                }
                // After maximum ammount of rooms has been reached, spawn a dead end
                else
                {
                    _rand = _roomTemplates.Up.Length - 1;
                }
                Instantiate(_roomTemplates.Up[_rand], transform.position, Quaternion.identity);
            }
            // if spawn point needs to have a room with a left door
            else if (_openingDirection == 3)
            {
                // Makes sure levels have minimum ammount of rooms
                if (_roomTemplates._minimumSize > 1)
                {
                    _rand = Random.Range(0, _roomTemplates.Left.Length - 1);
                }
                // Spawn room with left door
                else if (_roomTemplates._leftToSpawn > 1)
                {
                    _rand = Random.Range(0, _roomTemplates.Left.Length);
                }
                // After maximum ammount of rooms has been reached, spawn a dead end
                else
                {
                    _rand = _roomTemplates.Left.Length - 1;
                }
                Instantiate(_roomTemplates.Left[_rand], transform.position, Quaternion.identity);
            }
            // if spawn point needs to have a room with a right door
            else if (_openingDirection == 4)
            {
                // Makes sure levels have minimum ammount of rooms
                if (_roomTemplates._minimumSize > 1)
                {
                    _rand = Random.Range(0, _roomTemplates.Right.Length - 1);
                }
                // Spawn room with right door
                else if (_roomTemplates._leftToSpawn > 1)
                {
                    _rand = Random.Range(0, _roomTemplates.Right.Length);
                }
                else
                // After maximum ammount of rooms has been reached, spawn a dead end
                {
                    _rand = _roomTemplates.Right.Length - 1;
                }
                Instantiate(_roomTemplates.Right[_rand], transform.position, Quaternion.identity);
            }
            _spawned = true;
            _connected = true;
            _roomTemplates._leftToSpawn--;
            _roomTemplates._minimumSize--;
        }
    }

    /// <summary>
    /// This Method checks if a spawn point is unable to spawn a room, if this is true, remove the door in this direction
    /// </summary>
    void CheckMissingRoom()
    {
       
        if (_connected == false)
        {
            Destroy(_door);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        _roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        // if spawn point is colliding with a destroyer, set spawn point to spawned and connected
        if (other.CompareTag("Destroyer"))
        {
            _spawned = true;
            _connected = true;

            // if the room it is colliding with does not have a door leading into this room, delete this door
            if (_openingDirection == 1)
            {
                if (other.transform.parent.Find("Door_Down") == null)
                {
                    Destroy(_door);
                }
            }
            else if (_openingDirection == 2)
            {
                if (other.transform.parent.Find("Door_Up") == null)
                {
                    Destroy(_door);
                }
            }
            else if (_openingDirection == 3)
            {
                if (other.transform.parent.Find("Door_Left") == null)
                {
                    Destroy(_door);
                }
            }
            else if (_openingDirection == 4)
            {
                if (other.transform.parent.Find("Door_Right") == null)
                {
                    Destroy(_door);
                }
            }
            
        }
        // else if spawn point is colliding with another spawn point, set it to spawned and destroy its door
        else if (other.CompareTag("RoomSpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>()._spawned == false && _spawned == false)
            {
                // Remove door from this side of wall
                Destroy(_door);
            }
            _spawned = true;
            
        }
    }

}
