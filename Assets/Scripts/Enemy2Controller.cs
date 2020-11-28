using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    Rigidbody2D _enemyRig;
    public GameObject player; // The target player
    SpriteRenderer _renderer;
    Animator _animator;
    Collider2D _collider;

    public float _walkingDistance = 300.0f; // maximum distance which the enemy will start moving towards the player

    public float speed = 50.0f; //In what time will the enemy complete the journey between its position and the players position

    public bool _canMove = false; // Indicates if monster can move

    private float _attackRange = 20.0f;
    private float _attackSpeed = 250.0f;

    RoomStatus _temporaryRoom;

    string _currentDirection = "right";

    //enemy states
    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;
    const int STATE_DEAD = 3;

    public float _currentAnimationState = STATE_IDLE;

    public Transform[] _attackPoint;
    public float _attackCircle = 5f;
    public LayerMask _enemyLayers;


    private void Start()
    {
        _enemyRig = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        _animator = GetComponentInChildren<Animator>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (_currentAnimationState != STATE_DEAD && PlayerMovement._currentAnimationState != 4)
        {
            if (_canMove == true && _currentAnimationState != STATE_ATTACK)
            {
                ChangeState(STATE_WALK);
                MoveTowardsPlayer();
            }

            //change direction enemy is looking
            if (transform.position.x < player.transform.position.x)
            {
                ChangeDirection("right");
            }
            else if (transform.position.x > player.transform.position.x)
            {
                ChangeDirection("left");
            }


            float distanceX = Mathf.Abs(transform.position.x - player.transform.position.x);
            float distanceY = (transform.position.y - player.transform.position.y);
            //move enemy
            if (distanceX < _walkingDistance && distanceX > _attackRange)
            {
                _canMove = true;
            }
            else if (distanceX < _attackRange && (distanceY > 10 || distanceY < -10))
            {
                MoveVertically(distanceY);
            }
            //stop moving and attack
            else if (distanceX < _attackRange && _currentAnimationState != STATE_ATTACK)
            {
                _canMove = false;
                ChangeState(STATE_ATTACK);

            }
        }

    }

    void MoveVertically(float direction)
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.up * direction, speed * Time.deltaTime);
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void ChangeDirection(string direction)
    {

        if (_currentDirection != direction)
        {
            if (direction == "right")
            {
                _renderer.flipX = false;
                _currentDirection = "right";
            }
            else if (direction == "left")
            {
                _renderer.flipX = true;
                _currentDirection = "left";
            }
        }

    }

    /// <summary>
    /// Changes enemy current state, and updates animation
    /// </summary>
    /// <param name="state"> target state </param>
    void ChangeState(int state)
    {

        if (_currentAnimationState == state)
            return;

        switch (state)
        {
            case STATE_IDLE:
                _animator.SetInteger("state", STATE_IDLE);//0
                break;

            case STATE_WALK:
                _animator.SetInteger("state", STATE_WALK);//1
                break;

            case STATE_ATTACK:
                _animator.SetTrigger("attack");//2
                break;

            case STATE_DEAD:
                _animator.SetBool("dead", true);
                _collider.enabled = false;
                break;

        }

        _currentAnimationState = state;
    }

    void StartAttack()
    {
        Collider2D[] _enemiesHit;

        if (_currentDirection == "right")
        {
            _enemyRig.velocity = Vector2.right * _attackSpeed;
        }
        else
        {
            _enemyRig.velocity = Vector2.left * _attackSpeed;
        }

        //detect enemies hit
        if (_currentDirection == "right")
        {
            _enemiesHit = Physics2D.OverlapCircleAll(_attackPoint[0].position, _attackCircle, _enemyLayers);
        }
        else
        {
            _enemiesHit = Physics2D.OverlapCircleAll(_attackPoint[1].position, _attackCircle, _enemyLayers);
        }


        //damage enemies hit, for now kills them
        foreach (Collider2D enemy in _enemiesHit)
        {
            enemy.SendMessageUpwards("OnKill");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.DrawWireSphere(_attackPoint[0].position, _attackCircle);
        Gizmos.DrawWireSphere(_attackPoint[1].position, _attackCircle);
    }

    /// <summary>
    /// Stops attack
    /// </summary>
    void StopAttack()
    {
        //disable sword hitbox
        if (_currentAnimationState == STATE_ATTACK)
        {
            _enemyRig.velocity = Vector2.zero;
            ChangeState(STATE_IDLE);
        }

    }


    void OnKill() // on enemy killed, reduce amount of remaining enemies
    {
        ChangeState(STATE_DEAD);
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
