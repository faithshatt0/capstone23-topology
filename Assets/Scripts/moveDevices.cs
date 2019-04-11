using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveDevices : MonoBehaviour
    {
    Vector3 dist; //Distance
    float posX; //x
    float posY; //y
    float posZ; //z
    Vector3 prevLocation; //location so object doesn't move unless toggled to where it will
    Vector3 worldPos; //helps move object
   
    //if mouse clicked on device
    void OnMouseDown()
        {
        prevLocation = transform.position; //get device position
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        posZ = Input.mousePosition.z - dist.z;
        }
    //if mouse is dragging on device
    void OnMouseDrag()
        {
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, Input.mousePosition.z - posZ);
        worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = worldPos;
        }

    //Once you let go of the mouse click object will go right back to original place
    void OnMouseUp()
        {
        transform.position = prevLocation;
        }
    }
