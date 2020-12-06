using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] _enemies = null;

    /// <summary>
    /// This method spawns enemies Randomly in the middle of the room
    /// </summary>
    /// <param name="_num"></param>
    public void SpawnEnemies(int _num)
    {
        float _spawnX, _spawnY;
        for (int i = 0; i < _num; i++)
        {
            _spawnX = Random.Range (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 120, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 120);
            _spawnY = Random.Range (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + 50, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - 50);

            Vector2 spawnPosition = new Vector2(_spawnX, _spawnY);
            Instantiate(_enemies[Random.Range(0, 1)], spawnPosition, Quaternion.identity);

        }
    }
}
