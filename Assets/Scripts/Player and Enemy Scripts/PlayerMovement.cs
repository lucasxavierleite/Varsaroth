using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D _playerRB;
    Vector3 _moveDirection;
    float _moveSpeed = 100f;

    //player states
    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;
    const int STATE_DODGE = 3;
    const int STATE_DEAD = 4; 
    const int STATE_TAKE_DAMAGE = 5;

    public string _currentDirection = "right";
    public static int _currentAnimationState = STATE_IDLE;

    Animator _animator;
    SpriteRenderer _renderer;
    Collider2D _collider;

    private bool _attackInb;
    private bool _dodgeInb;

    private int player_hp = StageData._data.GetHP(); // StageData script holds global hp and max hp values

    public float _dashSpeed;
    public bool _isInvulnerable;


    public Transform _attackPoint;
    public float _attackRange = 10f;
    public LayerMask _enemyLayers;

    [SerializeField]
    private HpBar _hpBar;

    // Start is called before the first frame update
    void Start()
    {
        _currentAnimationState = STATE_IDLE;
        _playerRB = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _isInvulnerable = false;
        _hpBar.SetMaxHp((StageData._data.GetMAXHP()));
        _hpBar.SetHp(player_hp);
    }

    private void Update()
    {
        if (_currentAnimationState != STATE_DEAD && Time.timeScale != 0)
        {

            _attackInb = Input.GetKeyDown("k");
            _dodgeInb = Input.GetKeyDown("j");
            if (_attackInb && _currentAnimationState != STATE_DODGE)
            {
                if(_currentAnimationState == STATE_WALK || _currentAnimationState == STATE_IDLE){
                    if(AudioManager.instance.isPlaying("AtackMiss") == false){
                        AudioManager.instance.Play("AtackMiss");
                    }   
                }
                ChangeState(STATE_ATTACK);

                
            }
            if (_dodgeInb)
            {

                ChangeState(STATE_DODGE);

                AudioManager.instance.Play("Dodge");
            }
        }
    }

    private void FixedUpdate()
    {

            _moveDirection = new Vector3(Input.GetAxis("Horizontal") * _moveSpeed, Input.GetAxis("Vertical") * _moveSpeed, 0);

            if (_currentAnimationState != STATE_DODGE && _currentAnimationState != STATE_ATTACK && _currentAnimationState != STATE_DEAD)
            {
                MovePlayer(_moveDirection);
            }
        
        if(_currentAnimationState == STATE_WALK){
            if(AudioManager.instance.isPlaying("Steps") == false){
                AudioManager.instance.Play("Steps");
            }
        }else{
            AudioManager.instance.Stop("Steps");
        }
    }

    void MovePlayer(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            _playerRB.MovePosition(transform.position + (dir * Time.deltaTime));
            if (dir.x < 0)
            {
                ChangeDirection("left");
            }
            else if (dir.x > 0)
            {
                ChangeDirection("right");
            }
            ChangeState(STATE_WALK);
        }
        else
        {
            ChangeState(STATE_IDLE);
        }
    }

    /// <summary>
    /// Changes player direction
    /// </summary>
    /// <param name="direction">target direction</param>
    void ChangeDirection(string direction)
    {

        if (_currentAnimationState != STATE_ATTACK && _currentDirection != direction)
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
                transform.Rotate(0, 180, 0);
                _currentDirection = "left";
            }
        }

    }

    /// <summary>
    /// Changes player animation state
    /// </summary>
    /// <param name="state"> animation to play</param>
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

            case STATE_DODGE:
                _animator.SetTrigger("dodge");//3
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

    /// <summary>
    /// This Method makes the player attack whichever way he is looking
    /// </summary>
    void StartAttack()
    {
        Collider2D[] _enemiesHit;

        //detect enemies hit
        _enemiesHit = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        //damage enemies hit
        foreach (Collider2D enemy in _enemiesHit)
        {
            AudioManager.instance.Play("AtackHit");
            enemy.SendMessageUpwards("TakeDamage", 25);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    /// <summary>
    /// Stops attack
    /// </summary>
    void StopAttack()
    {
        //disable sword hitbox
        ChangeState(STATE_IDLE);

    }

    /// <summary>
    /// This Method makes the player dodge whichever way he is looking
    /// </summary>
    void StartDodge()
    {
        if (_currentDirection == "right")
        {
            _playerRB.velocity = Vector2.right * _dashSpeed;
        }
        else
        {
            _playerRB.velocity = Vector2.left * _dashSpeed;
        }
        ToggleInvulnerability();
    }
    /// <summary>
    /// Stops dodge ability
    /// </summary>
    void StopDodge()
    {   
        _playerRB.velocity = Vector2.zero;
        ChangeState(STATE_IDLE);
        ToggleInvulnerability();
    }

    /// <summary>
    /// Toggles invulnerability
    /// </summary>
    void ToggleInvulnerability()
    {
        if (_isInvulnerable == true)
        {
            _isInvulnerable = false;
        }
        else
        {
            _isInvulnerable = true;
        }
    }

   void OnKill()
   {
        ChangeState(STATE_DEAD);
        Time.timeScale = 0.5f;
        AudioManager.instance.Play("PlayerDeath");
  
   }

    /* Funcao do player de receber dano */
    public void TakeDamage(int damTaken){

        if (_isInvulnerable == false)
        {
            Debug.Log("Vida do player = " + player_hp);
            player_hp -= damTaken;
            _hpBar.SetHp(player_hp);
            ChangeState(STATE_TAKE_DAMAGE);
            AudioManager.instance.Play("GettingHit");

            StageData._data.SetHP(player_hp);

            if (player_hp <= 0)
            {
                OnKill();
            }
        }
    }

    public bool isAlive()
    {
        if (_currentAnimationState != STATE_DEAD)
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// This Method is activated when the player enters in contact with a trigger hitbox
    /// </summary>
    /// <param name="other"> The trigger that the player collided with</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Door")) // if player in contact with door, move camera
        {
            Debug.Log(other.name);
            if (other.name == "Door_Up")
            {
                Debug.Log(other.name);
                _playerRB.transform.position += Vector3.up * 90;
                Camera.main.transform.position += Vector3.up * 216;

            }
            else if (other.name == "Door_Down")
            {
                Debug.Log(other.name);
                _playerRB.transform.position += Vector3.down * 90;
                Camera.main.transform.position += Vector3.down * 216;
            }
            else if (other.name == "Door_Right")
            {
                Debug.Log(other.name);
                _playerRB.transform.position += Vector3.right * 100;
                Camera.main.transform.position += Vector3.right * 384;
            }
            else if (other.name == "Door_Left")
            {
                Debug.Log(other.name);
                _playerRB.transform.position += Vector3.left * 100;
                Camera.main.transform.position += Vector3.left * 384;
            }
        }
        else if (other.CompareTag("TrapDoor"))  // if player touches trapdoor, send him to next level
        {
            StageData._data.NextLevel();// go to next level
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
    }
}
