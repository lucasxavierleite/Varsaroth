using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
