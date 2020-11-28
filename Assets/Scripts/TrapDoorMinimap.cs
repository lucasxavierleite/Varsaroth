using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorMinimap : MonoBehaviour
{
    // makes the trapdoor visible on the minimap
    private void OnBecameVisible()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
