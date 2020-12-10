using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    Rigidbody2D _enemyRig;
    public GameObject player; // The target player
    Animator _animator;
    Collider2D _collider;

    public float _walkingDistance = 300.0f; // maximum distance which the enemy will start moving towards the player

    public float speed = 50.0f; //In what time will the enemy complete the journey between its position and the players position

    public bool _canMove = false; // Indicates if monster can move

    private float _attackRange = 40.0f;
    private int enemy_hp = 50;

    RoomStatus _temporaryRoom;

    string _currentDirection = "right";

    //enemy states
    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;
    const int STATE_DEAD = 3;
    const int STATE_TAKE_DAMAGE = 4;

    public float _currentAnimationState = STATE_IDLE;
    private bool _playerHit;

    public Transform _attackPoint;
    public float _attackCircle = 5f;
    public LayerMask _enemyLayers;

    [SerializeField]
    private GameObject _enemyIcon;

    private void Start()
    {
        _enemyRig = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<Collider2D>();
        _playerHit = false;
    }

    private void Update()
    {
        if (_currentAnimationState != STATE_DEAD && PlayerMovement._currentAnimationState != STATE_TAKE_DAMAGE && _currentAnimationState != STATE_ATTACK)
        {
            if (_canMove == true)
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
            float distanceY = - (transform.position.y - player.transform.position.y);
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
        else if (_currentAnimationState == STATE_ATTACK && _playerHit == false)
        {
            StartAttack();
        }
    }

    void MoveVertically(float direction)
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.up * direction, (speed/2) * Time.deltaTime);
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
                // _renderer.flipX = false;
                transform.Rotate(0, -180, 0);
                _currentDirection = "right";
            }
            else if (direction == "left")
            {
                // _renderer.flipX = true;
                transform.Rotate(0, -180, 0);
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
                AudioManager.instance.Play("RatSound");
                break;

            case STATE_DEAD:
                _animator.SetBool("dead", true);
                _collider.enabled = false;
                AudioManager.instance.Play("RatDying");
                break;

            case STATE_TAKE_DAMAGE:

                break;
        }

        _currentAnimationState = state;
    }

    void StartAttack()
    {
        Collider2D[] _enemiesHit;

        //detect enemies hit
        _enemiesHit = Physics2D.OverlapCircleAll(_attackPoint.position, _attackCircle, _enemyLayers);

        //damage enemies hit, for now kills them
        foreach (Collider2D enemy in _enemiesHit)
        {
            enemy.SendMessageUpwards("TakeDamage", 15);
            _playerHit = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackCircle);
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
            _playerHit = false;
        }

    }

    /* Funcao do inimigo de receber dano */
    public void TakeDamage(int damTaken)
    {
        enemy_hp -= damTaken;
        ChangeState(STATE_TAKE_DAMAGE);
        Debug.Log("Vida do inimigo = " + enemy_hp);

        if (enemy_hp <= 0)
        {
            OnKill();
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
        
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Light"))
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
