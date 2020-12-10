using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedallionManager : MonoBehaviour
{
    private Vector3 _destination;
    
    [SerializeField]
    private Animator _animator;
    
    private bool _floating;
    
    [SerializeField]
    private float _speed = 10;

    [SerializeField]
    private TransitionManager _transitionManager;

    private void Start()
    {
        _transitionManager = GameObject.FindWithTag("TransitionManager").GetComponent<TransitionManager>();
        _destination = transform.position + new Vector3(0, 40);
        _floating = false;
    }
    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);
        if (transform.position == _destination && !_floating)
        {
            _floating = true;
            _animator.SetTrigger("float");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && _floating)
        {
            _transitionManager.ShowFinalTransition();
        }
    }
}
