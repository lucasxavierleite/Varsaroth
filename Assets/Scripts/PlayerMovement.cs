using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D _playerRB;
    Vector3 _moveDirection;
    private float _moveSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _moveDirection = new Vector3(Input.GetAxis("Horizontal") * _moveSpeed, Input.GetAxis("Vertical") * _moveSpeed, 0);
    }


    private void FixedUpdate()
    {
        //if (Player.state != dodging)
        //{
            MovePlayer(_moveDirection);
        //}  
    } 

    void MovePlayer(Vector3 dir)
    {
        _playerRB.MovePosition(transform.position + (dir * Time.deltaTime));
    }


    /// <summary>
    /// This Method is activated when the player enters in contact with a trigger hitbox
    /// </summary>
    /// <param name="other"> The trigger that the player collided with</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door")) // if player in contact with door, move camera
        {
            if (other.name == "Door Up")
            {
                _playerRB.transform.position += Vector3.up * 90;
                Camera.main.transform.position += Vector3.up * 216;
                
            }
            else if (other.name == "Door Down")
            {
                _playerRB.transform.position += Vector3.down * 90;
                Camera.main.transform.position += Vector3.down * 216;
            }
            else if (other.name == "Door Right")
            {
                _playerRB.transform.position += Vector3.right * 100;
                Camera.main.transform.position += Vector3.right * 384;
            }
            else if (other.name == "Door Left")
            {
                _playerRB.transform.position += Vector3.left * 100;
                Camera.main.transform.position += Vector3.left * 384;
            }
        }

        if (other.CompareTag("Enemy")) // if player in contact with enemy, do X
        {
            Destroy(other.gameObject); // for now, kills enemy
        }
    }
}
