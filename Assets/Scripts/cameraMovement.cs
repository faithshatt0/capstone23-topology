using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// cameraMovement.cs
/// In this script it allows the camera to move on the right mouse click. The camera will go around in a 180 degree around the center
/// 

public class cameraMovement : MonoBehaviour
    {
    public float dragSpeed = 2; 
    private float angle;
    public Text text;
    float camZoom = -10f;
    float camZoomSpeed = 2f;

    // Update is called once per frame
    void Update()
        {
        if(text.text == "Birdseye")
            {
            if (Input.GetMouseButtonDown(1))
                {
                angle = 60 * (Input.mousePosition.x < Screen.width / 2 ? 1 : -1) * Time.deltaTime;
                return;
                }
            if (!Input.GetMouseButton(1)) return;
            transform.RotateAround(Vector3.zero, Vector3.up, angle);
        }
    }
       
    }
