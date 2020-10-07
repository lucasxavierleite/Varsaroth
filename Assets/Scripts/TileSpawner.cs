using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] TilePatterns;

    // Start is called before the first frame update
    void Start()
    {
        int _rand = Random.Range(0, TilePatterns.Length);
        Instantiate(TilePatterns[_rand], transform.position, Quaternion.identity);
    }
}
