using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    [SerializeField]
    public GameObject[] Down;
    [SerializeField]
    public GameObject[] Up;
    [SerializeField]
    public GameObject[] Right;
    [SerializeField]
    public GameObject[] Left;
    [SerializeField]
    public GameObject[] StartRoom;
    [SerializeField]
    public GameObject _nextFloor;

    public int _leftToSpawn = 15;
    public List<GameObject> _rooms;

    void Start() 
    {
        int _rand = Random.Range(0, StartRoom.Length);
        Debug.Log(_rand);
        Instantiate(StartRoom[_rand], transform.position, Quaternion.identity);
        Invoke("SpawnGate", 2f);
    }

    /// <summary>
    /// This Method spawns the gate that leads to the next floor, in the middle of the last room
    /// </summary>
    void SpawnGate()
    {
        Instantiate(_nextFloor,_rooms[_rooms.Count - 1].transform.position, Quaternion.identity);
        _rooms[_rooms.Count - 1].SendMessage("UpgradeRoom");
    }
}
