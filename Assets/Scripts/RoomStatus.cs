using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStatus : MonoBehaviour
{
    public bool _roomVisited;
    int _enemiesRemaining = -1;
    // _roomVisited indicates if the room has been visited before

    private void Update()
    {
        if (_enemiesRemaining == 0)
        {
            for (int a = 0; a < transform.childCount; a++)
            {
                if (transform.GetChild(a).tag == "Door")
                {
                    Debug.Log(transform.GetChild(a).name);
                    transform.GetChild(a).gameObject.SetActive(true);
                }
            }
            _enemiesRemaining--;
        }        
    }

    private void OnBecameVisible()
    {
        if (_roomVisited == false)
        {
            _roomVisited = true;

            for (int a = 0; a < transform.childCount; a++)
            {
                if (transform.GetChild(a).tag == "Door")
                {
                    Debug.Log(transform.GetChild(a));
                    transform.GetChild(a).gameObject.SetActive(false);
                }
            }
            // _enemiesRemaining = spawnmobs
        }
        _enemiesRemaining = 0;
    }

}
