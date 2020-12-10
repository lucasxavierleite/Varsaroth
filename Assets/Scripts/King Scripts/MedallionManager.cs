using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedallionManager : MonoBehaviour
{
    private Vector3 destination;
    
    [SerializeField]
    private Animator _animator;
    private bool _floating;
    float speed = 10;
    
    private void Start()
    {
        destination = transform.position + new Vector3(0, 40);
        _floating = false;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (transform.position == destination && !_floating)
        {
            _floating = true;
            _animator.SetTrigger("float");
        }
    }
}
