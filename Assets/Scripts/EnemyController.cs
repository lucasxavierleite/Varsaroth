using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  
    public Transform player; // The target player

    public float _walkingDistance = 300.0f; // maximum distance which the enemy will start moving towards the player

    public float speed = 50.0f; //In what time will the enemy complete the journey between its position and the players position

    public bool _canMove = false; // Indicates if monster can move

    RoomStatus _temporaryRoom;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (_canMove == true)
        {
            MoveTowardsPlayer();
        }

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < _walkingDistance)
        {
            _canMove = true;
        }

    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }


    private void OnDestroy() // on enemy killed, reduce amount of remaining enemies
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject room in rooms)
        {
            
            _temporaryRoom = room.GetComponent<RoomStatus>();
            if (_temporaryRoom._currentRoom == true)
            {
                _temporaryRoom._enemiesRemaining--;
                break;
            }
            
        }
    }
}
