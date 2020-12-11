using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  
    public GameObject player; // The target player
    SpriteRenderer _renderer;
    Animator _animator;
    Collider2D _collider;

    float _walkingDistance = 84.69f; // maximum distance which the enemy will start moving towards the player

    public float speed = 50.0f; //In what time will the enemy complete the journey between its position and the players position

    public bool _canMove; // Indicates if monster can move
    bool _canAttack;

    private float _attackRange = 20.0f;

    RoomStatus _temporaryRoom;

    string _currentDirection = "right";

    //enemy states
    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;
    const int STATE_DEAD = 3;
    const int STATE_TAKE_DAMAGE = 4;

    private int enemy_hp = 100;

    public int _currentAnimationState = STATE_IDLE;

    public Transform[] _attackPoint;
    public float _attackCircle = 5f;
    public LayerMask _enemyLayers;

	[SerializeField]
	private GameObject _enemyIcon;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        _animator = GetComponentInChildren<Animator>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _canMove = false;
        _canAttack = true;
    }

    private void Update()
    {
        if (_currentAnimationState != STATE_DEAD && PlayerMovement._currentAnimationState != 4 && _canAttack == true)
        {

            if (_canMove == true  && _currentAnimationState != STATE_ATTACK)
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

            float distance = Vector2.Distance(transform.position, player.transform.position);
            //move enemy
            if (distance < _walkingDistance && distance > _attackRange)
            {
                _canMove = true;
            }

            //stop moving and attack
            if (distance < _attackRange && _currentAnimationState != STATE_ATTACK)
            {
                _canMove = false;
                ChangeState(STATE_ATTACK);

            }
            
        }

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
            
            case STATE_TAKE_DAMAGE:
                _animator.SetTrigger("take_damage");
                break;
        }

        _currentAnimationState = state;
    }

    void StartAttack()
    {
        Collider2D[] _enemiesHit;

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
            enemy.SendMessageUpwards("TakeDamage", 30);
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
            ChangeState(STATE_IDLE);
            StartCoroutine(AttackCD());
        }

    }

    IEnumerator AttackCD()
    {
        _canAttack = false;
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 1.5 seconds.
        yield return new WaitForSeconds(0.5f);

        //After we have waited 1.5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        _canAttack = true;
    }

    /* Funcao do inimigo de receber dano */
    public void TakeDamage(int damTaken){
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
		_enemyIcon.SetActive(false); 

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
