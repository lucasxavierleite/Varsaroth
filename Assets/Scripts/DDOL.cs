using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    // This class allows us to manage any data that has to be transitioned from a scene to another 


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

}
