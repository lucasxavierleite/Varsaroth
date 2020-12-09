using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingManager : MonoBehaviour
{
    GameObject _player;
    [SerializeField]
    GameObject _amulet;
    bool spawned = false;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        
        if (spawned == false)
        {
            float distance = Mathf.Abs(transform.position.x - _player.transform.position.x) + Mathf.Abs(transform.position.y - _player.transform.position.y);

            if (distance < 40)
            {
                Instantiate<GameObject>(_amulet, transform.position, Quaternion.identity);
                spawned = true;
            }
        }
    }
}
