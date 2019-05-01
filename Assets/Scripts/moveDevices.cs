using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// moveDevices.cs
/// This script moves the devices that are not routers because it doesn't need a toggle button 
/// 

public class moveDevices : MonoBehaviour
    {
    Vector3 dist; //Distance
    float posX; //x
    float posY; //y
    float posZ; //z
    Vector3 prevLocation; //location so object doesn't move unless toggled to where it will
    Vector3 worldPos; //helps move object

    private float _sensitivity = 0.01f;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation = Vector3.zero;
    private bool _isRotating;


    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "viewObject_scene")
        {
            if (Input.GetMouseButton(1))
            {
                _isRotating = true;
                if (_isRotating)
                {
                    // get mouse offset
                    _mouseOffset = (Input.mousePosition - _mouseReference);
                    // apply rotation

                    _rotation.y = -(_mouseOffset.z + _mouseOffset.x) * _sensitivity; // rotate
                    gameObject.transform.Rotate(_rotation); // store new mouse position

                    return;
                }
            }

            if (!Input.GetMouseButton(1))
            {
                _isRotating = false;
                return;
            }

        }
    }

    //if mouse clicked on device
    void OnMouseDown()
        {
        if (SceneManager.GetActiveScene().name != "viewObject_scene")
            {
            prevLocation = transform.position; //get device position
            dist = Camera.main.WorldToScreenPoint(transform.position);
            posX = Input.mousePosition.x - dist.x;
            posY = Input.mousePosition.y - dist.y;
            posZ = Input.mousePosition.z - dist.z;
            }
        else
            {
            //put rotation here
            }    
        }

    //if mouse is dragging on device
    void OnMouseDrag()
        {
        if (SceneManager.GetActiveScene().name != "viewObject_scene")
            {
            Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, Input.mousePosition.z - posZ);
            worldPos = Camera.main.ScreenToWorldPoint(curPos);
            transform.position = worldPos;
            }
        else
            {
            //put rotation here
            }
        }

    //Once you let go of the mouse click object will go right back to original place
    void OnMouseUp()
        {
        if (SceneManager.GetActiveScene().name != "viewObject_scene")
            {
            transform.position = prevLocation;
            }
        else
            {
            //put rotation here
            }
            
        }
    }
