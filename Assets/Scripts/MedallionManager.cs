using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedallionManager : MonoBehaviour
{
    Vector3 destination;
    private void Start()
    {
        destination = transform.position + new Vector3(0, 40); 
    }

    float speed = 10;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }
}
