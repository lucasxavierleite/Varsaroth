using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapZoom : MonoBehaviour
{
    [SerializeField]
    Camera minimapCam;

    bool _isZoomed = false;

    private void Update()
    {
        if (Input.GetKeyDown("tab") && Time.timeScale > 0)
        {
            if (_isZoomed)
            {
                minimapCam.orthographicSize = 600;
            }

            else
            {
                minimapCam.orthographicSize = 1200;
            }
            _isZoomed = !_isZoomed;
        }
    }
}
